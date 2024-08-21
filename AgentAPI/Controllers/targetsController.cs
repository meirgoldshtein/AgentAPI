using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AgentAPI.Context;
using AgentAPI.Models;
using Newtonsoft.Json.Linq;

namespace AgentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class targetsController : ControllerBase
    {
        private readonly AgentDbContext _context;

        public targetsController(AgentDbContext context)
        {
            _context = context;
        }

        // שליחת כל המטרות
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Target>>> GetTargets()
        {
            return await _context.Targets.ToListAsync();
        }


        // עדכון נקודות במרחב לפי שני נתוני אורך ורוחב
        [HttpPut("{id}/pin")]
        public async Task<IActionResult> PutTarget(int id, [FromBody] JObject data)
        {
            // פירוק החבילה של הגייסון
            int x = data["x"].ToObject<int>();
            int y = data["y"].ToObject<int>();

            var target = await _context.Targets.FindAsync(id);
            if (target == null)
            {
                return NotFound();
            }

            // עדכון המודל עם הנתונים החדשים
            target.X = x;
            target.Y = y;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TargetExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // קבלת משימה חדשה למסד הנתונים
        [HttpPost]
        public async Task<ActionResult<int>> PostTarget(string data)
        {
            Console.WriteLine(data);
            //// יצירת אובייקט חדש על פי הפרמטרים שקיבלנו
            //Target target = new Target { Name = data["name"].ToString(), Description = data["position"].ToString(),
            //    Photo_url = data["photo_url"].ToString(), Status = "a" };
            //_context.Targets.Add(target);
            //await _context.SaveChangesAsync();
            return Ok();
            //return Ok(new { Id = target.Id }); ;
        }



        // הזזת יעד לחיסול לאחת משמונה פינות אם מתאפשר חוקית
        [HttpPut("{id}/move")]
        public async Task<IActionResult> MoveTarget(int id, [FromBody] JObject data)
        {
            // פירוק החבילה של הגייסון
            int x = 0;
            int y = 0;

            // תרגום הסטרינג של הכיוון לנקודת ציון
            switch (data["direction"].ToString())
            {
                case "nw":
                    x -= 1;
                    y += 1;
                    break;
                case "n":
                    y += 1;
                    break;
                case "ne":
                    x += 1;
                    y += 1;
                    break;
                case "w":
                    x -=1 ;
                    break;
                case "e":
                    x += 1;
                    break;
                case "sw":
                    x -= 1;
                    y -= 1;
                    break;
                case "s":
                    y -= 1;
                    break;
                case "se":
                    x += 1;
                    y -= 1;
                    break;
            }

            var target = await _context.Targets.FindAsync(id);
            if (target == null)
            {
                return NotFound();
            }

            // עדכון המודל עם הנתונים החדשים
            // בדיקה מוקדמת שנקודת הציון אפשרית מבחינת גודל המטריצה
            if(target.X + x >= 0 && target.X + x < 1000 && target.Y + y >= 0 && target.Y + y < 1000)
            {
                target.X += x;
                target.Y += y;
            }


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TargetExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool TargetExists(int id)
        {
            return _context.Targets.Any(e => e.Id == id);
        }
    }
}

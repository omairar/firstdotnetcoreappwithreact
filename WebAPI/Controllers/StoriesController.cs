using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BOL;
using DAL;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] //applying JWT 
    [Route("api/[controller]")]
    [ApiController]
    public class StoriesController : ControllerBase
    {
        private readonly SSDBContext _context;

        public StoriesController(SSDBContext context)
        {
            _context = context;
        }

        // GET: api/Stories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Story>>> GetStories()
        {
            if (_context.Stories == null)
            {
                return NotFound();
            }
            return await _context.Stories.ToListAsync();
        }

        // GET: api/Stories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Story>> GetStory(int id)
        {
            if (_context.Stories == null)
            {
                return NotFound();
            }
            var story = await _context.Stories.FindAsync(id);

            if (story == null)
            {
                return NotFound();
            }

            return story;
        }


        // GET: api/Stories/5
        [HttpGet("getStoriesByUserID/{id}")]
        public async Task<ActionResult<Story>> GetStoriesByUserID(string id)
        {
            if (_context.Stories == null)
            {
                return NotFound();
            }
            var story = await _context.Stories.Where(x => x.Id == id).ToListAsync();

            if (story == null)
            {
                return NotFound();
            }

            return Ok(story);
        }


        // GET: api/getStoriesByStatus/true
        [HttpGet("getStoriesByStatus/{isApproved}")]

        [AllowAnonymous]
        public async Task<IActionResult> GetStoriesByStatus(bool isApproved)
        {
            if (_context.Stories == null)
            {
                return NotFound();
            }
            var stories = await _context.Stories.Where(x => x.IsApproved == isApproved).ToListAsync();



            return Ok(stories);
        }


        // Put: api/approveStory/5
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles = "Admin")]
        [HttpPut("approveStory/{id}")]
        public async Task<IActionResult> ApproveStory(int id, Story story)
        {
            if (id != story.SSid)
            {
                return BadRequest();
            }



            try
            {
                var stry = await _context.Stories.Where(x => x.SSid == id).AsNoTracking().FirstOrDefaultAsync();

                if (stry != null)
                {
                    stry.IsApproved = true;
                    _context.Stories.Update(stry);
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
            }
            catch (Exception E)
            {
                if (!StoryExists(id))
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



        // PUT: api/Stories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStory(int id, Story story)
        {
            if (id != story.SSid)
            {
                return BadRequest();
            }

            _context.Entry(story).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StoryExists(id))
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

        // POST: api/Stories
        [HttpPost]
        public async Task<ActionResult<Story>> PostStory(Story story)
        {
            try
            {
                if (_context.Stories == null)
                {
                    return Problem("Entity set 'SSDBContext.Stories'  is null.");
                }

                if (ModelState.IsValid)
                {
                    story.Id = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
                    story.CreatedOn = DateTime.Now;
                    story.IsApproved = false;

                    _context.Stories.Add(story);

                    await _context.SaveChangesAsync();

                    return CreatedAtAction("GetStory", new { id = story.SSid }, story);
                }
                else
                {
                    return BadRequest(ModelState);
                }

            }
            catch (Exception e)
            {
                var msg = (e.InnerException != null) ? (e.InnerException.Message) : (e.Message);

                return StatusCode(500, "Admin is working on it " + msg);
            }





        }



        // DELETE: api/Stories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStory(int id)
        {
            if (_context.Stories == null)
            {
                return NotFound();
            }
            var story = await _context.Stories.FindAsync(id);
            if (story == null)
            {
                return NotFound();
            }

            _context.Stories.Remove(story);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StoryExists(int id)
        {
            return (_context.Stories?.Any(e => e.SSid == id)).GetValueOrDefault();
        }
    }
}

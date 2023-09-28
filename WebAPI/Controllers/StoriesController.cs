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
using DAL.Interfaces;

namespace WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] //applying JWT 
    [Route("api/[controller]")]
    [ApiController]
    public class StoriesController : ControllerBase
    {
      

        IStoryDB _storyDB;

        public StoriesController( IStoryDB storyDB)
        {
          
            _storyDB = storyDB;
        }

        // GET: api/Stories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Story>>> GetStories()
        {
            return await _storyDB.GetAll().ToListAsync();
        }

        // GET: api/Stories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Story>> GetStory(int id)
        {
            
            var story = await _storyDB.GetById(id).FirstOrDefaultAsync();

            if (story == null)
            {
                return NotFound();
            }

            return Ok(story);
        }


        // GET: api/Stories/5
        [HttpGet("getStoriesByUserID/{id}")]
        public async Task<ActionResult<Story>> GetStoriesByUserID(string id)
        {
            var story = await _storyDB.GetByUserId(id).ToListAsync();

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
           
            var stories = await _storyDB.GetStoriesByStatus(isApproved).ToListAsync();
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
                var stry = await _storyDB.GetById(id).AsNoTracking().FirstOrDefaultAsync();

                if (stry != null)
                {
                    stry.IsApproved = true;

                    var pro = await _storyDB.Approve(stry);

                    if (pro)
                    {
                        return NoContent();
                    }
                    else
                    {
                        return BadRequest();
                    }
                   
                    
                }
            }
            catch (Exception E)
            {
                 return BadRequest();
               
            }

            return NoContent();
        }



      

        // POST: api/Stories
        [HttpPost]
        public async Task<ActionResult<Story>> PostStory(Story story)
        {
            try
            {
                

                if (ModelState.IsValid)
                {
                    story.Id = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
                    story.CreatedOn = DateTime.Now;
                    story.IsApproved = false;

                    _storyDB.Create(story);

                  

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
            

            var story = await _storyDB.Delete(id);
            if (story)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        //private bool StoryExists(int id)
        //{
        //    return (_context.Stories?.Any(e => e.SSid == id)).GetValueOrDefault();
        //}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Keepr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KeepsController : ControllerBase
    {
        private readonly KeepsService _ks;
        public KeepsController(KeepsService ks)
        {
            _ks = ks;
        }

        [HttpPost]
        [Authorize]
        public ActionResult<Keep> Post([FromBody] Keep newKeep)
        {
            try
            {
                Claim user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                if (user == null)
                {
                    throw new Exception("you must be logged in to Post");
                }
                newKeep.UserId = user.Value;
                return Ok(_ks.Create(newKeep));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<Keep>> Get()
        {
            try
            {
                return Ok(_ks.Get());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            };
        }

        [HttpGet("user")]
        public ActionResult<IEnumerable<Keep>> GetUserKeeps()
        {
            try
            {
                Claim user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                if (user == null)
                {
                    throw new Exception("not the right user");
                }
                string userId = user.Value;
                return Ok(_ks.GetUserKeeps(userId));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            };
        }

        [HttpGet("{id}")]
        public ActionResult<Keep> GetOne(int id)
        {
            try
            {
                return Ok(_ks.GetOne(id));
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }


         [HttpPut("{id}")]
    public ActionResult<Keep> Edit(int id, [FromBody] Keep keepToUpdate)
    {
      keepToUpdate.Id = id;
      try
      {
        return Ok(_ks.Edit(keepToUpdate));
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }

        [Authorize]
        [HttpDelete("{id}")]
        public ActionResult<string> Delete(int id)
        {
            try
            {
                Claim user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                if (user == null)
                {
                    throw new Exception("you must be logged in to delete");
                }
                string userId = user.Value;
                return Ok(_ks.Delete(id, userId));
            }
            catch (System.Exception error)
            {
                return BadRequest(error.Message);
            }
        }
    }
}
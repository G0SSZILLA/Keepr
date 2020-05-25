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

        // NOTE Gets all keeps
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

        // NOTE Gets keeps by ID
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Keep>> Get(int id)
        {
            try
            {
                return Ok(_ks.Get(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            };
        }

        // NOTE Gets User Keeps
        [HttpGet("userId")]
        [Authorize]
        public ActionResult<IEnumerable<Keep>> GetUserKeeps()
        {
            try
            {
                string userId = HttpContext.User.FindFirstValue("Id");
                return Ok(_ks.GetUserKeeps(userId));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // NOTE Creates new keep
        [HttpPost]
        [Authorize]
        public ActionResult<Keep> Post([FromBody] Keep newKeep)
        {
            try
            {
                var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                newKeep.UserId = userId;
                return Ok(_ks.Create(newKeep));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //NOTE Update a keep
        [Authorize]
        [HttpPut("{keepId}")]
        public ActionResult<Keep> Put(int keepId, [FromBody] Keep editKeep)
        {
            try
            {
                editKeep.Id = keepId;
                return Ok(_ks.Edit(editKeep));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

// NOTE Delete keep by ID
        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult<String> Delete(int id)
        {
            try
            {
                return Ok(_ks.Delete(id));

            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

    }
}
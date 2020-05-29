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
    public class VaultKeepsController : ControllerBase
    {
        private readonly VaultKeepsService _vks;
        public VaultKeepsController(VaultKeepsService vks)
        {
            _vks = vks;
        }

        [Authorize]
        [HttpPost]
        public ActionResult<VaultKeep> Create([FromBody] VaultKeep newVaultKeep)
        {
            try
            {
                var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                newVaultKeep.UserId = userId;
                return Ok(_vks.Create(newVaultKeep));
            }
            catch (System.Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<VaultKeep>> Get()
        {
            try
            {
                Claim user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                if (user == null)
                {
                    throw new Exception("You must be logged in!");
                }
                string userId = user.Value;

                return Ok(_vks.Get(userId));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            };
        }

        [Authorize]
        [HttpDelete("{id}")]
        public ActionResult<string> Delete(int id)
        {
            try
            {
                return Ok(_vks.Delete(id));
            }
            catch (System.Exception error)
            {
                return BadRequest(error.Message);
            }
        }
    }
}
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
        private readonly VaultKeepsService _vs;
        public VaultKeepsController(VaultKeepsService vs)
        {
            _vs = vs;
        }


        [HttpGet("{vaultId}")]
        [Authorize]
        public ActionResult<IEnumerable<Keep>> Get(int vaultId)
        {
            try
            {
                string userId = HttpContext.User.FindFirstValue("Id");
                return Ok(_vs.Get(vaultId, userId));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPost]
        [Authorize]
        public ActionResult<VaultKeep> Create([FromBody] VaultKeep vaultKeep)
        {
            try
            {
                string userId = HttpContext.User.FindFirstValue("Id");
                return Ok(_vs.Create(vaultKeep, userId));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPut]
        [Authorize]
        public ActionResult<string> Delete([FromBody] VaultKeep vaultKeep)
        {
            try
            {
                return Ok(_vs.Remove(vaultKeep));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

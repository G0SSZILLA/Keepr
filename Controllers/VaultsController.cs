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
    public class VaultsController : ControllerBase
    {
        private readonly VaultsService _vs;
        public VaultsController(VaultsService vs)
        {
            _vs = vs;
        }
        [HttpGet]
        // [Authorize]
        public ActionResult<IEnumerable<Vault>> Get()
        {
            try
            {
                // var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                return Ok(_vs.Get());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            };
        }
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<Vault> GetById(int id)
        {
            try
            {
                var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                return Ok(_vs.GetById(id, userId));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        [Authorize]
        public ActionResult<Vault> Create([FromBody] Vault newVault)
        {
            try
            {
                var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                newVault.UserId = userId;
                return Ok(_vs.Create(newVault));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult<String> Delete(int id)
        {
            try
            {
                return Ok(_vs.Delete(id));

            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        [HttpPost("{id}")]
        [Authorize]
        public ActionResult<Vault> Edit([FromBody] Vault update, int id)
        {
            try
            {
                update.Id = id;
                var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                return Ok(_vs.Edit(update));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
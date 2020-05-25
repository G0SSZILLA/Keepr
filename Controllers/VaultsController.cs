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
    [Route("api/[controller]")]
    public class VaultsController : ControllerBase
    {
        private readonly VaultsService _vs;
        public VaultsController(VaultsService vs)
        {
            _vs = vs;
        }

        // NOTE Gets vaults
        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<Vault>> Get()
        {
            try
            {
                string userId = HttpContext.User.FindFirstValue("Id");
                return Ok(_vs.Get(userId));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // NOTE Gets vault by ID
        [HttpGet("{vaultId}")]
        [Authorize]
        public ActionResult<Vault> Get(int vaultId)
        {
            try
            {
                return Ok(_vs.Get(vaultId));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // NOTE Adds Vaults
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
            catch (Exception error)
            {

                return BadRequest(error.Message);
            }
        }

        // NOTE Edits a vault
        [HttpPut("{vaultId}")]
        [Authorize]
        public ActionResult<Vault> Put(int vaultId, [FromBody] Vault editVault)
        {
            try
            {
                editVault.Id = vaultId;
                return Ok(_vs.Edit(editVault));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        // NOTE Deletes vault
        [HttpDelete("{vaultId}")]
        [Authorize]
        public ActionResult<string> Delete(int vaultId)
        {
            try
            {
                string userId = HttpContext.User.FindFirstValue("id");
               return Ok(_vs.Remove(vaultId, userId));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

    }
}
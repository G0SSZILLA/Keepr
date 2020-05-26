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
    public VaultKeepsController(VaultKeepsService vks) => _vks = vks;
    

    [HttpGet("{vaultId}/Keeps")]
    [Authorize]
    public ActionResult<IEnumerable<Keep>> Get(int vaultId)
    {
      try
      {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        return Ok(_vks.GetKeepsByVaultId(vaultId, userId));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpPost]
    [Authorize]
    public ActionResult<VaultKeep> Post([FromBody] VaultKeep newData )
    {
      try
      {
        newData.UserId = userId;
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        ;
        return Ok(_vks.Create(newData));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpDelete("{vaultId}/Keeps/{keepId}")]
    [Authorize]
    public ActionResult<String> Delete(int vaultId, int keepId)
    {
      try
      {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        return Ok(_vks.Delete(vaultId, keepId, userId));
      }
      catch (Exception e)
      {

        return BadRequest(e.Message); ;
      }
    }

  }
}
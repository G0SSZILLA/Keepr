using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _repo;
    public VaultKeepsService(VaultKeepsRepository repo)
    {
      _repo = repo;
    }

 public VaultKeep Create(VaultKeep newVaultKeep)
    {
      return _repo.Create(newVaultKeep);
    }

    public IEnumerable<VaultKeep> Get(string userId)
    {
      return _repo.Get(userId);
    }

    public VaultKeep GetOne(int id)
    {
      VaultKeep foundVaultKeep = _repo.GetOne(id);
      if (foundVaultKeep == null)
      {
        throw new Exception("Invalid Id");
      }
      return foundVaultKeep;
    }

      public IEnumerable<VaultKeepViewModel> GetKeepsByVaultId(int id)
    {
      return _repo.GetKeepsByVaultId(id);
    }

    internal string Delete(int id)
    {
      if (_repo.Delete(id))
      {
        return ("Successfully deleted!");
      }
      throw new Exception("Delete unsuccessful");
    }
  }
}
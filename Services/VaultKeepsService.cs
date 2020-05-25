using System;
using System.Collections.Generic;
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

        public IEnumerable<Keep> Get(int vaultId, string userId)
        {
            return _repo.Get(vaultId, userId);
        }

        public VaultKeep Create(VaultKeep vaultKeep, string userId)
        {
            vaultKeep.UserId = userId;
            int id = _repo.Create(vaultKeep);
            vaultKeep.Id = id;
            return vaultKeep;
        }

        public string Remove(VaultKeep vaultKeep)
        {
            VaultKeep exists = _repo.Check(vaultKeep.VaultId, vaultKeep.KeepId);
            if (exists == null) { throw new Exception("Invalid ID"); }
            _repo.Remove(exists.Id);
            return "Successfully deleted.";
        }
    }
}
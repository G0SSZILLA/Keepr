using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
    public class KeepsService
    {
        private readonly KeepsRepository _repo;
        public KeepsService(KeepsRepository repo)
        {
            _repo = repo;
        }

        public Keep Create(Keep newKeep)
        {
            return _repo.Create(newKeep);
        }

        public IEnumerable<Keep> Get()
        {
            return _repo.Get();
        }

        internal IEnumerable<Keep> GetKeepsByVaultId(string userId)
        {
            return _repo.GetKeepsByVaultId(userId);
        }

        internal IEnumerable<Keep> GetUserKeeps(string userId)
        {
            return _repo.GetUserKeeps(userId);
        }

        public Keep GetOne(int id)
        {
            Keep foundKeep = _repo.GetOne(id);
            if (foundKeep == null)
            {
                throw new Exception("Invalid Id");
            }
            return foundKeep;
        }


        public Keep Edit(Keep keepToUpdate)
        {
            Keep foundKeep = GetOne(keepToUpdate.Id);
            if (foundKeep != null)
            {
                if (foundKeep.Views < keepToUpdate.Views)
                {
                    if (_repo.UpdateViewCount(foundKeep))
                    {
                        return foundKeep;
                    }
                    return foundKeep;
                }

                if (foundKeep.Keeps < keepToUpdate.Keeps)
                {
                    if (_repo.UpdateKeptCount(foundKeep))
                    {
                        return foundKeep;
                    }
                    return foundKeep;
                }
            }
            throw new Exception("Unable to edit.");
        }

        internal string Delete(int id, string userId)
        {
            Keep foundKeep = GetOne(id);
            if (foundKeep.UserId != userId)
            {
                throw new Exception("This is not your keep!");
            }
            if (_repo.Delete(id, userId))
            {
                return "Sucessfully deleted.";
            }
            throw new Exception("Delete failed");
        }


    }
}
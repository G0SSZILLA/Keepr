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
        public KeepsService(KeepsRepository kr)
        {
            _repo = kr;
        }
        public IEnumerable<Keep> Get()
        {
            return _repo.Get();
        }

        public Keep GetById(int id)
        {
            var exists = _repo.GetById(id);
            if (exists == null) { throw new Exception("Invalid ID"); }
            return exists;
        }

        public Keep Create(Keep newKeep)
        {

            return _repo.Create(newKeep);
        }

        public Keep Edit(Keep update)
        {
            Keep exists = _repo.GetById(update.Id);
            if (exists == null)
            {
                throw new Exception("Invalid Id");
            }
            _repo.Edit(update);
            return update;
        }

        public object Delete(int id)
        {
            var exists = _repo.GetById(id);
            if (exists == null)
            {
                throw new Exception("Invalid ID");
            }
            _repo.Delete(id);
            return "Successfully Deleted";
        }
    }
}
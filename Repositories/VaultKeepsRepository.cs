using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Dapper;

namespace Keepr.Repositories
{
    public class VaultKeepsRepository
    {
        private readonly IDbConnection _db;

        public VaultKeepsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal VaultKeep Create(VaultKeep vaultKeepData)
        {
            string sql = @"
        INSERT INTO vaultkeeps
        (userId, vaultId, keepId)
        VALUES
        (@UserId, @VaultId, @KeepId);
        SELECT LAST_INSERT_ID()";
            vaultKeepData.Id = _db.ExecuteScalar<int>(sql, vaultKeepData);
            return vaultKeepData;
        }

        internal IEnumerable<VaultKeep> Get(string userId)
        {
            string sql = "SELECT * FROM vaultkeeps WHERE userid = @userId";
            return _db.Query<VaultKeep>(sql, new { userId });
        }
        internal VaultKeep GetOne(int id)
        {
            string sql = "SELECT * FROM vaultkeeps WHERE id = @Id";
            return _db.QueryFirstOrDefault<VaultKeep>(sql, new { id });
        }

        internal IEnumerable<VaultKeepViewModel> GetKeepsByVaultId(int id)
        {
            string sql = @"SELECT 
        k.*,
        vk.id as vaultKeepId
        FROM vaultkeeps vk
        INNER JOIN keeps k ON k.id = vk.keepId 
        WHERE (vaultId = @Id)";
            return _db.Query<VaultKeepViewModel>(sql, new { id });
        }

        internal bool Delete(int id)
        {
            string sql = "DELETE FROM vaultkeeps WHERE id = @id LIMIT 1";
            int affectedRows = _db.Execute(sql, new { id });
            return affectedRows == 1;
        }

    }
}
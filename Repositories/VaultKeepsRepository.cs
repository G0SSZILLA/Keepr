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

    public IEnumerable<VaultKeep> Get()
    {
      string sql = "SELECT * FROM vaultkeeps WHERE isPrivate = 0;";
      return _db.Query<VaultKeep>(sql);
    }
    public VaultKeep GetById(int id)
    {
      string sql = "SELECT * FROM vaultkeeps WHERE id = @id";
      return _db.QueryFirstOrDefault<VaultKeep>(sql, new { id });

    }
    internal VaultKeep GetById(int vaultId, int keepId, string userId)
    {
      string sql = @"SELECT * FROM vaultkeeps WHERE vaultId = @VaultId AND keepId = @KeepId AND userId = @UserId";
      return _db.QueryFirstOrDefault<VaultKeep>(sql, new { vaultId, keepId, userId });
    }

    internal IEnumerable<Keep> GetKeepsByVaultId(int vaultId, string userId)
    {
      string sql = @"
      SELECT k.* FROM vaultkeeps vk
      INNER JOIN keeps k ON k.id = vk.keepId 
      WHERE (vk.vaultId = @VaultId AND vk.userId = @UserId)
      ";
      return _db.Query<Keep>(sql, new { vaultId, userId });
    }

    internal VaultKeep Find(int vaultId, int keepId)
    {
      string sql = "SELECT * FROM vaultkeeps WHERE (VaultId = @vaultId AND KeepId = @keepId)";
      return _db.QueryFirstOrDefault(sql, new { vaultId, keepId });
    }


    internal int Create(VaultKeep newData)
    {
      string sql = @"
            INSERT INTO vaultkeeps (vaultId, keepId, userId) VALUES (@VaultId, @KeepId, @UserId);
            SELECT LAST_INSERT_ID();";
      int id = _db.ExecuteScalar<int>(sql, newData);
      // newData.Id = id;
      return id;
    }


    internal void Delete(int vaultId, int keepId, string userId)
    {
      string sql = @"DELETE FROM vaultkeeps WHERE vaultId = @vaultId AND keepId = @keepId AND userId = @userId LIMIT 1";
      _db.Execute(sql, new { vaultId, keepId, userId });
    }
  }
}
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Keepr.Models;
using Dapper;

namespace Keepr.Repositories
{
  public class VaultsRepository
  {
    private readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal IEnumerable<Vault> Get(string userId)
    {
      string sql = "SELECT * FROM vaults WHERE userId = @userId;";
      return _db.Query<Vault>(sql, new { userId });
      // return _db.Query<Vault>(sql);
    }
    internal Vault GetById(int id)
    {
      string sql = "SELECT * FROM vaults WHERE id = @id";
      return _db.QueryFirstOrDefault<Vault>(sql, new { id });
    }

    internal Vault Create(Vault vaultData)
    {
      string sql = @"
            INSERT INTO vaults (name, description, userId) VALUES (@Name, @Description, @UserId);
            SELECT LAST_INSERT_ID();";
      int id = _db.ExecuteScalar<int>(sql, vaultData);
      vaultData.Id = id;
      return vaultData;
    }

    internal void Delete(int id)
    {
      string sql = "DELETE FROM vaults WHERE id = @id";
      _db.Execute(sql, new { id });
    }

    internal void Edit(Vault update)
    {
      throw new NotImplementedException();
    }
  }
}
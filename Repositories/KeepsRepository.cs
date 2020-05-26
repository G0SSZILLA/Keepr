using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Dapper;

namespace Keepr.Repositories
{
  public class KeepsRepository
  {
    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal IEnumerable<Keep> Get()
    {
      string sql = "SELECT * FROM keeps;";
      return _db.Query<Keep>(sql);
    }
    internal Keep GetById(int Id)
    {
      string sql = "SELECT * FROM keeps WHERE id = @Id";
      return _db.QueryFirstOrDefault<Keep>(sql, new { Id });
    }

    internal Keep Create(Keep keepData)
    {
      string sql = @"
            INSERT INTO keeps (name, description, img, isPrivate) VALUES (@Name, @Description, @Img, @IsPrivate);
            SELECT LAST_INSERT_ID();";
      int id = _db.ExecuteScalar<int>(sql, keepData);
      keepData.Id = id;
      return keepData;
    }
    internal void Edit(Keep update)
    {
      string sql = @"
      UPDATE keeps
      SET name = @Name
      WHERE id = @Id";
      _db.Execute(sql, update);
    }
    internal void Delete(int id)
    {
      string sql = "DELETE FROM keeps WHERE id = @id";
      _db.Execute(sql, new { id });
    }

  }
}
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

        internal Keep Create(Keep KeepData)
        {
            string sql = @"
        INSERT INTO keeps
        (userId, name, description, img, isPrivate, views, shares, keeps)
        VALUES
        (@UserId, @Name, @Description, @Img, @IsPrivate, @Views, @Shares, @Keeps);
        SELECT LAST_INSERT_ID()";
            KeepData.Id = _db.ExecuteScalar<int>(sql, KeepData);
            return KeepData;
        }

        

        internal bool AddKeep(Keep KeepData)
        {
            string sql = @"
          UPDATE keeps SET
          keeps = keeps + 1
          WHERE id = @Id LIMIT 1";
            int affectedRows = _db.Execute(sql, new { KeepData.Id });
            return affectedRows == 1;
        }

        internal bool AddShare(Keep KeepData)
        {
            string sql = @"
          UPDATE keeps SET
          shares = shares + 1
          WHERE id = @Id LIMIT 1";
            int affectedRows = _db.Execute(sql, new { KeepData.Id });
            return affectedRows == 1;
        }

        internal bool UpdateViewCount(Keep KeepData)
        {
            string sql = @"
          UPDATE keeps SET
          views = views + 1
          WHERE id = @Id LIMIT 1";
            int affectedRows = _db.Execute(sql, new { KeepData.Id });
            return affectedRows == 1;
        }

        internal bool UpdateKeptCount(Keep keepToUpdate)
    {
      string sql = @"
      UPDATE keeps
      SET keeps = keeps + 1
      WHERE id = @Id";
      int affectedRows = _db.Execute(sql, keepToUpdate);
      return affectedRows == 1;
    }
        internal IEnumerable<Keep> Get()
        {
            string sql = "SELECT * FROM keeps WHERE isPrivate = 0";
            return _db.Query<Keep>(sql);
        }

        internal IEnumerable<Keep> GetKeepsByVaultId(string userId)
        {
            string sql = "SELECT * FROM keeps WHERE userid = @UserId";
            return _db.Query<Keep>(sql, new { userId });
        }

        internal IEnumerable<Keep> GetUserKeeps(string userId)
        {
            string sql = "SELECT * FROM keeps WHERE userid = @UserId";
             return _db.Query<Keep>(sql, new { userId });
        }
        internal Keep GetOne(int id)
        {
            string sql = "SELECT * FROM keeps WHERE id = @Id";
            return _db.QueryFirstOrDefault<Keep>(sql, new { id });
        }

        internal bool Delete(int id, string userId)
        {
            string sql = "DELETE FROM keeps WHERE id = @Id AND userId = @UserId LIMIT 1";
            int affectedRows = _db.Execute(sql, new { id, userId });
            return affectedRows == 1;
        }



    }
}
using SEDC.NotesAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper.Contrib.Extensions;
using System.Data.SqlClient;
using Dapper;
using System.Linq;

namespace SEDC.NotesAPI.DataAccess.DapperNoteRepository
{
    public class NoteDapperRepository : IRepository<Note>
    {
        private string _connectionString;
        public NoteDapperRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(Note entity)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                sqlConnection.Insert<Note>(entity);
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                // One way
                //Note entity = sqlConnection.Query<Note>("SELECT * FROM dbo.Notes WHERE Id = @NoteId", new { NoteId = id }).FirstOrDefault();
                //sqlConnection.Delete<Note>(entity);

                // Another way
                string query = "DELETE FROM dbo.Notes WHERE Id = @NoteId";
                sqlConnection.Execute(query, new { NoteId = id });
            }
        }

        public List<Note> GetAll()
        {
            using(SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                List<Note> notesDb = sqlConnection.Query<Note>("SELECT * FROM dbo.Notes").ToList();
                return notesDb;
            }
        }

        public Note GetById(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                return sqlConnection.Query<Note>("SELECT * FROM dbo.Notes WHERE Id = @NoteId", new { NoteId = id }).FirstOrDefault();
            }
        }

        public void Update(Note entity)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                sqlConnection.Update<Note>(entity);
            }
        }
    }
}

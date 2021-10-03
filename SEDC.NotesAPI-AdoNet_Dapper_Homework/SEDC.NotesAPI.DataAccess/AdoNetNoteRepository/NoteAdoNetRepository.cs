using SEDC.NotesAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using SEDC.NotesAPI.Domain.Enums;
using System.Linq;

namespace SEDC.NotesAPI.DataAccess.AdoNetNoteRepository
{
    public class NoteAdoNetRepository : IRepository<Note>
    {
        private string _connectionString;
        public NoteAdoNetRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(Note entity)
        {
            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = sqlConnection;

            // SQL Injection attack
            //            command.CommandText = $@"INSERT INTO Notes(Text, Color, Tag, UserId)
            //VALUES('{entity.Text}', '{entity.Color}', {entity.Tag}, {entity.UserId})";

            // In order to prevent attacks, we should validate the entity and its values

            command.CommandText = $@"INSERT INTO Notes(Text, Color, Tag, UserId)
VALUES(@NoteText, @NoteColor, @NoteTag, @NoteUserId)";
            command.Parameters.AddWithValue("@NoteText", entity.Text);
            command.Parameters.AddWithValue("@NoteColor", entity.Color);
            command.Parameters.AddWithValue("@NoteTag", entity.Tag);
            command.Parameters.AddWithValue("@NoteUserId", entity.UserId);

            command.ExecuteNonQuery(); // user for Queries that do NOT return sets of data
            sqlConnection.Close();
        }

        public void Delete(int id)
        {
            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = sqlConnection;

            command.CommandText = "DELETE FROM Notes WHERE Id = @id";
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public List<Note> GetAll()
        {
            // 1. Create new connection to a database
            SqlConnection sqlConnection = new SqlConnection(_connectionString);

            // 2. Open an SQL Connection with the provided connection string
            sqlConnection.Open();

            // 3. Create an SQL Command(Class that represents an instruction to SQL)
            SqlCommand command = new SqlCommand();

            // 4. Give the command a connection to the database
            command.Connection = sqlConnection;

            // 5. Execute the command
            command.CommandText = "SELECT * FROM dbo.Notes";

            List<Note> notesDb = new List<Note>();

            // 6. Create an SQL Data Reader if there is a need to read more data
            SqlDataReader reader = command.ExecuteReader();

            // 7. Get and convert the data needed
            while (reader.Read())
            {
                notesDb.Add(new Note
                {
                    Id = (int)reader["Id"],
                    Text = (string)reader["Text"],
                    Color = (string)reader["Color"],
                    Tag = (TagType)reader["Tag"],
                    UserId = (int)reader["UserId"]
                });
            }

            // 7. Close the SQL Connection
            sqlConnection.Close();

            return notesDb;
        }

        public Note GetById(int id)
        {
            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = sqlConnection;

            // command.CommandText = $"SELECT * FROM dbo.Notes WHERE Id = {id}"; // bad example - SQL Ijection attack
            command.CommandText = $"SELECT * FROM dbo.Notes WHERE Id = @id";
            command.Parameters.AddWithValue("@id", id);

            SqlDataReader reader = command.ExecuteReader();

            List<Note> notesDb = new List<Note>();

            while (reader.Read())
            {
                notesDb.Add(new Note
                {
                    Id = (int)reader["Id"],
                    Text = (string)reader["Text"],
                    Color = (string)reader["Color"],
                    Tag = (TagType)reader["Tag"],
                    UserId = (int)reader["UserId"]
                });
            }

            sqlConnection.Close();

            return notesDb.FirstOrDefault();
        }

        public void Update(Note entity)
        {
            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = sqlConnection;

            command.CommandText = $@"UPDATE dbo.Notes SET Text = @NoteText, Color = @NoteColor, Tag = @NoteTag, UserId = @NoteUserId
WHERE Id = @NoteId";
            command.Parameters.AddWithValue("@NoteId", entity.Id);
            command.Parameters.AddWithValue("@NoteText", entity.Text);
            command.Parameters.AddWithValue("@NoteColor", entity.Color);
            command.Parameters.AddWithValue("@NoteTag", entity.Tag);
            command.Parameters.AddWithValue("@NoteUserId", entity.UserId);

            command.ExecuteNonQuery();
            sqlConnection.Close();
        }
    }
}

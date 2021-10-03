using SEDC.NotesAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SEDC.NotesAPI.DataAccess.AdoNetNoteRepository
{
    public class UserAdoNetRepository : IRepository<User>
    {

           private string _connectionString;
            public UserAdoNetRepository(string connectionString)
            {
                _connectionString = connectionString;
            }

            public void Add(User entity)
            {
                SqlConnection sqlConnection = new SqlConnection(_connectionString);
                sqlConnection.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = sqlConnection;

                command.CommandText = $@"INSERT INTO Users(FirstName, LastName, UserName)
                VALUES(@UserFirstName, @UserLastName, @UserUserName)";
                command.Parameters.AddWithValue("@UserFirstName", entity.FirstName);
                command.Parameters.AddWithValue("@UserLastName", entity.LastName);
                command.Parameters.AddWithValue("@UserUserName", entity.Username);



            command.ExecuteNonQuery(); 
                sqlConnection.Close();
            }

            public void Delete(int id)
            {
                SqlConnection sqlConnection = new SqlConnection(_connectionString);
                sqlConnection.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = sqlConnection;

                command.CommandText = "DELETE FROM Users WHERE Id = @id";
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
                sqlConnection.Close();
            }

            public List<User> GetAll()
            {
               
                SqlConnection sqlConnection = new SqlConnection(_connectionString);

            
                sqlConnection.Open();

                
                SqlCommand command = new SqlCommand();

               
                command.Connection = sqlConnection;

              
                command.CommandText = "SELECT * FROM dbo.Users";

                List<User> usersDb = new List<User>();

               
                SqlDataReader reader = command.ExecuteReader();

              
                while (reader.Read())
                {
                    usersDb.Add(new User
                    {
                        Id = (int)reader["Id"],
                        FirstName = (string)reader["FirstName"],
                        LastName = (string)reader["LastName"],
                        Username = (string)reader["UserName"],
                    });
                }

                sqlConnection.Close();

                return usersDb;
            }

            public User GetById(int id)
            {
                SqlConnection sqlConnection = new SqlConnection(_connectionString);
                sqlConnection.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = sqlConnection;

                command.CommandText = $"SELECT * FROM dbo.Users WHERE Id = @id";
                command.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = command.ExecuteReader();

                List<User> usersDb = new List<User>();

                while (reader.Read())
                {
                    usersDb.Add(new User
                    {
                        Id = (int)reader["Id"],
                        FirstName = (string)reader["FirstName"],
                        LastName = (string)reader["LastName"],
                        Username = (string)reader["UserName"],
                    });
                }

                sqlConnection.Close();

                return usersDb.FirstOrDefault();
            }

            public void Update(User entity)
            {
                SqlConnection sqlConnection = new SqlConnection(_connectionString);
                sqlConnection.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = sqlConnection;

                command.CommandText = $@"UPDATE dbo.Users SET Id = @UserId, FirstName  @UserFirstName, LastName = @UserLastName,UserName = @UserUserName 
                WHERE Id = @Id";
                command.Parameters.AddWithValue("@UserId", entity.Id);
                command.Parameters.AddWithValue("@UserFirstName", entity.FirstName);
                command.Parameters.AddWithValue("@UserLastName", entity.LastName);

                command.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }
}

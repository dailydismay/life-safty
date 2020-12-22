using Npgsql;
using SemesterWork.DAL.Dto;
using SemesterWork.DAL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SemesterWork.DAL.Entities;
using SemesterWork.DAL.Repository;

namespace SemesterWork.DAL.Repository
{
    public class UserRepo : IUserRepo
    {
        private const string FindByEmailQuery = "SELECT id, email, username, password, created_at, first_name, last_name, patronymic, birth_date FROM users where email = @email;";
        private const string FindByDoctorIdQuery = "SELECT u.id, email, username, password, created_at, first_name, last_name, patronymic, birth_date, d.id, d.is_verified, d.user_id FROM users u RIGHT JOIN doctors d ON d.user_id = u.id WHERE d.id = 1;";
        private const string InsertOneQuery = "INSERT INTO users (username, email, password) VALUES (@username, @email, @password) returning id";
        private const string FindByIdQuery =
            "SELECT id, email, username, password, created_at, first_name, last_name, patronymic, birth_date FROM users where id = @id;";

        private const string UpdateByIdQuery = @"
            UPDATE users
            SET 
                first_name = @first_name,
                last_name = @last_name,
                patronymic = @patronymic,
                birth_date = @birth_date
            WHERE id = @id;
        ";
        
        private NpgsqlConnection Conn { get; set; }

        public UserRepo(NpgsqlConnection conn)
        {
            Conn = conn;
        }
        
        public async Task<UserEntity> FindByEmail(string email)
        {
            await using var query =
                new NpgsqlCommand(FindByEmailQuery,
                    Conn);
            query.Parameters.AddWithValue("email", email);

            await using (var reader = await query.ExecuteReaderAsync())
            {
                await reader.ReadAsync();
        
                if (!reader.HasRows)
                { 
                    return null;
                }
            
                var user = MapToEntity(reader);
                return user;    
            }
        }

        public async Task<UserEntity> Create(UserEntity user)
        {
            try
            {
                var query = new NpgsqlCommand(
                    InsertOneQuery, Conn
                    );

                query.Parameters.AddWithValue("username", user.Username);
                query.Parameters.AddWithValue("email", user.Email);
                query.Parameters.AddWithValue("password", user.Password);
            
                await query.ExecuteNonQueryAsync();
        
                return await FindByEmail(user.Email);   
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<UserEntity> FindById(long id)
        {
            await using var query = new NpgsqlCommand(FindByIdQuery, Conn);
            query.Parameters.AddWithValue("id", id);

            await using var reader = await query.ExecuteReaderAsync();
            await reader.ReadAsync();
        
            if (!reader.HasRows)
            { 
                return null;
            }
            
            var user = MapToEntity(reader);
            return user;
        }

        public async Task<Pageable<UserEntity>> Read(PageableArgs args)
        {
            throw new NotImplementedException();
        }


        public async Task<UserEntity> Update(UserEntity sourceObject, UserEntity updatedFields)
        {
            await using var query =
                new NpgsqlCommand(UpdateByIdQuery,
                    Conn);
            
            
            
            query.Parameters.AddWithValue("id", sourceObject.Id);
            query.Parameters.AddWithValue("first_name", updatedFields.FirstName);
            query.Parameters.AddWithValue("last_name", updatedFields.LastName);
            query.Parameters.AddWithValue("patronymic", updatedFields.Patronymic);
            query.Parameters.AddWithValue("birth_date", updatedFields.BirthDate);
            
            var updated = await query.ExecuteNonQueryAsync();
            if (updated == 0) return null;
            
            return await FindById(sourceObject.Id);
        }

        public async Task Delete(UserEntity sourceObject)
        {
            throw new NotImplementedException();
        }

        private UserEntity MapToEntity(NpgsqlDataReader reader) => new UserEntity()
        {
            Id = reader.GetInt64(0),
            Email = reader.GetString(1),
            Username = reader.GetString(2),
            Password = reader.GetString(3),
            CreatedAt = reader.GetDateTime(4),
            FirstName = reader.IsDBNull(8) ? null : reader.GetString(5),
            LastName =  reader.IsDBNull(8) ? null : reader.GetString(6),
            Patronymic =  reader.IsDBNull(8) ? null : reader.GetString(7),
            BirthDate =  reader.IsDBNull(8) ? default(DateTime) : reader.GetDateTime(8),
        };
    }
}

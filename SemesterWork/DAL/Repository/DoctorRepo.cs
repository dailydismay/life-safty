using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Npgsql;
using SemesterWork.DAL.Dto;
using SemesterWork.DAL.Entities;
using SemesterWork.DAL.Repository.Interfaces;

namespace SemesterWork.DAL.Repository
{
    public class DoctorRepo : IDoctorRepo
    {
        private NpgsqlConnection Conn { get; set; }
        private const string CreateDoctorQuery = "insert into doctors (is_verified, user_id, specialization, description, start_time) VALUES (true, @userId, @spez, @descr, @start_time);";
        private const string ListDoctorQuery = @"
            select 
               d.id, d.is_verified, d.specialization,
               d.description, d.start_time, u.id,
               u.first_name, u.last_name, u.patronymic,
               u.email, u.birth_date 
            from doctors d
            left join users u on u.id = d.user_id
        ";

        private const string CountDoctorQuery = @"
        select
            count(d.*)
        from doctors d
        left join users u on u.id = d.user_id
        ";
        
        private const string ListSpecializationsQuery = @"WITH specialisations(spec_type) AS (select unnest(enum_range(null::spez_type))) select spec_type from specialisations where spec_type != '';";
        
        public DoctorRepo(NpgsqlConnection conn)
        {
            Conn = conn;
        }

        public async Task<DoctorEntity> Create(DoctorEntity doctor)
        {
            var query = new NpgsqlCommand(
                CreateDoctorQuery, Conn
            );

            query.Parameters.AddWithValue("userId", doctor.UserId);
            query.Parameters.AddWithValue("spez", doctor.Specialization);
            query.Parameters.AddWithValue("descr", doctor.Description);
            query.Parameters.AddWithValue("start_time", doctor.StartTime);
            
            
            await query.ExecuteNonQueryAsync();
        
            return null;   
        }

        public async Task<Pageable<DoctorResponse>> Read(ListDoctorsDto args)
        {
            var doctors = new List<DoctorResponse>();
            var result = new Pageable<DoctorResponse> {Items = doctors, Total = 0};

            var sql = ListDoctorQuery;

            var whereQ = "WHERE 1 = 1";

            if (!args.Specialization.Equals(string.Empty))
            {
                whereQ += "AND specialization = @spec";
            }
            sql += whereQ;
            sql += $"limit {args.PerPage} offset {args.PerPage * args.Page}";

            
            await using var query =
                new NpgsqlCommand(sql, Conn);

            if (!args.Specialization.Equals(string.Empty))
            {
                query.Parameters.AddWithValue("spec", args.Specialization);
            }

            await using var reader = await query.ExecuteReaderAsync();
            try
            {
                while (await reader.ReadAsync())
                    doctors.Add(MapToDoctorResp(reader));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            var countSql = CountDoctorQuery;

            if (!args.Specialization.Equals(string.Empty))
            {
                countSql += "where specialization = @spec";
            }

            await query.DisposeAsync();
            await reader.CloseAsync();
            
            await using var countQuery = new NpgsqlCommand(countSql, Conn);
            
            if (!args.Specialization.Equals(string.Empty))
            {
                countQuery.Parameters.AddWithValue("spec", args.Specialization);
            }

            var countReader = (long)await countQuery.ExecuteScalarAsync();

            result.Total = countReader;
            
            return result;
        }

        private DoctorResponse MapToDoctorResp (NpgsqlDataReader reader) => new DoctorResponse()
            {
                Profile = new ProfileResponse()
                {
                    Id = reader.GetInt64(5),
                    FirstName = reader.GetString(6),
                    LastName = reader.GetString(7),
                    Patronymic = reader.GetString(8),
                    Email = reader.GetString(9),
                    BirthDate = reader.GetDateTime(10).ToShortDateString()
                },
                Doctor = new DoctorEntity()
                {
                    Id = reader.GetInt64(0),
                    IsVerified = reader.GetBoolean(1),
                    Specialization = reader.GetString(2),
                    Description = reader.GetString(3),
                    StartTime = reader.GetDateTime(4)
                }
            };
        
        public async Task<List<string>> ListSpecializations()
        {
            
            await using var query =
                new NpgsqlCommand(ListSpecializationsQuery, Conn);
            
            await using (var reader = await query.ExecuteReaderAsync())
            {
                try
                {
                    var result = new List<string>();

                    while (await reader.ReadAsync())
                        result.Add(reader.GetString(0));
             
                    return result;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
        
        public async Task<DoctorEntity> FindForUser()
        {
            return null;
        }
        
        private DoctorEntity MapToEntity(NpgsqlDataReader reader) => new DoctorEntity()
        {
            Id = reader.GetInt64(0),
            UserId = reader.GetInt64(1),
            Specialization = reader.GetString(2),
            Description = reader.GetString(3),
            IsVerified = reader.GetBoolean(4),
            StartTime = reader.IsDBNull(5) ? default(DateTime) : reader.GetDateTime(5),
        };
    }
}
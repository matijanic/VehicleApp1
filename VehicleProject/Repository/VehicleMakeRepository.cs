using Dapper;
using Microsoft.VisualBasic;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using VehicleProject.Models;
using VehicleProject.Util;
using Constants = VehicleProject.Util.Constants;

namespace VehicleProject.Repository
{
    public class VehicleMakeRepository
    {
        public async Task<List<VehicleMake>> GetVehicleMakes()
        {
            var vehicleMakes = new List<VehicleMake>();
            //var sql = "SELECT \"Id\",\"Name\",\"Abrv\", \"DateCreated\",\"DateUpdated\" FROM \"VehicleMake\"";
            var sql ="Select * From \"VehicleMake\"";
            
            using (var connection = new NpgsqlConnection(Constants.connectionString))
            {
               await connection.OpenAsync();

                vehicleMakes = (await connection.QueryAsync<VehicleMake>(sql)).ToList();

            }

            return vehicleMakes;
            
            //using (var connection = new NpgsqlConnection(Constants.connectionString))
            //{

            //    await connection.OpenAsync();


            //    using (var command = new NpgsqlCommand("SELECT \"VehicleMake_id\",\"Name\",\"Abrv\", \"DateCreated\",\"DateUpdated\" FROM \"VehicleMake\"", connection))
            //    {
            //        using (var reader = await command.ExecuteReaderAsync())
            //        {
            //            while (await reader.ReadAsync())
            //            {
            //                var vehicleMake = new VehicleMake
            //                {
            //                    Id = reader.GetInt32(reader.GetOrdinal("VehicleMake_id")),
            //                    Name = reader.GetString(reader.GetOrdinal("Name")),
            //                    Abrv = reader.GetString(reader.GetOrdinal("Abrv")),
                                
            //                    DateCreated = reader.GetDateTime(reader.GetOrdinal("DateCreated")),
            //                    DateUpdated = reader.GetDateTime(reader.GetOrdinal("DateUpdated"))
            //                };

            //                vehicleMakes.Add(vehicleMake);
            //            }
            //        }
             
            //    }   

            //}

            //return vehicleMakes;
           
        }

        public async Task AddVehicleMake(VehicleMake newVehicleMake)
        {
            using (var connection = new NpgsqlConnection(Constants.connectionString))
            {
                var sql = "insert into \"VehicleMake\" (\"Name\",\"Abrv\", \"DateCreated\",\"DateUpdated\") values (@Name, @Abrv,@DateCreated,@DateUpdated);";

                await connection.OpenAsync();

                var vehicleMake = new VehicleMake()
                {
                    Name = newVehicleMake.Name,
                    Abrv = newVehicleMake.Abrv,
                    DateCreated = newVehicleMake.DateCreated,
                    DateUpdated = newVehicleMake.DateUpdated,

                };

                var rowsAffected =await connection.ExecuteAsync(sql,vehicleMake);
            }

            //using (var connection = new NpgsqlConnection(Constants.connectionString))
            //{
            //    await connection.OpenAsync();
              
            //    using (var command = new NpgsqlCommand("Insert into \"VehicleMake\" (\"Name\",\"Abrv\",\"DateCreated\", \"DateUpdated\") values (@Name,@Abrv,@DateCreated, @DateUpdated)", connection))
            //    {
            //        command.Parameters.AddWithValue("@Name", newVehicleMake.Name);
            //        command.Parameters.AddWithValue("@Abrv", newVehicleMake.Abrv);
            //        command.Parameters.AddWithValue("@DateCreated", newVehicleMake.DateCreated);
            //        command.Parameters.AddWithValue("@DateUpdated", newVehicleMake.DateUpdated);
                    

            //        var result = await command.ExecuteNonQueryAsync();
            //        return result > 0;
            //    }
            //}
        }

        public async Task DeleteVehicleMake(int vehicleId)
        {


            using (var connection = new NpgsqlConnection(Constants.connectionString)) {

                await connection.OpenAsync();

                var sql = $"delete from \"VehicleMake\" where \"Id\" = {vehicleId}";

                await connection.QueryFirstOrDefaultAsync(sql);

            }
            //using (var connection = new NpgsqlConnection(Constants.connectionString))
            //{
            //    await connection.OpenAsync();
            //    using (var command = new NpgsqlCommand("delete from \"VehicleMake\" where \"VehicleMake_id\" = @vehicleId", connection))
            //    {
            //        command.Parameters.AddWithValue("@vehicleId", vehicleId);
            //        var result = await command.ExecuteNonQueryAsync();
            //        return result > 0;

            //    }
            //}

        }

        public async Task UpdateVehicleMake(VehicleMake updatedVehicleMake)
        {
           
            using(var connection = new NpgsqlConnection(Constants.connectionString))
            {
                await connection.OpenAsync();

                var sql = $"update \"VehicleMake\" set \"Name\" = @Name, \"Abrv\" = @Abrv, \"DateUpdated\" = @DateUpdated  where \"Id\" = @Id";

                var vehicleMake = new VehicleMake()
                {
                    Id = updatedVehicleMake.Id,
                    Name = updatedVehicleMake.Name,
                    Abrv = updatedVehicleMake.Abrv,
                    DateUpdated = updatedVehicleMake.DateUpdated,
                };

                await connection.ExecuteAsync(sql, vehicleMake);

                //using (var command = new NpgsqlCommand("update \"VehicleMake\" set \"Name\" = @Name, \"Abrv\" = @Abrv, \"DateUpdated\" = @date  where \"VehicleMake_id\" = @vehicleId", connection))
                //{
                //    command.Parameters.AddWithValue("@vehicleId", updatedVehicleMake.Id);
                //    command.Parameters.AddWithValue("@Name", updatedVehicleMake.Name);
                //    command.Parameters.AddWithValue("@Abrv", updatedVehicleMake.Abrv);
                //    command.Parameters.AddWithValue("@date", updatedVehicleMake.DateUpdated);

                //    var result = await command.ExecuteNonQueryAsync();
                //    return result > 0;
                //}
            }
        }

        public async Task<List<VehicleMake>> SearchVehicleMakes(string searchParam)
        {

           var vehicleMakes= new List<VehicleMake>();

            using (var connection = new NpgsqlConnection (Constants.connectionString))
            {
                //await connection.OpenAsync();

                var sql = $"SELECT * FROM \"VehicleMake\" where \"Name\" ilike @searchParam or \"Abrv\" ilike @searchParam";

                vehicleMakes = (await connection.QueryAsync<VehicleMake>(sql, new { searchParam = "%" + searchParam + "%" })).ToList();
               


                //using (var command  = new NpgsqlCommand("SELECT * FROM \"VehicleMake\" where \"Name\" ilike @searchParam or \"Abrv\" ilike @searchParam", connection))
                //{
                //    command.Parameters.AddWithValue("@searchParam",$"%{searchParam}%" );

                //    using (var reader = await command.ExecuteReaderAsync())
                //    {
                //        while (await reader.ReadAsync())
                //        {
                //            var vehicleMake = new VehicleMake()
                //            {
                //                Id = reader.GetInt32(reader.GetOrdinal("VehicleMake_id")),
                //                Name = reader.GetString(reader.GetOrdinal("Name")),
                //                Abrv = reader.GetString(reader.GetOrdinal("Abrv")),
                                
                //                DateCreated = reader.GetDateTime(reader.GetOrdinal("DateCreated")),
                //                DateUpdated = reader.GetDateTime(reader.GetOrdinal("DateUpdated"))
                //            };

                //            vehicleMakes.Add(vehicleMake);
                //        }
                        
                //    }
                //}

                return vehicleMakes;
            }
        }

        public async Task<VehicleMake> GetVehicleMakeId (int vehicleMakeId)
        {
            

            using(var connection = new NpgsqlConnection(Constants.connectionString))
            {
                //await connection.OpenAsync();


                var sql = $"select * from \"VehicleMake\" where \"Id\" = @Id";
                // var param = new { VehicleMakeId = vehicleMakeId };
                var anonimus = new
                {
                    Id = vehicleMakeId
                };
                var vehicleMake = await connection.QueryFirstOrDefaultAsync<VehicleMake>(sql, anonimus);

               if (vehicleMake == null)
                {
                    return null;
                }
                else
                {
                    return vehicleMake;
                }

              
                //using(var command = new NpgsqlCommand("select * from \"VehicleMake\" where \"VehicleMake_id\" = @vehicleMakeId",connection))
                //{
                //    command.Parameters.AddWithValue("@vehicleMakeId", vehicleMakeId);

                //    using (var reader = await command.ExecuteReaderAsync())
                //    {

                //        while (await reader.ReadAsync())
                //        {                   

                //           var vehicleMake = new VehicleMake()

                //            {
                //                Id = reader.GetInt32(reader.GetOrdinal("VehicleMake_id")),
                //                Name = reader.GetString(reader.GetOrdinal("Name")),
                //                Abrv = reader.GetString(reader.GetOrdinal("Abrv")),
                //                DateCreated = reader.GetDateTime(reader.GetOrdinal("DateCreated")),
                //                DateUpdated = reader.IsDBNull(reader.GetOrdinal("DateUpdated")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("DateUpdated"))
                //           };

                //            return vehicleMake;

                //        } 
                //        return null;


                //    }

                //}

            }

        }

    }
}

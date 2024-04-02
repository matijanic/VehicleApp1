using Dapper;
using Microsoft.VisualBasic;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using VehicleProject.Models;
using Constants = VehicleProject.Util.Constants;


namespace VehicleProject.Repository
{
    public class VehicleModelRepository
    {
        public async Task<List<VehicleModel>> GetVehicleModels()
        {

            using (var connection = new NpgsqlConnection(Constants.connectionString))
            {
                await connection.OpenAsync();

                var sql = "select * from \"VehicleModel\"";

                var vehicleModel = await connection.QueryAsync<VehicleModel>(sql);

                //using (var command = new NpgsqlCommand("select \"VehicleModel_id\",\"Name\",\"Abrv\",\"VehicleMake_id\",\"DateCreated\",\"DateUpdated\" from \"VehicleModel\"", connection))
                //{
                //    using (var reader = await command.ExecuteReaderAsync())
                //    {
                //        while (await reader.ReadAsync())
                //        {
                //            var vehicleModel = new VehicleModel()
                //            {
                //                Id = reader.GetInt32(reader.GetOrdinal("VehicleModel_id")),
                //                Name = reader.GetString(reader.GetOrdinal("Name")),
                //                Abrv = reader.GetString(reader.GetOrdinal("Abrv")),
                //                MakeId = reader.GetInt32(reader.GetOrdinal("VehicleMake_id")),
                //                DateCreated = reader.GetDateTime(reader.GetOrdinal("DateCreated")),
                //                DateUpdated = reader.GetDateTime(reader.GetOrdinal("DateUpdated")),


                //            };

                //            vehicleModels.Add(vehicleModel);
                //        }
                //    }
                //}

                return vehicleModel.ToList();

            }
        }

        public async Task<List<VehicleModel>> GetAllOwnerModels(string firstName, string lastName)
        {
            var modelList = new List<VehicleModel>();

            using (var connection = new NpgsqlConnection(Constants.connectionString))
            {

                var sql = "select vm.\"Name\", vm.\"Abrv\", vm.\"MakeId\", vm.\"DateCreated\"" +
                    "\r\nfrom \"VehicleModel\" vm" +
                    " \r\njoin \"VehicleModelOwner\" vmo on vmo.\"VehicleModel_id\" = vm.\"Id\"" +
                    " \r\njoin \"Owner\" o on o.\"Owner_id\" = vmo.\"Owner_id\" " +
                    "\r\nwhere o.\"FirstName\" = @name and o.\"LastName\" = @lastname";


                var newModels = await connection.QueryAsync<VehicleModel>(sql, new
                {
                    name = firstName,
                    lastname = lastName
                });

                return newModels.ToList();


                //await connection.OpenAsync();
                //using (var command = new NpgsqlCommand("select vm.* \r\nfrom \"Owner\" \r\njoin \"VehicleModelOwner\" vmo on \"Owner\".\"Owner_id\" = vmo.\"Owner_id\" \r\njoin \"VehicleModel\" " +
                //    "vm on vmo.\"VehicleModel_id\" = vm.\"VehicleModel_id\" \r\nwhere \"Owner\".\"FirstName\"=@firstName and \"Owner\".\"LastName\" =@lastName",connection))
                //{
                //    command.Parameters.AddWithValue("@firstName", firstName);
                //    command.Parameters.AddWithValue("@lastName", lastName);

                //    using (var reader = await command.ExecuteReaderAsync())
                //    {
                //       while(await reader.ReadAsync())
                //        {
                //            var vehicleModel = new VehicleModel()
                //            {
                //                Name = reader.GetString(reader.GetOrdinal("Name")),
                //                Abrv = reader.GetString(reader.GetOrdinal("Abrv")),
                //                MakeId = reader.GetInt32(reader.GetOrdinal("VehicleMake_id")),
                //                DateCreated = reader.GetDateTime(reader.GetOrdinal("DateCreated")),
                //                DateUpdated = reader.GetDateTime(reader.GetOrdinal("DateUpdated")),
                //            };

                //            modelList.Add(vehicleModel);
                //        }
                //    }


            }
           

        
           
        }

        public async Task<List<VehicleModel>> SearcVehicleModels(string stringParams)

        {

            using (var connection = new NpgsqlConnection(Constants.connectionString))
            {

                var sql = "select * from \"VehicleModel\" where \"Name\" ILike @stringParams or \"Abrv\" ilike @stringParams";


                var vehicleModelRes = await connection.QueryAsync<VehicleModel>(sql, new { stringParams = $"%{stringParams}%"});
                //await connection.OpenAsync();
                //using (var command = new NpgsqlCommand ("select * from \"VehicleModel\" where \"Name\" ILike @stringParams or \"Abrv\" ilike @stringParams",connection))
                //{
                //    command.Parameters.AddWithValue("@stringParams", $"%{stringParams}%");

                //    using (var reader = await command.ExecuteReaderAsync())
                //    {
                //        while(await reader.ReadAsync())
                //        {
                //            var vehicleModel = new VehicleModel()
                //            {
                //                Id = reader.GetInt32(reader.GetOrdinal("VehicleModel_id")),
                //                Name = reader.GetString(reader.GetOrdinal("Name")),
                //                Abrv = reader.GetString(reader.GetOrdinal("Abrv")),
                //                MakeId = reader.GetInt32(reader.GetOrdinal("VehicleMake_id")),
                //                DateCreated = reader.GetDateTime(reader.GetOrdinal("DateCreated")),
                //                DateUpdated = reader.GetDateTime(reader.GetOrdinal("DateUpdated")),
                //            };

                //            vehicleModelList.Add(vehicleModel);


                //        }
                //    }
                //}
               
                if (vehicleModelRes != null)
                {
                   return vehicleModelRes.ToList();
                }
                else
                {
                    return null;
                }

                

                
            }
        }
        

        public async Task AddVehicleModel(VehicleModel newVehicleModel)
        {
            using (var connection = new NpgsqlConnection(Constants.connectionString))
            {
                //await connection.OpenAsync();

                var sql = "insert into \"VehicleModel\" (\"Name\",\"Abrv\",\"MakeId\",\"DateCreated\",\"DateUpdated\") values (@Name,@Abrv,@MakeId,@DateCreated,@DateUpdated)";

                //var vehicleModel = new VehicleModel
                //{
                //    Id = newVehicleModel.Id,
                //    Name = newVehicleModel.Name,
                //    Abrv = newVehicleModel.Abrv,
                //    MakeId = newVehicleModel.MakeId,
                //    DateCreated = newVehicleModel.DateCreated,
                //    DateUpdated = newVehicleModel.DateUpdated
                //};

                await connection.ExecuteAsync(sql, newVehicleModel);

                //using (var command = new NpgsqlCommand("insert into \"VehicleModel\" (\"Name\",\"Abrv\",\"VehicleMake_id\",\"DateCreated\",\"DateUpdated\") values (@Name,@Abrv,@MakeId,@DateCreated,@DateUpdated)", connection))
                //{
                //    command.Parameters.AddWithValue("@Name", newVehicleModel.Name);
                //    command.Parameters.AddWithValue("@Abrv", newVehicleModel.Abrv);
                //    command.Parameters.AddWithValue("@MakeId", newVehicleModel.MakeId);  
                //    command.Parameters.AddWithValue("@DateCreated", newVehicleModel.DateCreated);
                //    command.Parameters.AddWithValue("@DateUpdated", newVehicleModel.DateUpdated);


                //    await command.ExecuteNonQueryAsync();
                //}
            }
        }

        public async Task DeleteVehicleModel(int id)
        {
            using (var connection = new NpgsqlConnection(Constants.connectionString))
            {
                var sql = "DELETE FROM \"VehicleModel\" WHERE \"Id\" = @modelId";


                await connection.ExecuteAsync(sql, new { modelId = id });



                //await connection.OpenAsync();
                //using (var command = new NpgsqlCommand("DELETE FROM \"VehicleModel\" WHERE \"VehicleModel_id\" = @vehicleModelId",connection))
                //{
                //    command.Parameters.AddWithValue("@vehicleModelId", vehicleModelId);
                //    await command.ExecuteNonQueryAsync();
                //}
            }
        }

        public async Task UpdateVehicleModel(VehicleModel newVehicleModel)
        {
            using (var connection = new NpgsqlConnection(Constants.connectionString))
            {

                var sql = "update \"VehicleModel\" set \"Name\" = @name,\"Abrv\" = @abrv,\"MakeId\" = @makeId, \"DateUpdated\"= @date where \"Id\" = @vehicleModelId";

                await connection.ExecuteAsync(sql, new
                
                {
                    vehicleModelId = newVehicleModel.Id,
                    name = newVehicleModel.Name,
                    abrv = newVehicleModel.Abrv,
                    makeId = newVehicleModel.MakeId,
                    date = newVehicleModel.DateUpdated

                });

                //await connection.OpenAsync();
                //using ( var command = new NpgsqlCommand("update \"VehicleModel\" set \"Name\" = @name,\"Abrv\" = @abrv,\"VehicleMake_id\" = @makeId, \"DateUpdated\"= @date where \"VehicleModel_id\" = @vehicleModelId", connection))
                //{
                //    command.Parameters.AddWithValue("@vehicleModelId", newVehicleModel.Id);
                //    command.Parameters.AddWithValue("@name", newVehicleModel.Name);
                //    command.Parameters.AddWithValue("@abrv", newVehicleModel.Abrv);
                //    command.Parameters.AddWithValue("@makeId", newVehicleModel.MakeId);
                //    command.Parameters.AddWithValue("@date", newVehicleModel.DateUpdated);

                //    await command.ExecuteNonQueryAsync();
                //}
            }
        }

        public async Task<VehicleModel> GetModelById(int vehicleModelId)
        {
            using (var connection = new NpgsqlConnection(Constants.connectionString))
            {

                var sql = "Select * from \"VehicleModel\" where \"Id\" = @vehicleModelId";

                var vehicleModel = await connection.QueryFirstOrDefaultAsync<VehicleModel>(sql, new { vehicleModelId = vehicleModelId});

               if (vehicleModel == null)
                {
                    return null;
                }

               return vehicleModel;




                    
    
                //await connection.OpenAsync();

                //using (var command =  new NpgsqlCommand("select * from \"VehicleModel\" where \"VehicleModel_id\" = @vehicleModelId",connection))
                //{
                //    command.Parameters.AddWithValue("@vehicleModelId", vehicleModelId);

                //    using (var reader = command.ExecuteReader())
                //    {
                //        if(await reader.ReadAsync())
                //        {
                //            var vehicleModel = new VehicleModel()
                //            {
                //                Id = reader.GetInt32(reader.GetOrdinal("VehicleModel_id")),
                //                Name = reader.GetString(reader.GetOrdinal("Name")),
                //                Abrv = reader.GetString(reader.GetOrdinal("Abrv")),
                //                MakeId = reader.GetInt32(reader.GetOrdinal("VehicleMake_id")),
                //                DateCreated = reader.GetDateTime(reader.GetOrdinal("DateCreated")),
                //                DateUpdated = reader.IsDBNull(reader.GetOrdinal("DateUpdated")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("DateUpdated"))

                //            };

                //            return vehicleModel;
                //        }
                //        else
                //        {
                //            return null;
                //        }
                //    }
                //}
            }
        }
    }
}
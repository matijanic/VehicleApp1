using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.VisualBasic.FileIO;
using Npgsql;
using VehicleProject.Models;
using VehicleProject.Util;

namespace VehicleProject.Repository
{
    public class ModelOwnerRepository
    {
        //public VehicleContainer vehicleContainer { get; set; }


        public async Task<bool> AddVehicleMakeOwner(VehicleModelOwner newVehicleModelOwner)
        {

            using (var connection = new NpgsqlConnection(Constants.connectionString))
            {

                var sqlFunction = "Select add_vehiclemodelowner(@VehicleModel_id, @Owner_id, @dateCreated, @dateUpdated, @price, @isActive)";

                var result = await connection.QueryFirstOrDefaultAsync<bool>(sqlFunction, new
                {

                    VehicleModel_id = newVehicleModelOwner.VehicleModel_id,
                    Owner_id = newVehicleModelOwner.Owner_id,
                    dateCreated = newVehicleModelOwner.DateCreated,
                    dateUpdated = newVehicleModelOwner.DateUpdated,
                    price = newVehicleModelOwner.Price,
                    isActive = newVehicleModelOwner.IsActive,



                });

                if (result)
                {
                    var sqlNew = "SELECT vk.\"Name\" AS \"MakeName\", vm.\"Name\" AS \"ModelName\", o.\"FirstName\", o.\"LastName\", vmo.\"Price\", vmo.\"IsActive\"\r\n" +
                        "FROM \"VehicleModelOwner\" vmo\r\n" +
                        "JOIN \"VehicleModel\" vm ON vm.\"Id\" = vmo.\"VehicleModel_id\"\r\n" +
                        "JOIN \"VehicleMake\" vk ON vk.\"Id\" = vm.\"MakeId\"\r\n" +
                        "JOIN \"Owner\" o ON o.\"Owner_id\" = vmo.\"Owner_id\" " +
                        "where vmo.\"VehicleModel_id\" = @vm_id and vmo.\"Owner_id\" = @ow_id;";

                    var vehicleModelRab = await connection.QueryFirstOrDefaultAsync<VehicleOwnerRabbit>(sqlNew, new
                    {
                        vm_id = newVehicleModelOwner.VehicleModel_id,
                        ow_id = newVehicleModelOwner.Owner_id,

                    });


                    var vehicleContainer = new VehicleContainer
                    {
                        actionType = ActionType.Insert,
                        vehicleModelOwner = vehicleModelRab
                    };

                    Sender.SendMessage(vehicleContainer);
                }

                return result;


                //await connection.OpenAsync();

                //using (var command = new NpgsqlCommand("Select add_vehiclemodelowner(@VehicleModel_id, @Owner_id, @dateCreated, @dateUpdated)", connection))
                //{


                //    {
                //        command.Parameters.AddWithValue("@VehicleModel_id", newVehicleModelOwner.VehicleModel_id);
                //        command.Parameters.AddWithValue("@Owner_id", newVehicleModelOwner.Owner_id);
                //        command.Parameters.AddWithValue("@dateCreated", newVehicleModelOwner.DateCreated);
                //        command.Parameters.AddWithValue("@dateUpdated", newVehicleModelOwner.DateUpdated);

                //        var result = await command.ExecuteScalarAsync();
                //        return (bool)result;


                //    }
                //}
            }
        }

        public async Task<VehicleModelOwner> GetVehicleModelOwnerById(int vehicleModelOwnerId)
        {
            using (var connection = new NpgsqlConnection(Constants.connectionString))
            {
                await connection.OpenAsync();

                using (var command = new NpgsqlCommand("select * from \"VehicleModelOwner\" vmo \r\nwhere vmo.\"VehicleModel_id\" = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", vehicleModelOwnerId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (await reader.ReadAsync())
                        {
                            var vehicleModelOwner = new VehicleModelOwner()
                            {
                                VehicleModel_id = reader.GetInt32(reader.GetOrdinal("VehicleModel_id")),
                                Owner_id = reader.GetInt32(reader.GetOrdinal("Owner_id")),
                                DateCreated = reader.GetDateTime(reader.GetOrdinal("DateCreated")),
                                DateUpdated = reader.GetDateTime(reader.GetOrdinal("DateUpdated")),
                                Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                                IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"))

                            };

                            return vehicleModelOwner;

                        }
                        else
                        {
                            return null;
                        }



                    }
                }
            }
        }

        public async Task<VehicleModelOwner> GetVehicleModelOwnerByOwnerId(int ownerId)
        {
            using (var connection = new NpgsqlConnection(Constants.connectionString))
            {
                var sql = "select * from \"VehicleModelOwner\" vmo where vmo.\"Owner_id\" = @ownerId";

                var vehicleOwner = await connection.QueryFirstOrDefaultAsync<VehicleModelOwner>(sql, new
                {
                    ownerId = ownerId
                });

                if (vehicleOwner == null)
                {
                    return null;
                }

                return vehicleOwner;
            }
        }

        public async Task<List<VehicleModelOwner>> GetAllVehicleModelOwner()
        {
            var listAll = new List<VehicleModelOwner>();
            using (var connection = new NpgsqlConnection(Constants.connectionString))
            {
                var sql = "select * from \"VehicleModelOwner\" vmo;";

                var list = await connection.QueryAsync<VehicleModelOwner>(sql);
                listAll = list.ToList();
            }

            return listAll;
        }


        public async Task DeleteVehicelModelOwner(int vehicleModelId, int vehicleOwnerId)
        {
            using (var connection = new NpgsqlConnection(Constants.connectionString))
            {
                await connection.OpenAsync();


                var sqlFirstLastName = "SELECT vk.\"Name\" AS \"MakeName\",\r\n    vm.\"Name\" AS \"ModelName\",\r\n    o.\"FirstName\",\r\n    o.\"LastName\",\r\n    vmo.\"Price\",\r\n    vmo.\"IsActive\"\r\n     " +
                           "FROM \"VehicleModelOwner\" vmo\r\n     " +
                           "JOIN \"VehicleModel\" vm ON vm.\"Id\" = vmo.\"VehicleModel_id\"\r\n     " +
                           "JOIN \"VehicleMake\" vk ON vk.\"Id\" = vm.\"MakeId\"\r\n     " +
                           "JOIN \"Owner\" o ON o.\"Owner_id\" = vmo.\"Owner_id\"\r\n     " +
                           "where vmo.\"VehicleModel_id\" = @vehicleId " +
                           "and vmo.\"Owner_id\" = @ownerId;";

                var vehicleModelRab = await connection.QueryFirstOrDefaultAsync<VehicleOwnerRabbit>(sqlFirstLastName, new
                {
                    vehicleId = vehicleModelId,
                    ownerId = vehicleOwnerId

                });

                var vehicleContainer = new VehicleContainer
                {
                    actionType = ActionType.Delete,
                    vehicleModelOwner = vehicleModelRab
                };

                Sender.SendMessage(vehicleContainer);



                if (vehicleModelRab != null)
                {

                    using (var command = new NpgsqlCommand("delete from \"VehicleModelOwner\" " +
                    "where \"VehicleModelOwner\".\"VehicleModel_id\" = @vehicleId " +
                    "and \"VehicleModelOwner\".\"Owner_id\" = @ownerId;", connection))

                    {
                        command.Parameters.AddWithValue("vehicleId", vehicleModelId);
                        command.Parameters.AddWithValue("@ownerId", vehicleOwnerId);
                        var affectedRow = await command.ExecuteNonQueryAsync();

                    }

                }


            }
        }


        public async Task UpdateVehicleModelOwner(VehicleModelOwner modelOwner)
        {
            using (var connection = new NpgsqlConnection(Constants.connectionString))
            {
                var sql = "update \"VehicleModelOwner\"  set \"VehicleModel_id\" = @vehicleId,  \"Owner_id\" = @ownerId, \"DateUpdated\" = @dateUpdated, \"Price\" = @price, \"IsActive\" = @isActive where \"VehicleModel_id\" = @vehicleId;";

                var anonimObj = new
                {
                    vehicleId = modelOwner.VehicleModel_id,
                    ownerId= modelOwner.Owner_id,
                    dateUpdated = modelOwner.DateUpdated,
                    price = modelOwner.Price,
                    isActive = modelOwner.IsActive
                };

                var affecdedRow = await connection.ExecuteAsync(sql, anonimObj);

                if(affecdedRow > 0)
                {

                    var sqlUpdate = "SELECT vk.\"Name\" AS \"MakeName\",\r\n    vm.\"Name\" AS \"ModelName\",\r\n    o.\"FirstName\",\r\n    o.\"LastName\",\r\n    vmo.\"Price\",\r\n    vmo.\"IsActive\"\r\n     " +
                              "FROM \"VehicleModelOwner\" vmo\r\n     " +
                              "JOIN \"VehicleModel\" vm ON vm.\"Id\" = vmo.\"VehicleModel_id\"\r\n     " +
                              "JOIN \"VehicleMake\" vk ON vk.\"Id\" = vm.\"MakeId\"\r\n     " +
                              "JOIN \"Owner\" o ON o.\"Owner_id\" = vmo.\"Owner_id\"\r\n     " +
                              "where vmo.\"VehicleModel_id\" = @vehicleId " +
                              "and vmo.\"Owner_id\" = @ownerId;";

                    var modelUpdateRabbit = await connection.QueryFirstOrDefaultAsync<VehicleOwnerRabbit>(sqlUpdate, new
                    {
                        vehicleId = modelOwner.VehicleModel_id,
                        ownerId = modelOwner.Owner_id
                    });

                    var modelContainerUpdate = new VehicleContainer
                    {
                        actionType = ActionType.Update,
                        vehicleModelOwner = modelUpdateRabbit
                    };

                    Sender.SendMessage(modelContainerUpdate);

                }


            }
        }
        public async Task<VehicleModelOwner> FindModel(int vehicleId, int ownerId)
        {
            using (var connection = new NpgsqlConnection(Constants.connectionString))
            {
                var sql = "select * from \"VehicleModelOwner\" vmo where vmo.\"VehicleModel_id\" =@vehicle_Id and vmo.\"Owner_id\" =@owner_Id;";

                var existingModel = await connection.QueryFirstOrDefaultAsync<VehicleModelOwner>(sql, new
                {
                    vehicle_Id = vehicleId,
                    owner_Id = ownerId

                });

                if (existingModel == null)
                {
                    return null;
                }

                return existingModel;
            }
        }


    }
}


   
       
    


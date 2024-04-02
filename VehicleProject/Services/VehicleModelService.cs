using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql.Logging;
using VehicleProject.Models;
using VehicleProject.Repository;
using VehicleProject.Validation;

namespace VehicleProject.Services
{
    public class VehicleModelService
    {
        private VehicleModelRepository _vehicleModelRepository = new VehicleModelRepository();

        public async Task ListAllVehicleModels()
        {
            var vehicleModels = new List<VehicleModel>();
            vehicleModels = await _vehicleModelRepository.GetVehicleModels();

            Console.WriteLine();
            Console.WriteLine("Vehicle Models List");
            Console.WriteLine("--------------------------");
            foreach (var model in vehicleModels)
            {
                Console.WriteLine("Name: " + model.Name + " Abrv: " + model.Abrv + " Make Id: " + model.MakeId + " DateCreated: " + model.DateCreated + " DateUpdated: " + model.DateUpdated);
            }
        }

        public async Task DeleteVehicleModel()
        {
            Console.WriteLine("Enter Vehicle Model Id for delete: ");
            int inputModelId;

            if (!int.TryParse(Console.ReadLine(), out inputModelId))
            {
                Console.Write("Invalid input!");
                return;

            }

            var existingModel = await _vehicleModelRepository.GetModelById(inputModelId);
            if (existingModel == null)
            {
                Console.WriteLine("Id not found!");
                return;
            }

            await _vehicleModelRepository.DeleteVehicleModel(inputModelId);

        }


        public async Task GetVehicleModelsByOwner()
        {
            Console.WriteLine("Enter owner FirstName");
            string firstName = Console.ReadLine().Trim();

            firstName = StringValidate.CheckStringName(firstName);

            Console.WriteLine("Enter owner LastName");
            string lastName = Console.ReadLine().Trim();  

            lastName = StringValidate.CheckStringName(lastName);    

            var newList = await _vehicleModelRepository.GetAllOwnerModels(firstName, lastName);

            if (!newList.Any())
            {
                Console.WriteLine("Not found");
                return;
            }
            Console.WriteLine("All models by: {0} {1}",  firstName,lastName);
            Console.WriteLine("-----------------");
            foreach(var model in newList)
            {
                Console.WriteLine("Model Name: " + model.Name + " Abrv: " + model.Abrv + " Date Created: " + model.DateCreated + " Date Uddated: " + model.DateUpdated);
                
            }


        }
        

        public async Task SearchVehicleModels()
        {
            Console.WriteLine("Enter Name or Abrv for search");
            string searchPara = Console.ReadLine().Trim();

            var searchExistingModel = await _vehicleModelRepository.SearcVehicleModels(searchPara);


            if (!searchPara.Any())
            {
                Console.WriteLine("Not found!");
                return;
            }

            Console.WriteLine("Search Results");
            Console.WriteLine("-------------------");
            foreach (var vehicle in searchExistingModel)
            {
                Console.WriteLine("Name: " + vehicle.Name + " Abrv: " + vehicle.Abrv + "Make_ID: " + vehicle.MakeId + " DateCreated: " + vehicle.DateCreated + " DateUpdated: " + vehicle.DateUpdated);
            }


        }
        public async Task AddNewVehicleModel()
        {
            Console.WriteLine();
            Console.WriteLine("Enter new Vehicle Model:");
            Console.WriteLine("----------------------");

            Console.Write("Name: ");
            string nameModel = Console.ReadLine();
            nameModel = StringValidate.CheckStringName(nameModel);

            Console.Write("Abrv: ");
            string abrvModel = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(abrvModel))
            {
                abrvModel = nameModel.Substring(0, 3).ToLower();
            }


            abrvModel = StringValidate.CheckStringName(abrvModel);


           

            Console.Write("Enter Make Id:");
            int makeId;

            while (!int.TryParse(Console.ReadLine(), out makeId))
            {
                Console.WriteLine("Invalid input! Please enter a valid number: ");
            }

            var newVehicleModel = new VehicleModel()
            {
                Name = nameModel,
                Abrv = abrvModel,
                MakeId = makeId,
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.MinValue

            };

            await _vehicleModelRepository.AddVehicleModel(newVehicleModel);
        }

        public async Task UpdateVehicleModel()
        {
            Console.WriteLine("Enter Vehicle Model id for update");
            int inputIdModel;

            if (!int.TryParse(Console.ReadLine(), out inputIdModel))
            {
                Console.WriteLine("Invalid input! Please insert a valid number");
                return;
            }

            var existingModel = await _vehicleModelRepository.GetModelById(inputIdModel);
            if (existingModel == null)
            {
                Console.WriteLine("Id not Found");
                return;
            }

            Console.WriteLine("Enter new name: ");
            string modelName = Console.ReadLine().Trim();

            modelName = StringValidate.CheckStringName(modelName);

            Console.WriteLine("Enter new abrv: ");
            string modelAbrv = Console.ReadLine().Trim();

            modelAbrv = StringValidate.CheckStringName(modelAbrv); 

            Console.WriteLine("Enter new Make Id");
            int newMakeId;

            while (!int.TryParse(Console.ReadLine(), out newMakeId))
            {
                Console.WriteLine("Invalid input! Please enter a valid number");

            }

            var newVehicleModel = new VehicleModel()
            {
                Id = inputIdModel,
                Name = modelName,
                Abrv = modelAbrv,
                MakeId = newMakeId,
                DateUpdated = DateTime.Now
            };

            await _vehicleModelRepository.UpdateVehicleModel(newVehicleModel);
        }


        public async Task GetModelById()
        {
            Console.WriteLine("Enter Id for Vehicle Model");
            int idModel;

            if(!int.TryParse(Console.ReadLine(), out idModel))
            {
                Console.WriteLine("Invalid input! Please enter a valid number");
            }

            var existingModel  = await _vehicleModelRepository.GetModelById(idModel);
            if(existingModel == null)
            {
                Console.WriteLine("Model not found");
                return;
            }

            Console.WriteLine("-----------------------");
            Console.WriteLine("Name: " + existingModel.Name);
            Console.WriteLine("Abrv: " + existingModel.Abrv);
            Console.WriteLine("MakeId: " + existingModel.MakeId);
            Console.WriteLine("DateCreated: " + existingModel.DateCreated);
            Console.WriteLine("DateUpdated: " + existingModel.DateUpdated);

        }

    }
}

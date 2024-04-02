using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleProject.Models;
using VehicleProject.Repository;

namespace VehicleProject.Services
{
    public class ModelOwnerService
    {

        private VehicleModelService _vehicleModelService = new VehicleModelService();
        private OwnerService _ownerService = new OwnerService();
        private ModelOwnerRepository _modelOwnerRepository = new ModelOwnerRepository();

        public async Task AddNewModelForOwner()
        {
            Console.WriteLine("All models");
            await _vehicleModelService.ListAllVehicleModels();
            Console.WriteLine();

            Console.WriteLine("All owners");
            await _ownerService.GetAllOwner();



            Console.WriteLine("---------------");
            Console.WriteLine("Enter model id for input");
            int modelInput;

            if (!int.TryParse(Console.ReadLine(), out modelInput))
            {
                Console.WriteLine("Invalid input");
                return;
            }

            Console.WriteLine("Enter owner id for input");
            int ownerInput;

            if (!int.TryParse(Console.ReadLine(), out ownerInput))
            {
                Console.WriteLine("Invalid input");
                return;
            }

            Console.WriteLine("Enter price:");
            decimal priceInput;

            while(!decimal.TryParse(Console.ReadLine(), out priceInput))
            {
                Console.WriteLine("Weong input: Enter again: ");
            }

            Console.WriteLine("Chose: ");
            Console.WriteLine("1. IsValid: ");
            Console.WriteLine("2. NonValid: ");

            int inputValid;

            while(!int.TryParse(Console.ReadLine(), out inputValid) || (inputValid != 1 && inputValid != 2))
            {
                Console.WriteLine("Enter valid number or 1- IsActive, 2- NonActive");
            }

            bool isValid = false;

            switch (inputValid)
            {
                case 1: isValid = true; break;
                case 2: isValid = false; break;
            }
            
            

            var vehicleModelOwner = new VehicleModelOwner()
            {
                VehicleModel_id = modelInput,
                Owner_id = ownerInput,
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.MinValue,
                Price= priceInput,
                IsActive = isValid
                


            };

            var result=  await _modelOwnerRepository.AddVehicleMakeOwner(vehicleModelOwner);
            if (result)
            {
                Console.WriteLine("Success added!");
            }
            else
            {
                Console.WriteLine("Not added! Check Model, Owner or existing record!");
            }
        }

        public async Task DeleteVehicleModelOwner()
        {
            Console.WriteLine("List all");
            Console.WriteLine("--------------");

            var vehicleModelOwnerList = await _modelOwnerRepository.GetAllVehicleModelOwner();

            foreach(var modelOwner in vehicleModelOwnerList)
            {
                Console.WriteLine($"VehicleModel_id: {modelOwner.VehicleModel_id}, Owner_id: {modelOwner.Owner_id}, Price: {modelOwner.Price}, IsActive: {modelOwner.IsActive}");
            }
            Console.WriteLine();


            

            Console.WriteLine("Enter VehicleModel_id for delete: ");
            int inputDelete;

            

            while(!int.TryParse(Console.ReadLine(), out inputDelete))
            {
                Console.WriteLine("Wrong input! Enter valid id:");
            }

            var existingVehicle = await _modelOwnerRepository.GetVehicleModelOwnerById(inputDelete);
            
           
            
            if (existingVehicle == null)
            {
                Console.WriteLine("Id not found!");
                return;
            }
            else
            {
                Console.WriteLine("Enter Owner_id for delete: ");
                int inputOwnerid;

                while (!int.TryParse(Console.ReadLine(), out inputOwnerid))
                {
                    Console.WriteLine("Wrong input! Enter valid id:");
                }

                var existingVehicleOwnerId = await _modelOwnerRepository.GetVehicleModelOwnerByOwnerId(inputOwnerid);


                if (existingVehicleOwnerId == null)
                {
                    Console.WriteLine("Owner_id not found");
                    return;
                }
                else if (existingVehicle.Owner_id == inputOwnerid)
                {
                    await _modelOwnerRepository.DeleteVehicelModelOwner(inputDelete, inputOwnerid);
                    Console.WriteLine("Success deleted!");
                }

                else
                {
                    Console.WriteLine("VehicleId and Owner_id not from same record!");
                    return;
                }

                
            }

           
        }

        public async Task UpdateVehicleModelOwner()
        {

            Console.WriteLine("List all");
            Console.WriteLine("--------------");

            var vehicleModelOwnerList = await _modelOwnerRepository.GetAllVehicleModelOwner();

            foreach (var modelOwner in vehicleModelOwnerList)
            {
                Console.WriteLine($"VehicleModel_id: {modelOwner.VehicleModel_id}, Owner_id: {modelOwner.Owner_id}, DateUpdated: {modelOwner.DateUpdated}, Price: {modelOwner.Price}, IsActive: {modelOwner.IsActive}");
            }
            Console.WriteLine();
            Console.WriteLine("Enter VehicleModel_id for update: ");
            int inputUpdate;

            while(!int.TryParse(Console.ReadLine(),out inputUpdate))
            {
                Console.WriteLine("Wrong input! Enter valid number");
            }

            Console.WriteLine("Enter Owner_id for update");
            int inputOwnerUpdate;

            while (!int.TryParse(Console.ReadLine(), out inputOwnerUpdate))
            {
                Console.WriteLine("Wrong input! Enter valid number");
            }

            var existingModel = await _modelOwnerRepository.FindModel(inputUpdate, inputOwnerUpdate);

            if(existingModel == null)
            {
                Console.WriteLine("Not Found");
                return;

            }

     

            Console.WriteLine($"Enter new price: ");
            decimal inputPrice;

            while(!decimal.TryParse(Console.ReadLine(), out inputPrice))
            {
                Console.WriteLine("Wrong input! Enter valid price:");
            }

            Console.WriteLine("Enter 1: IsActive, 2. NonActive");
            int insertIsActive;

            while(!int.TryParse(Console.ReadLine(), out insertIsActive))
            {
                Console.WriteLine("Enter valid number or 1- IsActive, 2- NonActive");
            }

            var boolIsActive = false;

            switch (insertIsActive)
            {
                case 1: boolIsActive = true;
                    break;
                case 2: boolIsActive = false;
                    break;
            }


            var newvehicleModelUpdate = new VehicleModelOwner
            {
                VehicleModel_id = inputUpdate,
                Owner_id = inputOwnerUpdate,
                DateUpdated = DateTime.Now,
                Price = inputPrice,
                IsActive = boolIsActive,
            };

            await _modelOwnerRepository.UpdateVehicleModelOwner(newvehicleModelUpdate);

            Console.WriteLine("Succes updated");
        }
    


        }
    }


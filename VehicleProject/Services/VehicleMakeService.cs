using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleProject.Models;
using VehicleProject.Repository;
using VehicleProject.Validation;

namespace VehicleProject.Services
{
    public class VehicleMakeService
    {
        private VehicleMakeRepository _vehicleMakeRepo = new VehicleMakeRepository();

        public async Task ListAllVehicleMakes()
        {

            var vehicleMakes = await _vehicleMakeRepo.GetVehicleMakes();



            Console.WriteLine("Vehicle Makes List");
            Console.WriteLine("--------------------------");

            foreach (var vehicle in vehicleMakes)
            {
                Console.WriteLine("Name: " + vehicle.Name + " Abrv: " + vehicle.Abrv + " DateCreated: " + vehicle.DateCreated + " DateUpdated: " + vehicle.DateUpdated);


            }
        }

        public async Task SearchVehicleMakes()
        {
            Console.WriteLine("Enter Name or Abrv for search: ");
            string searchParam = Console.ReadLine();

            var vehicleMakesExisting = await _vehicleMakeRepo.SearchVehicleMakes(searchParam);
            if(!vehicleMakesExisting.Any())
            {
                Console.WriteLine("Not Found!");
                return;
            }

            Console.WriteLine("Search Results");
            Console.WriteLine("-------------------");

            foreach(var vehicle in vehicleMakesExisting)
            {
                Console.WriteLine("Name: " + vehicle.Name + " Abrv: " + vehicle.Abrv + " DateCreated: " + vehicle.DateCreated + " DateUpdated: " + vehicle.DateUpdated);
            }

        }

        public async Task DeleteVehicleMake()
        {
            Console.WriteLine("Enter Vehicle Make id for delete: ");
            int inputMakeId;

            if (!int.TryParse(Console.ReadLine(), out inputMakeId))
            {
                Console.WriteLine("Invalid input!");
                return;
            }

            var existingVehicleMake = await _vehicleMakeRepo.GetVehicleMakeId(inputMakeId);

            if (existingVehicleMake == null)
            {
                Console.WriteLine("Id not found");
                return;
            }

            await _vehicleMakeRepo.DeleteVehicleMake(inputMakeId);

        }

        public async Task GetVehicleMakeById()
        {
            Console.WriteLine("Search Vehicle Make By Id");
            int searchId;

            if (!int.TryParse(Console.ReadLine(), out searchId))
            {
                Console.WriteLine("Id not Found");
                return;
            }

            var makeById = await _vehicleMakeRepo.GetVehicleMakeId(searchId);

            if (makeById == null)
            {
                Console.WriteLine("Id not found");
                return;
            }

            Console.WriteLine("--------------------------");
            Console.WriteLine("Name: " + makeById.Name);
            Console.WriteLine("Abrv: " + makeById.Abrv);
            Console.WriteLine("DateCreated: " + makeById.DateCreated);
            Console.WriteLine("DateUpdated: " + makeById.DateUpdated);
        }

        public async Task AddNewVehicleMake()
        {

            Console.WriteLine();
            Console.WriteLine("Enter new Vehicle Make:");
            Console.WriteLine("-------------------");

            Console.Write("Name : ");
            string nameMake = Console.ReadLine().Trim();

            nameMake = StringValidate.CheckStringName(nameMake);


            Console.Write("Abrv : ");
            string adrvmake = Console.ReadLine().Trim();

            

            if (string.IsNullOrWhiteSpace(adrvmake))
            {
                adrvmake = nameMake.Substring(0, 3).ToLower();
                
            }

            adrvmake= StringValidate.CheckStringName(adrvmake);
            

            var newCar = new VehicleMake
            {
                Name = nameMake,
                Abrv = adrvmake,
                DateCreated = DateTime.Now,
                DateUpdated=DateTime.MinValue
                

            };

            await _vehicleMakeRepo.AddVehicleMake(newCar);

        }

        public async Task UpdateVehicleMake()
        {
            Console.WriteLine("Enter Vehicle Make Id for update:");
            int inputId;

            if (!int.TryParse(Console.ReadLine(), out inputId))
            {
                Console.WriteLine("Invalid input. Please enter a valid number");
                return;
            }


            var existingVehicleMake = await _vehicleMakeRepo.GetVehicleMakeId(inputId);


            if (existingVehicleMake == null)
            {
                Console.WriteLine("Id not Found");
                return;
            }

            Console.WriteLine("Enter new Name");
            string newName = Console.ReadLine().Trim();

            newName = StringValidate.CheckStringName(newName);



            Console.WriteLine("Enter new abrv");
            string newAbrv = Console.ReadLine();

            if(string.IsNullOrWhiteSpace(newAbrv))
            {
                newAbrv = newName.Substring(0, 3);
            }

            newAbrv = StringValidate.CheckStringName(newAbrv);

            var updatedVehicleMake = new VehicleMake
            {
                Id = inputId,
                Name = newName,
                Abrv = newAbrv,
                DateUpdated = DateTime.Now

            };

            await _vehicleMakeRepo.UpdateVehicleMake(updatedVehicleMake);

        }

    }

}
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
    public class OwnerService
    {

        private OwnerRepository _ownerRepository = new OwnerRepository();
        
        
        


        public async Task GetAllOwner()
        {
            var ownerLists = await _ownerRepository.GetAllOwner();
            Console.WriteLine("---------------------");
            foreach (var owner in ownerLists)
            {
                Console.WriteLine("Owner: {0}, {1}, {2}, {3}, {4}, {5}", owner.Owner_id, owner.FirstName, owner.LastName, owner.Address, owner.DateCreated, owner.DateUpdated);
                
            } 
            
        }

        

        public async Task AddNewOwner()
        {


            Console.WriteLine("Enter first Name: ");
            string firstName = Console.ReadLine().Trim();
            firstName = StringValidate.CheckStringName(firstName);
            

            Console.WriteLine("Enter last Name: ");
            string lastName = Console.ReadLine().Trim();

            lastName = StringValidate.CheckStringName(lastName);

            Console.WriteLine("Enter Address: ");
            string address = Console.ReadLine();

            address = StringValidate.CheckStringName(address);

            var newOwner = new Owner()
            {
                FirstName = firstName,
                LastName = lastName,
                Address = address,
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.MinValue


            };
            //StringValidate.CheckOwnerName(newOwner);

            await _ownerRepository.AddNewOwner(newOwner);
        }
    }
}

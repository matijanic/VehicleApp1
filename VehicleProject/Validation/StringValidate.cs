using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using VehicleProject.Models;

namespace VehicleProject.Validation
{
    public static class StringValidate
    {

        public static string CheckStringName(string stringName)
        {


            if (char.IsUpper(stringName[0]) && stringName.Substring(1).All(char.IsLower))
            {
                return stringName;
            }

            else
            {
                var newWord = stringName.Substring(0,1).ToUpper() + stringName.Substring(1).ToLower();
                return newWord;
            }

            

            //(char.IsUpper(stringName[0]) && stringName.Substring(1).All(char.IsLower)) 

            //   return stringName;

            //   if 



        }



        // Jure 
        //public static void CheckOwnerName(Owner owner)
        //{

        //    string newWord = owner.FirstName.Substring(0, 1).ToUpper() + owner.FirstName.Substring(1).ToLower();
        //    owner.FirstName = newWord;
        //}
    }
}

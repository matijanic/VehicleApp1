using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleProject.Models;

namespace VehicleProject.Util
{
    public class Response<T>
    {
        public Response(List<T> items)
        {
            Items = items;
            Count = items.Count();
        }
        public Response(string[] errors)
        {
            Errors = errors;
        }
        public List<T> Items{ get; set; }
        public int Count { get; set; }

        public string[] Errors { get; set; }

        public static Response<T> Success(List<T> items)
        {
            return new Response<T>(items);
        }
        public static Response<T> Error(string[] errors)
        {
            return new Response<T>(errors);
        }
    }
    public class Auto
    {
        public int Motor { get; set; }
    }
    public class Vozilo : Auto 
    {
        public int Id { get; set; }
    }

    public class Jurilica : Auto
    {
        public int Id { get; set; }

        public static Response<T> Populate<T>(T items) where T : Auto
        {
            items.Motor = 0;
            if(items.Motor == 0)
            {
            return Response<T>.Error(new string[] { "error1", "error2" });

            
            }
            else
            {
                return Response<T>.Success(new List<T> { });

            }
        }
        public static void Zovi()
        {
            var res = Populate(new Vozilo { Id = 1 });
            if (res.Errors.Any())
            {

            }
            else
            {
                foreach (var item in res.Items)
                {
                    Console.WriteLine(item.Id + item.Motor);
                }
            }
        }
    }
    
}

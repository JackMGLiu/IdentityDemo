using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApp1.Models;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new List<UserPermission>
            {
                new UserPermission { Url="/", UserName="gsw"},
                new UserPermission { Url="/home/contact", UserName="gsw"},
                new UserPermission { Url="/home/about", UserName="aaa"},
                new UserPermission { Url="/", UserName="aaa"} 
            };

            var data = list.GroupBy(g => g.Url).Where(m=>m.Key=="/");

            Console.WriteLine(JsonConvert.SerializeObject(data));
            Console.ReadKey();
        }
    }
}

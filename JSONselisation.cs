using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;

namespace JsonSerialization
{
    class Pet
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public double Age { get; set; }
    }

    class Person
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public string StateOfOrigin { get; set; }

        public List<Pet> Pets { get; set; }
    }


    class serilizeTask<T>
    {

        public serilizeTask()
        {

        }
        public static async Task SerializeToFile()
        {
            var pets = new List<Pet>
            {
                new Pet { Type = "Cat", Name = "MooMoo", Age = 3.4 },
                new Pet { Type = "Squirrel", Name = "Sandy", Age = 7 }
            };
            var person = new Person
            {
                Name = "John",
                Age = 34,
                StateOfOrigin = "England",
                Pets = pets
            };

            var fileName = "Person.json";

            var stream = File.Create(fileName);
            await JsonSerializer.SerializeAsync(stream, person);
           // await stream.DisposeAsync();
            Console.WriteLine(File.ReadAllText(fileName));

        }

        public static async Task<string> GetJson(object obj)
        {
            string json = string.Empty;
            using (var stream = new MemoryStream())
            {
                await JsonSerializer.SerializeAsync(stream, obj, obj.GetType());
                stream.Position = 0;
                var reader = new StreamReader(stream);
                return await reader.ReadToEndAsync();
            }
        }


        public static T DeserizalizeWithOptions(string jsonPerson)
        {
            var options = new JsonSerializerOptions
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            };
            T personObject;
             personObject = JsonSerializer.Deserialize<T>(jsonPerson);
        
            return personObject;

            //Console.WriteLine($"Person's age: {personObject.Age}");
            //Console.WriteLine($"Person's first pet's name: {personObject.Pets.First().Age}");
        }

    }
}
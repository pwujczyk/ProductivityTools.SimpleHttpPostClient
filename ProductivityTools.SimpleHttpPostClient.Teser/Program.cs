﻿using NUnit.Framework;
using ProductivityTools.SimpleHttpPostClient.CommonObjects;
using System;

namespace ProductivityTools.SimpleHttpPostClient.Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine("Hit enter to start test");
            Console.ReadLine();
            HttpPostClient client = new HttpPostClient(true);
            client.SetBaseUrl("https://localhost:8801/Api");

            ComplexObject complex = new ComplexObject();
            complex.NameIn = "Pawel Wujczyk";
            Assert.IsNull(complex.NameOut);

            var index = client.PostAsync<string>("Test", "Index").GetAwaiter().GetResult();
            Assert.AreEqual("Test", index);
            Console.WriteLine("Index string test OK!");


            var nullresult = client.PostAsync<object>("Test", "Null").GetAwaiter().GetResult();
            Console.WriteLine("null test OK!");

            var result = client.PostAsync<ComplexObject>("Test", "FillNameOut", complex).GetAwaiter().GetResult();
            Assert.AreEqual("Pawel Wujczyk", result.NameOut);
            Console.WriteLine("Complex object test OK!");

            Console.Write("If you see this line, it means that test passed ;-)");
            Console.ReadLine();
        }
    }
}

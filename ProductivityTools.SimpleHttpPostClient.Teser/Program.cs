using NUnit.Framework;
using ProductivityTools.SimpleHttpPostClient.CommonObjects;
using System;

namespace ProductivityTools.SimpleHttpPostClient.Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            HttpPostClient client = new HttpPostClient();
            client.EnableLogging();
            client.SetBaseUrl("https://localhost:44311/Api");

            ComplexObject complex = new ComplexObject();
            complex.NameIn = "Pawel Wujczyk";
            Assert.IsNull(complex.NameOut);

            var index = client.Post<string>("Test", "Index").GetAwaiter().GetResult();
            Assert.AreEqual("Test", index);
            Console.WriteLine("Index string test OK!");


            var nullresult = client.Post<object>("Test", "Null").GetAwaiter().GetResult();
            Console.WriteLine("null test OK!");

            var result = client.Post<ComplexObject>("Test", "FillNameOut", complex).GetAwaiter().GetResult();
            Assert.AreEqual("Pawel Wujczyk", result.NameOut);
            Console.WriteLine("Complex object test OK!");


            Console.Write("If you see this line, it means that test passed ;-)");
            Console.ReadLine();
        }
    }
}

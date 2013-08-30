using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Person
    {
        public int Age
        {
            get;
            set;
        }
    }

    class Program
    {
        private static void Main(string[] args)
        {
            Person p1 = new Person() {Age = 22};

            Person p2 = p1;

            p1.Age = 10;

            Console.WriteLine(p2.Age);


            Person[] ar = new Person[3] { new Person(), new Person(), new Person() };
            var age = ar[0].Age;
        }
    }
}

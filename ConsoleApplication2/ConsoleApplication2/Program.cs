using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {
        public enum BorderSide { Left, Right, Top,Bottom }

        public enum Days {  Sunday, Momday, TuresDay, Wednesday, Thursday, Friday, Saturday  }

        static void Main(string[] args)
        {
            BorderSide TopSide = BorderSide.Top;
            bool IsTop = (TopSide == BorderSide.Top);
            
            Days day = Days.Friday;
            Console.WriteLine(day);
        }

      
    }
}

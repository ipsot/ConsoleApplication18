using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication18
{
    class Program
    {
        static void Main(string[] args)
        {
            CellManager cellManager = new CellManager();

            cellManager.AddCells(150);

            cellManager.StartSimulation();

            Console.ReadKey();
        }
    }
}

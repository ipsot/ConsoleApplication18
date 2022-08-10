using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication18
{
    class CellManager
    {
        private static Random rnd = new Random();

        private List<Cell> cells;

        private char[] faces = {'@', '*', 'f', 'x', 'o', 'v'};
        private int width, height;
        
        public CellManager()
        {
            cells = new List<Cell>();

            width = Console.WindowWidth - 2;
            height = Console.WindowHeight - 2;
        }

        public void AddCells(int count)
        {
            for (int i = 0; i < count; i++)
            {
                cells.Add(CreateNewCell());
            }
        }

        public void AddSingleCell(Cell cell)
        {
            cells.Add(cell);
        }

        public void StartSimulation()
        {
            while (cells.Count>1)
            {
                Console.Clear();
                ShowAll();
                MakeStepForAllCells();
                System.Threading.Thread.Sleep(10);
            }

            Console.Clear();
            ShowAll();
        }

        private void ShowAll()
        {
            for (int i = 0; i < cells.Count; i++)
            {
                cells[i].Show();
            }
        }

        private void MakeStepForAllCells()
        {
            for (int i = 0; i < cells.Count; i++)
            {
                cells[i].MakeStep(width,height);
            }

            List<int> childrens = new List<int>();

            for (int i = 0; i < cells.Count - 1; i++)
            {
                for (int j = i + 1; j < cells.Count; j++)
                {
                    if (cells[i].IsCollision(cells[j]))
                    {
                        if (cells[i].IsEqualGender(cells[j]))
                        {
                            cells[i].Eat(cells[j]);
                        }
                        else
                        {
                            childrens.Add(cells[i].GetHalfHpSum(cells[j]));
                        }
                    }
                }
            }

            for (int i = 0; i < childrens.Count; i++)
            {
                cells.Add(CreateNewCell(childrens[i]));
            }

            for (int i = 0; i < cells.Count; i++)
            {
                if (cells[i].IsAlive() == false)
                {
                    cells.RemoveAt(i);
                    i = 0;
                }
            }
        }

        private Cell CreateNewCell()
        {
            int x = rnd.Next(0, width);
            int y = rnd.Next(0, height);
            int hp = rnd.Next(3, 100 + 1);
            char face = faces[rnd.Next(0, faces.Length)];
            Cell.Gender gender = rnd.Next(0, 1000) >= 500 ? Cell.Gender.Male : Cell.Gender.Female;

            Cell cell = new Cell(x, y, gender, hp, face);

            while (IsCollisionWithAnotherCells(cell))
            {
                x = rnd.Next(0, width);
                y = rnd.Next(0, height);

                cell.SetPosition(x, y);
            }

            return cell;
        }

        private Cell CreateNewCell(int halfHp)
        {
            int x = rnd.Next(0, width);
            int y = rnd.Next(0, height);
            int hp = halfHp;
            char face = faces[rnd.Next(0, faces.Length)];
            Cell.Gender gender = rnd.Next(0, 1000) >= 500 ? Cell.Gender.Male : Cell.Gender.Female;

            Cell cell = new Cell(x, y, gender, hp, face);

            while (IsCollisionWithAnotherCells(cell))
            {
                x = rnd.Next(0, width);
                y = rnd.Next(0, height);

                cell.SetPosition(x, y);
            }

            return cell;
        }


        private bool IsCollisionWithAnotherCells(Cell cell)
        {
            for (int i = 0; i < cells.Count; i++)
            {
                if (cells[i].IsCollision(cell))
                {
                    return true;
                }
            }
            return false;
        }



    }
}

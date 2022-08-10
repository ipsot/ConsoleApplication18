using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication18
{
    class Cell
    {
        private static Random rnd = new Random();

        public enum Gender
        {
            Male,
            Female
        }

        private int x, y;
        private Gender gender;
        private int hp;
        private char face;

        public Cell()
        {
            x = 0;
            y = 0;
            gender = Gender.Male;
            hp = 0;
            face = '*';
        }

        public Cell(int x, int y, Gender gender, int hp, char face)
        {
            this.x = x;
            this.y = y;
            this.gender = gender;
            this.hp = hp;
            this.face = face;
        }

        public Cell(Cell cell)
        {
            this.x = cell.x;
            this.y = cell.y;
            this.gender = cell.gender;
            this.hp = cell.hp;
            this.face = cell.face;
        }

        public void Show()
        {
            Console.SetCursorPosition(x,y);
            switch (gender)
            {
                case Gender.Female:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case Gender.Male:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
            }
            Console.Write(face);
        }

        public void MakeStep(int width, int height)
        {
            int direction = rnd.Next(0, 3+1);

            switch (direction)
            {
                case 0:
                    y--;
                    if (y < 0)
                    {
                        y = height;
                    }
                    break;
                case 1:
                    x++;
                    if (x > width)
                    {
                        x = 0;
                    }
                    break;
                case 2:
                    y++;
                    if (y > height)
                    {
                        y = 0;
                    }
                    break;
                case 3:
                    x--;
                    if (x < 0)
                    {
                        x = width;
                    }
                    break;
            }
        }

        public bool IsCollision(Cell cell)
        {
            return x == cell.x && y == cell.y;
        }

        public bool IsEqualGender(Cell cell)
        {
            return gender == cell.gender;
        }

        public void Eat(Cell cell)
        {
            if (hp < cell.hp)
            {
                cell.hp += hp;
                hp = 0;
            }
            else if (hp > cell.hp)
            {
                hp += cell.hp;
                cell.hp = 0;
            }
        }

        public bool IsAlive()
        {
            return hp > 0;
        }

        public int GetHalfHpSum(Cell cell)
        {
            return (hp + cell.hp) / 2;
        }

        public void SetPosition(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}

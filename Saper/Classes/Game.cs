using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saper.Classes
{
    public class Game
    {
        public GameStatus Status { get; private set; } //Статус игры
        public Field[,] Fields { get; private set; }//Поле
        public Level level { get; private set; }//Уровень сложности
        public int Size { get; private set; }//Размер поля
        public int CountBomb { get; private set; }//Кол-во бомб

        public Game(Level level)//Конструктор
        {
            this.level = level;
            NewGame(level);
        }
        //Проверка выигрыша
        public Boolean gameWin()
        {
            int N = 0;
            foreach (var item in Fields)
            {
                if (item.Click && !item.Bomb)
                {
                    N++;
                }
            }
            if (N == (Size * Size - CountBomb))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //Проверка клика на проигрыш/выигрыш
        public void click(Field field)
        {
            field.Click = true;
            if (field.Bomb)
            {
                Status = GameStatus.GameOver;
            }
            else if (gameWin())
            {
                Status = GameStatus.Win;
            }
        }

        public void NewGame(Level level)//Метод
        {
            Status = GameStatus.Game;
            switch (level)
            {
                case Level.Easy:
                    Size = 5;
                    CountBomb = 2;
                    break;
                case Level.Medium:
                    Size = 7;
                    CountBomb = 4;
                    break;
                case Level.Hard:
                    Size = 9;
                    CountBomb = 8;
                    break;
            }

            Fields = new Field[Size, Size];

            for (int iX = 0; iX < Size; iX++)
            {
                for (int iY = 0; iY < Size; iY++)
                {
                    Fields[iX, iY] = new Field(iX, iY, false);
                }
            }

            Random random = new Random();

            for (int i = 0; i < CountBomb; i++)
            {
                int x = random.Next(Size);
                int y = random.Next(Size);

                Fields[x, y].Bomb = true;
            }


            List<Field> CellAround;

            foreach (var item in Fields)
            {
                CellAround = new List<Field>();
                for (int iX = item.X - 1; iX < item.X + 2; iX++)
                {
                    for (int iY = item.Y - 1; iY < item.Y + 2; iY++)
                    {
                        if (iX < Size && iX >= 0 && iY < Size && iY >= 0)
                        {
                            CellAround.Add(Fields[iX, iY]);
                        }
                    }
                }
                foreach (var cell in CellAround)
                {
                    if (cell.Bomb)
                    {
                        item.Number++;
                    }
                }

            }
        }

    }

}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XO
{
    class Program
    {
        static char win = '-';
        static string[] PlayerName;
        static char[] cells = new char[] { '-', '-', '-', '-', '-', '-', '-', '-', '-' };

        static void show_cells()
        {
            Console.Clear();

            Console.WriteLine("Числа клеток:");
            Console.WriteLine("-1-|-2-|-3-");
            Console.WriteLine("-4-|-5-|-6-");
            Console.WriteLine("-7-|-8-|-9-");

            Console.WriteLine("Текущая ситуация (---пустой):");
            Console.WriteLine($"-{cells[0]}-|-{cells[1]}-|-{cells[2]}-");
            Console.WriteLine($"-{cells[3]}-|-{cells[4]}-|-{cells[5]}-");
            Console.WriteLine($"-{cells[6]}-|-{cells[7]}-|-{cells[8]}-");
        }
        static void make_move(int num)
        {
            string raw_cell;
            int cell;
            Console.Write(PlayerName[num]);
            do
            {
                Console.Write(",введите номер ячейки,сделайте свой ход:");

                raw_cell = Console.ReadLine();
            }
            while (!Int32.TryParse(raw_cell, out cell));
            while (cell > 9 || cell < 1 || cells[cell - 1] == 'O' || cells[cell - 1] == 'X')
            {
                do
                {
                    Console.Write("Введите номер правильного ( 1-9 ) или пустой ( --- ) клетки , чтобы сделать ход:");
                    raw_cell = Console.ReadLine();
                }
                while (!Int32.TryParse(raw_cell, out cell));
                Console.WriteLine();
            }
            cells[cell - 1] = (num == 0) ? 'X' : 'O';
        }
        static char check()
        {
            for (int i = 0; i < 3; i++)
                if (IsCheck(i))
                    return cells[i];
            return '-';
        }

        static bool IsCheck(int i) => IsDiagonalCheck() || IsHorizontalCheck(i) || IsVarticalCheck(i);

        static bool IsDiagonalCheck() => (cells[2] == cells[4] && cells[4] == cells[6]) || (cells[0] == cells[4] && cells[4] == cells[8]);

        static bool IsHorizontalCheck(int i) => cells[i * 3] == cells[i * 3 + 1] && cells[i * 3 + 1] == cells[i * 3 + 2];

        static bool IsVarticalCheck(int i) => cells[i] == cells[i + 3] && cells[i + 3] == cells[i + 6];

        static void result()
        {
            var result = (win == 'X') ? $"{PlayerName[0]} вы  выиграли поздравляем {PlayerName[1]} а вы проиграли..." :
                $"{PlayerName[1]} вы  выиграли поздравляем {PlayerName[0]} а вы проиграли...";

            Console.WriteLine(result);
        }

        static void Main(string[] args)
        {
            do
            {
                Console.Write("Введите имя первого игрока : ");
                PlayerName[0] = Console.ReadLine();

                Console.Write("Введите имя второго игрока: ");
                PlayerName[1] = Console.ReadLine();
                Console.WriteLine();
            } while (PlayerName[0] == PlayerName[1]);

            show_cells();

            for (int move = 1; move <= 9; move++)
            {
                make_move(move % 2);
                show_cells();

                if (move >= 5 && check() != '-')
                    break;
            }

            result();
        }
    }
}

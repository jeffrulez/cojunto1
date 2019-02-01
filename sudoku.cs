using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    class Program
    {
        static bool result = true;
        static bool CheckInput(short[,] input, short[,] solution)
        {
            for (int i = 0; i < input.GetUpperBound(0); i++)
            {
                for (int j = 0; j < input.GetUpperBound(1); j++)
                {
                    if(input[i,j] != 0 && input[i,j] != solution[i, j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        static bool CheckMatrix(short[,] solution, int row, int col)
        {
            List<short?> numberList = FillList();

            for (int i = row - 3; i < row; i++)
            {
                for (int j = col - 3; j < col; j++)
                {
                    if (solution[i, j] == 0)
                    {
                        result = false;
                        break;
                    }

                    short? aux = numberList.Find(nl => nl == solution[i, j]);
                    if (aux == null)
                    {
                        result = false;
                        break;
                    }
                    else
                    {
                        numberList.Remove(aux);
                    }
                }
            }
            if (col < 9 && result) CheckMatrix(solution, row, col + 3);
            if (row < 9 && result) CheckMatrix(solution, row + 3, 3);
            return result;
        }

        static bool CheckRowsColumns(short[,] solution)
        {
            List<short?> rowList = FillList();
            List<short?> ColumnList = FillList();

            for (int i = 0; i < solution.GetUpperBound(0); i++)
            {
                for (int j = 0; j < solution.GetUpperBound(1); j++)
                {
                    short? row = rowList.Find(nl => nl == solution[i, j]);
                    if (row == null)
                    {
                        return false;
                    }
                    else
                    {
                        rowList.Remove(row);
                    }

                    short? col = rowList.Find(nl => nl == solution[j, i]);
                    if (col == null)
                    {
                        return false;
                    }
                    else
                    {
                        rowList.Remove(col);
                    }
                }
                rowList = FillList();
                ColumnList = FillList();
            }
            return true;
        }

        static List<short?> FillList()
        {
            List<short?> list = new List<short?>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);
            list.Add(5);
            list.Add(6);
            list.Add(7);
            list.Add(8);
            list.Add(9);

            return list;
        }
        static void Main(string[] args)
        {
            short[,] input = new short[,]
            {
                { 4, 0, 0, 3, 0, 0, 0, 0, 0 },
                { 0, 9, 0, 4, 0, 0, 5, 8, 0 },
                { 0, 0, 0, 8, 6, 0, 3, 0, 0 },
                { 8, 0, 4, 6, 0, 2, 0, 0, 9 },
                { 0, 6, 0, 0, 0, 0, 0, 1, 0 },
                { 2, 0, 0, 5, 0, 1, 8, 0, 6 },
                { 0, 0, 1, 0, 8, 6, 0, 0, 0 },
                { 0, 2, 3, 0, 0, 4, 0, 9, 0 },
                { 0, 0, 0, 0, 0, 3, 0, 0, 2 }
            };

            short[,] solution = new short[,]
            {
                { 4, 8, 2, 3, 1, 5, 9, 6, 7 },
                { 3, 9, 6, 4, 2, 7, 5, 8, 1 },
                { 1, 5, 7, 8, 6, 9, 3, 2, 4 },
                { 8, 1, 4, 6, 3, 2, 7, 5, 9 },
                { 7, 6, 5, 9, 4, 8, 2, 1, 3 },
                { 2, 3, 9, 5, 7, 1, 8, 4, 6 },
                { 9, 7, 1, 2, 8, 6, 4, 3, 5 },
                { 6, 2, 3, 7, 5, 4, 1, 9, 8 },
                { 5, 4, 8, 1, 9, 3, 6, 7, 2 }
            };

            if(CheckInput(input, solution) && CheckMatrix(solution, 3, 3) && CheckRowsColumns(solution))
            {
                Console.WriteLine("La solucion dada satisface la instancia del sudoku");
            }
            else
            {
                Console.WriteLine("La solucion dada no satisface la instancia del sudoku");
            }
            Console.ReadLine();

        }
    }
}

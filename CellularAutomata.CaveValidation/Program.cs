using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cellular_Automata
{
	class Program
	{
		private static int _rows = 40;
		private static int _columns = 80;
		private static int _fillChance = 90;

		static void Main(string[] args)
		{
			Console.SetWindowSize(100, 50);

			int[,] array = new int[_rows, _columns];

			Console.WriteLine("Initial map.");
			InitMap(array);
			PrintArray(array);

			Console.WriteLine("First iteration.");
			FirstReMap(array);
			PrintArray(array);

			Console.WriteLine("Second iteration.");
			SecondReMap(array);
			PrintArray(array);

			Console.WriteLine("Third iteration.");
			ThirdReMap(array);
			PrintArray(array);

			Console.WriteLine("Fourth iteration.");
			Fourth(array);
			PrintArray(array);

			Console.WriteLine("Fifth iteration.");
			FifthReMap(array);
			PrintArray(array);

			Console.WriteLine("Sixth iteration.");
			FifthReMap(array);
			PrintArray(array);

			Console.WriteLine("Seven iteration.");
			FifthReMap(array);
			PrintArray(array);

			Console.WriteLine("Eigth iteration.");
			FifthReMap(array);
			PrintArray(array);

			Console.ReadLine();

			Cell[,] cellArray = new Cell[_rows, _columns];

			for (int i = 0; i < _rows; i++)
				for (int j = 0; j < _columns; j++)
					cellArray[i, j] = new Cell(array[i, j], i, j);

			FloodFill(cellArray);
		}

		private static void FloodFill(Cell[,] cellArray)
		{
			Random rand = new Random();

			int r = rand.Next(_rows);
			int c = rand.Next(_columns);

			Fill(cellArray, cellArray[r, c]);
		}

		private static void Fill(Cell[,] cellArray, Cell cell)
		{
			if (cell.IsPath)
			{
				cell.IsVisited = true;

				Cell up = cellArray[cell.Column, cell.Row + 1];
				Cell down = cellArray[cell.Column, cell.Row - 1];
				Cell left = cellArray[cell.Column - 1, cell.Row];
				Cell right = cellArray[cell.Column + 1, cell.Row];

				Console.Clear();
				PrintArray(cellArray);
				//Console.ReadLine();

				if (up.IsPath && !up.IsVisited)
				{
					Fill(cellArray, up, cell);
				}
				else if (down.IsPath && !down.IsVisited)
				{
					Fill(cellArray, down, cell);
				}
				else if (left.IsPath && !left.IsVisited)
				{
					Fill(cellArray, left, cell);
				}
				else if (right.IsPath && !right.IsVisited)
				{
					Fill(cellArray, right, cell);
				}
				else
				{
					Random rand = new Random();

					int r = rand.Next(_rows);
					int c = rand.Next(_columns);

					Fill(cellArray, cellArray[r, c]);
				}
			}
			else
			{
				Random rand = new Random();

				int r = rand.Next(_rows);
				int c = rand.Next(_columns);

				Fill(cellArray, cellArray[r, c]);
			}
		}

		private static void Fill(Cell[,] cellArray, Cell cell, Cell back)
		{
			if (cell.IsPath)
			{
				cell.IsVisited = true;

				Cell up = cellArray[cell.Column, cell.Row + 1];
				Cell down = cellArray[cell.Column, cell.Row - 1];
				Cell left = cellArray[cell.Column - 1, cell.Row];
				Cell right = cellArray[cell.Column + 1, cell.Row];

				Console.Clear();
				PrintArray(cellArray);
				//Console.ReadLine();

				if (up.IsPath && !up.IsVisited)
				{
					Fill(cellArray, up, cell);
				}
				else if (down.IsPath && !down.IsVisited)
				{
					Fill(cellArray, down, cell);
				}
				else if (left.IsPath && !left.IsVisited)
				{
					Fill(cellArray, left, cell);
				}
				else if (right.IsPath && !right.IsVisited)
				{
					Fill(cellArray, right, cell);
				}
				else
					Fill(cellArray, back);
			}
			else
			{
				Random rand = new Random();

				int r = rand.Next(_rows);
				int c = rand.Next(_columns);

				Fill(cellArray, cellArray[r, c]);
			}
		}

		private static void InitMap(int[,] array)
		{
			Random rand = new Random();

			for (int i = 0; i < _rows; i++)
				for (int j = 0; j < _columns; j++)
					array[i, j] = 0;

			for (int i = 1; i < _rows - 1; i++)
			{
				for (int j = 1; j < _columns - 1; j++)
				{
					if (rand.Next(100) < 40)
						array[i, j] = 0;
					else
						array[i, j] = 1;
				}
			}
		}

		private static void FirstReMap(int[,] array)
		{
			for (int i = 1; i < _rows - 1; i++)
			{
				for (int j = 1; j < _columns - 1; j++)
				{
					int surrounding = GetSurroundingWallCells(i, j, array);

					if (array[i, j] == 0)
					{
						if (surrounding > 4)
							array[i, j] = 0;
					}
					else if (array[i, j] == 1)
					{
						if (surrounding > 5)
							array[i, j] = 0;
					}
				}
			}
		}

		private static void SecondReMap(int[,] array)
		{
			for (int i = 1; i < _rows - 1; i++)
			{
				for (int j = 1; j < _columns - 1; j++)
				{
					int surrounding = GetSurroundingWallCells(i, j, array);

					if (array[i, j] == 0)
					{
						if (surrounding > 4)
							array[i, j] = 1;
					}
					else if (array[i, j] == 1)
					{
						if (surrounding > 5)
							array[i, j] = 0;
					}
				}
			}
		}

		private static void ThirdReMap(int[,] array)
		{
			for (int i = 1; i < _rows - 1; i++)
			{
				for (int j = 1; j < _columns - 1; j++)
				{
					int surrounding = GetSurroundingWallCells(i, j, array);

					if (array[i, j] == 0)
					{
						if (surrounding > 5)
							array[i, j] = 1;
					}
					else if (array[i, j] == 1)
					{
						if (surrounding > 4)
							array[i, j] = 0;
					}
				}
			}
		}

		private static void Fourth(int[,] array)
		{
			for (int i = 1; i < _rows - 1; i++)
			{
				for (int j = 1; j < _columns - 1; j++)
				{
					if (array[i, j] == 0)
					{
						int surrounding = GetSurroundingPathCells(i, j, array);

						if (surrounding >= 6)
							array[i, j] = 1;
					}
					else if (array[i, j] == 1)
					{
						int surroundingWalls = GetSurroundingWallCells(i, j, array);
						if (surroundingWalls >= 7)
							array[i, j] = 0;
					}
				}
			}
		}

		private static void FifthReMap(int[,] array)
		{
			for (int i = 1; i < _rows - 1; i++)
			{
				for (int j = 1; j < _columns - 1; j++)
				{
					if (array[i, j] == 0)
					{
						int surrounding = GetSurroundingPathCells(i, j, array);

						if (surrounding > 4)
							array[i, j] = 1;
					}
					else if (array[i, j] == 1)
					{
						int surroundingWalls = GetSurroundingWallCells(i, j, array);
						if (surroundingWalls > 5)
							array[i, j] = 0;
					}
				}
			}
		}

		private static int GetSurroundingWallCells(int i, int j, int[,] array)
		{
			int count = 8;

			count -= array[i - 1, j - 1];
			count -= array[i, j - 1];
			count -= array[i + 1, j - 1];

			count -= array[i - 1, j];
			count -= array[i + 1, j - 1];

			count -= array[i - 1, j + 1];
			count -= array[i, j + 1];
			count -= array[i + 1, j + 1];

			return count;
		}

		private static int GetSurroundingPathCells(int i, int j, int[,] array)
		{
			int count = 0;

			count += array[i - 1, j - 1];
			count += array[i, j - 1];
			count += array[i + 1, j - 1];

			count += array[i - 1, j];
			count += array[i + 1, j - 1];

			count += array[i - 1, j + 1];
			count += array[i, j + 1];
			count += array[i + 1, j + 1];

			return count;
		}

		private static int GetSecondSurroundingCells(int i, int j, int[,] array)
		{
			int count = 8;

			count -= array[i - 2, j - 2];
			count -= array[i, j - 2];
			count -= array[i + 2, j - 2];

			count -= array[i - 2, j];
			count -= array[i + 2, j - 2];

			count -= array[i - 2, j + 2];
			count -= array[i, j + 2];
			count -= array[i + 2, j + 2];

			return count;
		}

		private static void PrintArray(int[,] array)
		{
			for (int i = 0; i < _rows; i++)
			{
				for (int j = 0; j < _columns; j++)
				{
					if (array[i, j] == 1)
					{
						Console.ForegroundColor = ConsoleColor.Green;
						Console.Write(".");
					}
					else
					{
						Console.ForegroundColor = ConsoleColor.White;
						Console.Write("#");
					}
				}

				Console.WriteLine();
			}

			Console.WriteLine();
		}

		private static void PrintArray(Cell[,] array)
		{

			for (int i = 0; i < _rows; i++)
			{
				for (int j = 0; j < _columns; j++)
				{
					if (array[i, j].IsPath && array[i, j].IsVisited)
					{
						Console.ForegroundColor = ConsoleColor.Blue;
						Console.Write(".");
					}
					else if (array[i, j].IsPath)
					{
						Console.ForegroundColor = ConsoleColor.Green;
						Console.Write(".");
					}
					else
					{
						Console.ForegroundColor = ConsoleColor.White;
						Console.Write("#");
					}
				}

				Console.WriteLine();
			}

			Console.WriteLine();
		}
	}
}

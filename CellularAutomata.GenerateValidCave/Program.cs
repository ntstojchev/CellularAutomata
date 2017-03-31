using Automata.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automata
{
	class Program
	{
		private static int _rows = 30;
		private static int _columns = 30;
		private static int _fillChance = 40;

		private static int[,] _rawArray;

		private static Cell _startingCell;
		private static Cell[,] _cellArray;
		private static List<Cell> _listCells;

		private static int _visited = 1;
		private static int _available= 0;

		private static int _tileSize = 32;

		static void Main(string[] args)
		{
			Console.SetWindowSize(100, 50);

			while (_available != _visited + 1)
			{
				_listCells = new List<Cell>();
				_available = 0;
				_visited = 0;

				_rawArray = new int[_rows, _columns];

				InitMap(_rawArray);

				for (int i = 0; i < 6; i++)
				{
					_rawArray = IterateCave(_rawArray);
				}

				for (int k = 0; k < 3; k++)
				{
					PolishCave(_rawArray);
				}

				_cellArray = new Cell[_rows, _columns];

				for (int i = 0; i < _rows; i++)
					for (int j = 0; j < _columns; j++)
					{
						_available += _rawArray[i, j];
						_cellArray[i, j] = new Cell(_rawArray[i, j], i, j);
					}

				FloodFill(_cellArray);

				PrintArray(_cellArray);

				Console.WriteLine(_available);
				Console.WriteLine(_visited + 1);
				Console.ReadLine();
			}

			DrawImage();
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
					if (rand.Next(100) < _fillChance)
						array[i, j] = 0;
					else
						array[i, j] = 1;
				}
			}
		}

		private static int[,] IterateCave(int[,] array)
		{
			int[,] newArray = new int[_rows, _columns];

			for (int i = 1; i < _rows - 1; i++)
			{
				for (int j = 1; j < _columns - 1; j++)
				{
					int surrounding = GetSurroundingWalls(i, j, array);

					if (array[i, j] == 1 && surrounding > 5)
						newArray[i, j] = 1;
					else if (array[i, j] == 0 && surrounding > 1)
						newArray[i, j] = 1;
				}
			}
			return newArray;
		}

		private static void PolishCave(int[,] array)
		{
			for (int i = 1; i < _rows - 1; i++)
			{
				for (int j = 1; j < _columns - 1; j++)
				{
					int surrounding = GetAdjacentPaths(i, j, array);

					if (array[i, j] == 0 && surrounding > 2)
						array[i, j] = 1;
					if (array[i, j] == 0 && surrounding == 1)
						array[i, j] = 0;
					if (array[i, j] == 1 && surrounding == 1)
						array[i, j] = 0;
					else if (array[i, j] == 1 && surrounding == 0)
						array[i, j] = 0;
				}
			}
		}

		private static void FloodFill(Cell[,] cellArray)
		{
			Random rand = new Random();

			int r = rand.Next(_rows);
			int c = rand.Next(_columns);

			if (cellArray[r, c].IsPath)
			{
				_startingCell = cellArray[r, c];
				Fill(cellArray, _startingCell);
			}
		}

		private static void Fill(Cell[,] cellArray, Cell cell)
		{
			cell.IsVisited = true;

			Cell up = cellArray[cell.Column, cell.Row + 1];
			Cell down = cellArray[cell.Column, cell.Row - 1];
			Cell left = cellArray[cell.Column - 1, cell.Row];
			Cell right = cellArray[cell.Column + 1, cell.Row];

			if (up.IsPath && !up.IsVisited)
			{
				_visited += 1;
				_listCells.Add(cell);
				Fill(cellArray, up);
			}
			else if (down.IsPath && !down.IsVisited)
			{
				_visited += 1;
				_listCells.Add(cell);
				Fill(cellArray, down);
			}
			else if (left.IsPath && !left.IsVisited)
			{
				_visited += 1;
				_listCells.Add(cell);
				Fill(cellArray, left);
			}
			else if (right.IsPath && !right.IsVisited)
			{
				_visited += 1;
				_listCells.Add(cell);
				Fill(cellArray, right);
			}
			else
			{
				if (_listCells.Count == 0)
					return;

				Cell newCell = _listCells.Last();
				_listCells.Remove(_listCells.Last());
				Fill(cellArray, newCell);
			}
		}

		private static int GetSurroundingWalls(int row, int column, int[,] array)
		{
			int count = 8;

			count -= array[row - 1, column - 1];
			count -= array[row, column - 1];
			count -= array[row + 1, column - 1];

			count -= array[row - 1, column];
			count -= array[row + 1, column];

			count -= array[row - 1, column + 1];
			count -= array[row, column + 1];
			count -= array[row + 1, column + 1];

			return count;
		}

		private static int GetAdjacentPaths(int row, int column, int[,] array)
		{
				int count = 0;

				count += array[row, column - 1];
				count += array[row, column + 1];

				count += array[row - 1, column];
				count += array[row + 1, column];

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
		}

		public static void DrawImage()
		{
			Bitmap[] pathArray = new Bitmap[] { Resources.path1, Resources.path2, Resources.path3, Resources.path4,
												Resources.path5, Resources.path6, Resources.path7, Resources.path8,
												Resources.path9, Resources.path10, Resources.path11, Resources.path12,
												Resources.path13, Resources.path14, Resources.path15, Resources.path16,};

			Random rand = new Random();

			int width = _rows * _tileSize;
			int heigth = _columns * _tileSize;

			int x = 0;
			int y = 0;

			using (var bmp = new Bitmap(width, heigth))
			{
				using (Graphics g = Graphics.FromImage(bmp))
				{
					for (int i = 0; i < _rows; i++)
					{
						for (int j = 0; j < _columns; j++)
						{
							var point = new Point(x, y);

							if (_cellArray[i, j].IsPath)
							{
								int random = rand.Next(16);
								g.DrawImage(pathArray[random], point.X, point.Y, _tileSize, _tileSize);
							}
							else
							{
								g.DrawImage(Resources.wall, point.X, point.Y, _tileSize, _tileSize);
							}

							y += _tileSize;
						}

						x += _tileSize;
						y = 0;
					}
				}

				var memStream = new MemoryStream();
				bmp.Save(memStream, ImageFormat.Bmp);

				using (FileStream file = new FileStream("cave.bmp", FileMode.Create, System.IO.FileAccess.Write))
				{
					memStream.WriteTo(file);
					memStream.Close();
				}
			}
		}
	}
}

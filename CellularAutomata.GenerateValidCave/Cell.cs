using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automata
{
	public class Cell
	{
		public bool IsVisited { get; set; }

		public bool IsPath { get; set; }

		public int Column { get; set; }

		public int Row { get; set; }

		public int CursorTop { get; set; }

		public int CursorLeft { get; set; }

		public bool IsCurrent { get; set; }

		public Image Image { get; set; }

		public Cell(int v, int i, int j)
		{
			if (v == 1)
				IsPath = true;

			Column = i;
			Row = j;
		}
	}
}

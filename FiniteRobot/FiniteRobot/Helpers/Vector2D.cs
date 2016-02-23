using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drot.Helpers
{
	public class Vector2D
	{
		public double X { get; set; }
		public double Y { get; set; }

		public Vector2D()
		{
			Set(0.0, 0.0);
		}

		public Vector2D(double x, double y)
		{
			Set(x, y);
		}

		public void Set(double x, double y)
		{
			X = x;
			Y = y;
		}
	}
}

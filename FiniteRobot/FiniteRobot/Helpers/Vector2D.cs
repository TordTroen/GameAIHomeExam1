using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
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

		public double Distance(Vector2D v)
		{
			double dx = v.X - X;
			double dy = v.Y - Y;
			return Math.Sqrt((dx * dx) + (dy * dy));
		}

		public Vector2D ProjectForTime(double headingRadians, double velocity, double time)
		{
			return new Vector2D(
				X + (Math.Sin(headingRadians) * velocity * time),
				Y + (Math.Cos(headingRadians) * velocity * time));
		}
	}
}

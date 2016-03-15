using Drot.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drot
{
	public struct Behavior
	{
		public Vector2D position;
		public double angle;

		public Behavior(Vector2D pos, double ang)
		{
			position = pos;
			angle = ang;
		}
	}
}

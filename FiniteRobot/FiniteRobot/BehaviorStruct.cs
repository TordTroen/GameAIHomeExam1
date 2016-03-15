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
		public double speed;

		public Behavior(Vector2D pos, double ang, double speed)
		{
			position = pos;
			angle = ang;
			this.speed = speed;
		}
	}
}

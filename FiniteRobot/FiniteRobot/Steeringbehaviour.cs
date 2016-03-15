using Drot.Helpers;
using PG4500_2016_Exam1;
using Robocode.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drot
{
	public abstract class SteeringBehaviour
	{
		protected Trotor14 robot;
		//protected Vector2D velocity;

		public SteeringBehaviour(Trotor14 robot)
		{
			this.robot = robot;
			//velocity = new Vector2D();
		}

		public virtual Behavior GetBehavior(Vector2D targetPos)
		{
			return new Behavior(new Vector2D(), 0.0);
		}
	}
}

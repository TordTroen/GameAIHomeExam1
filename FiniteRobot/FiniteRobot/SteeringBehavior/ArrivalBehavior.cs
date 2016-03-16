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
	public class ArrivalBehavior : SteeringBehaviour
	{
		private double slowdownRadius = 100;

		public ArrivalBehavior(Trotor14 robot, double slowdownRadius)
			: base(robot)
		{
			this.slowdownRadius = slowdownRadius;
		}

		public override void Steer(Vector2D targetPos)
		{
			Vector2D curPos = robot.Position;

			// Calculate the slowdownfactor based on the distance to the endtarget
			double dist = (targetPos - curPos).Length;
			double slowdownFactor = 1.0;
			if (dist < slowdownRadius)
			{
				slowdownFactor = dist / slowdownRadius;
			}
			Vector2D desiredVelocity = (Vector2D.Normalize(targetPos - curPos) * Trotor14.MaxSpeed) * slowdownFactor;

			ApplySteering(desiredVelocity);
		}
	}
}

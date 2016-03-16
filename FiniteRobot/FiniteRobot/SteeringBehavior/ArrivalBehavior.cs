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

		public override BehaviorResult GetBehavior(Vector2D targetPos)
		{
			//robot.drawing.DrawCircle(System.Drawing.Color.Blue, targetPos, (float)slowdownRadius*2, (float)slowdownRadius*2);

			// Store robot values
			Vector2D velocity = robot.VelocityVector;
			Vector2D curPos = robot.Position;

			// Calculate the slowdownfactor based on the distance to the endtarget
			double dist = (targetPos - curPos).Length;
			double slowdownFactor = 1.0;
			if (dist < slowdownRadius)
			{
				slowdownFactor = dist / slowdownRadius;
			}
			//robot.drawing.DrawString(System.Drawing.Color.Black, string.Format("Dist: {0:0.0} - Slow: {1:0.0}", dist, slowdownFactor), new Vector2D(0, -30));
			Vector2D desiredVelocity = (Vector2D.Normalize(targetPos - curPos) * Trotor14.VelocityMax) * slowdownFactor;

			BehaviorResult behavior = ApplySteering(desiredVelocity, velocity, curPos);
			return behavior;
		}
	}
}

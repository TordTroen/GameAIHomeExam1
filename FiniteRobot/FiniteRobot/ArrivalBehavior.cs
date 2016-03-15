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

		public override Behavior GetBehavior(Vector2D targetPos)
		{
			robot.drawing.DrawCircle(System.Drawing.Color.Blue, targetPos, (float)slowdownRadius, (float)slowdownRadius);
			// Store robot values
			Vector2D velocity = robot.VelocityVector;
			Vector2D curPos = robot.Position;

			double dist = (targetPos - curPos).Length;
			double slowdownFactor = 1.0;
			if (dist < slowdownRadius)
			{
				slowdownFactor = dist / slowdownRadius;
			}
			robot.drawing.DrawString(System.Drawing.Color.Black, string.Format("Dist: {0:0.0} - Slow: {1:0.0}", dist, slowdownFactor), new Vector2D(0, -30));

			// The vector straight to the target
			Vector2D desiredVelocity = (Vector2D.Normalize(targetPos - curPos) * Trotor14.VelocityMax) * slowdownFactor;

			// Steering forces
			Vector2D steering = desiredVelocity - velocity;
			steering.Truncate(Trotor14.VelocityMax);
			steering = steering / Trotor14.Mass;

			velocity = velocity + steering;
			velocity.Truncate(Trotor14.SpeedMax);

			Vector2D pos = curPos + velocity;

			double absDeg = Vector2D.AbsoluteDegrees(robot.Position, targetPos);
			double angle = Utils.NormalRelativeAngleDegrees(absDeg - robot.Heading);
			return new Behavior(pos, angle, desiredVelocity.Length);
		}
	}
}

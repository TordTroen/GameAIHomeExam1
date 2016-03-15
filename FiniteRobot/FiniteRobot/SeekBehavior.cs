using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drot.Helpers;
using Robocode.Util;
using PG4500_2016_Exam1;

namespace Drot
{
	public class SeekBehavior : SteeringBehaviour
	{
		public SeekBehavior(Trotor14 robot)
			: base(robot)
		{
			
		}

		public override Behavior GetBehavior(Vector2D targetPos)
		{
			// Store robot values
			Vector2D velocity = robot.VelocityVector;
			Vector2D curPos = robot.Position;

			// The vector straight to the target
			Vector2D desiredVelocity = Vector2D.Normalize(targetPos - curPos) * Trotor14.VelocityMax;

			// Steering forces
			Vector2D steering = desiredVelocity - velocity;
			steering.Truncate(Trotor14.VelocityMax);
			steering = steering / Trotor14.Mass;

			velocity = velocity + steering;
			velocity.Truncate(Trotor14.SpeedMax);

			Vector2D pos = curPos + velocity;

			double absDeg = Vector2D.AbsoluteDegrees(robot.Position, targetPos);
			double angle = Utils.NormalRelativeAngleDegrees(absDeg - robot.Heading);
			return new Behavior(pos, angle);
		}
	}
}

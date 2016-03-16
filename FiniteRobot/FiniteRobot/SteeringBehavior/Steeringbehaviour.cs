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
	public class SteeringBehaviour
	{
		protected readonly Trotor14 robot;

		public SteeringBehaviour(Trotor14 robot)
		{
			this.robot = robot;
		}

		public virtual void Steer(Vector2D target)
		{

		}

		protected void ApplySteering(Vector2D desiredVelocity, Vector2D velocity)
		{
			Vector2D curPos = robot.Position;
			//Vector2D velocity = robot.VelocityVector;

			Vector2D steering = desiredVelocity - velocity;

			steering.Truncate(Trotor14.MaxSpeed);
			steering = steering / Trotor14.Mass;

			velocity = velocity + steering;
			velocity.Truncate(Trotor14.MaxSpeed);

			Vector2D pos = curPos + velocity;

			double absDeg = Vector2D.AbsoluteDegrees(robot.Position, pos);
			double angle = Utils.NormalRelativeAngleDegrees(absDeg - robot.Heading);

			robot.SetAhead(desiredVelocity.Length);
			robot.SetTurnRight(angle);

			//return new BehaviorResult(angle, desiredVelocity.Length);
			//return new BehaviorResult();
		}
	}
}

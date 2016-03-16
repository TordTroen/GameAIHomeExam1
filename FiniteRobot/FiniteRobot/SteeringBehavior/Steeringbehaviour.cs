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
		protected Trotor14 robot;
		//protected Vector2D velocity;

		public SteeringBehaviour(Trotor14 robot)
		{
			this.robot = robot;
			//velocity = new Vector2D();
		}

		public virtual BehaviorResult GetBehavior(Vector2D targetPos)
		{
			return new BehaviorResult(0.0, 0.0);
		}

		protected BehaviorResult ApplySteering(Vector2D desiredVelocity, Vector2D velocity, Vector2D curPos)
		{
			Vector2D steering = desiredVelocity - velocity;
			steering.Truncate(Trotor14.VelocityMax);
			steering = steering / Trotor14.Mass;

			velocity = velocity + steering;
			velocity.Truncate(Trotor14.SpeedMax);

			Vector2D pos = curPos + velocity;

			double absDeg = Vector2D.AbsoluteDegrees(robot.Position, pos);
			double angle = Utils.NormalRelativeAngleDegrees(absDeg - robot.Heading);

			//robot.SetAhead(desiredVelocity.Length);
			//robot.SetTurnRight(angle);

			return new BehaviorResult(angle, desiredVelocity.Length);
			return new BehaviorResult();
		}

		//public static BehaviorResult GetBehavior(Trotor14 robot, BehaviorType behaviorType, Vector2D targetPos)
		//{
		//	// Store robot values
		//	Vector2D velocity = robot.VelocityVector;
		//	Vector2D curPos = robot.Position;

		//	// The vector straight to the target
		//	Vector2D desiredVelocity = new Vector2D();// Vector2D.Normalize(targetPos - curPos) * Trotor14.VelocityMax;
		//	switch (behaviorType)
		//	{
		//		case BehaviorType.Seek:
		//			desiredVelocity = Seek();
		//			break;
		//		case BehaviorType.Arrival:
		//			desiredVelocity = Arrival();
		//			break;
		//		case BehaviorType.Wander:
		//			desiredVelocity = Wander();
		//			break;
		//	}

		//	// Steering forces
		//	Vector2D steering = desiredVelocity - velocity;
		//	steering.Truncate(Trotor14.VelocityMax);
		//	steering = steering / Trotor14.Mass;

		//	velocity = velocity + steering;
		//	velocity.Truncate(Trotor14.SpeedMax);

		//	Vector2D pos = curPos + velocity;

		//	double absDeg = Vector2D.AbsoluteDegrees(robot.Position, targetPos);
		//	double angle = Utils.NormalRelativeAngleDegrees(absDeg - robot.Heading);
		//	return new BehaviorResult(angle, desiredVelocity.Length);
		//}

		//private static Vector2D Seek(Vector2D targetPos, Vector2D curPos)
		//{
		//	return Vector2D.Normalize(targetPos - curPos) * Trotor14.VelocityMax;
		//}

		//private static Vector2D Arrival(Vector2D targetPos, Vector2D curPos, double slowdownRadius)
		//{
		//	double dist = (targetPos - curPos).Length;
		//	double slowdownFactor = 1.0;
		//	if (dist < slowdownRadius)
		//	{
		//		slowdownFactor = dist / slowdownRadius;
		//	}

		//	// The vector straight to the target
		//	return (Vector2D.Normalize(targetPos - curPos) * Trotor14.VelocityMax) * slowdownFactor;
		//}

		//private static Vector2D Wander()
		//{
		//	return new Vector2D();
		//}
	}

	public enum BehaviorType
	{
		Seek,
		Arrival,
		Wander
	}
}

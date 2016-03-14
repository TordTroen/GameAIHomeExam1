using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drot.Helpers;
using Robocode.Util;

namespace Drot.States
{
	public class BodyStatePursuit : State
	{
		private const double prefferedEnemyDistance = 250.0;

		private Vector2D velocity = new Vector2D(1, 1);
		private double maxVelocity = 1;
		private double maxSpeed = 8;
		private double mass = 1;
		private Random rnd = new Random();
		private double pursuitOffsetAngle;

		public override void OnEnter()
		{
			pursuitOffsetAngle = RandomPursuitAngleOffset(20, 30);
		}

		public override string OnUpdate()
		{
			string ret = base.OnUpdate();

			if (robot.enemyData.Distance < robot.prefferedEnemyDistance * 0.8)
			{
				ret = "CircleEnemy";
			}
			else
			{
				Vector2D position = Seek();

				// Translating into robocode
				double absDeg = Vector2D.AbsoluteDegrees(position, robot.enemyData.Position);
				double angle = Utils.NormalRelativeAngleDegrees(absDeg - robot.Heading);
				robot.SetTurnRight(angle + pursuitOffsetAngle);
				robot.SetAhead(maxSpeed);
			}

			return ret;
		}

		private Vector2D Seek()
		{
			Vector2D position = new Vector2D(robot.Position);
			Vector2D desiredVelocity = Vector2D.Normalize(robot.enemyData.Position - position) * maxVelocity;
			Vector2D steering = desiredVelocity - velocity;

			steering.Truncate(maxVelocity);
			steering = steering / mass;

			velocity = velocity + steering;
			velocity.Truncate(maxSpeed);

			return position + velocity;
		}

		private double RandomPursuitAngleOffset(double min, double max)
		{
			return (rnd.NextDouble() * (max - min) + min) * rnd.RandomSign();
		}
	}
}

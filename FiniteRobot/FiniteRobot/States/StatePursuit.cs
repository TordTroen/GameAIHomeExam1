﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drot.Helpers;
using Robocode.Util;

namespace Drot.States
{
	public class StatePursuit : State
	{
		private const double prefferedEnemyDistance = 250.0;

		private Vector2D velocity = new Vector2D(1, 1);
		private double maxVelocity = 1;
		private double maxSpeed = 1;
		private double mass = 1;
		private Random rnd = new Random();

		public override string OnUpdate()
		{
			string ret = base.OnUpdate();

			if (robot.enemyData.Distance < robot.prefferedEnemyDistance)
			{
				ret = "CircleEnemy";
			}
			else
			{
				// TODO improve this
				double diff = robot.enemyData.Distance - prefferedEnemyDistance - 36; // 36 to account for the robot size
				//robot.SetAhead(1000 * robot.wallHitMovementDir);
				//robot.SetTurnRight(robot.enemyData.Bearing + 90);

				// TODO Turn this into a function in a class or something

				// Steering behaviour stuff
				Vector2D position = new Vector2D(robot.Position);
				//velocity = (robot.enemyData.Position - position).Normalize();
				Vector2D desiredVelocity = Vector2D.Normalize(robot.enemyData.Position - position) * maxVelocity;
				Vector2D steering = desiredVelocity - velocity;

				steering.Truncate(maxVelocity);
				steering = steering / mass;

				velocity = velocity + steering;
				velocity.Truncate(maxSpeed);

				position = position + velocity;

				// Translating into robocode
				//double angle = Utils.NormalRelativeAngleDegrees(absDeg - robot.GunHeading);
				double absDeg = Vector2D.AbsoluteDegrees(position, robot.enemyData.Position);
				//double angle = Math.Atan2(position.Y, position.X);
				//angle = Utils.NormalRelativeAngleDegrees(Utils.ToDegrees(angle - robot.Heading));
				double angle = Utils.NormalRelativeAngleDegrees(absDeg - robot.Heading);
				robot.SetTurnRight(angle + RandomPursuitAngleOffset(20, 30));
				robot.SetAhead(position.Length);
			}

			return ret;
		}

		private double RandomPursuitAngleOffset(double min, double max)
		{
			return (rnd.NextDouble() * (max - min) + min) * rnd.RandomSign();
		}
	}
}

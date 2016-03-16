using System;
using System.Collections.Generic;
using System.Drawing;
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

		private Vector2D velocity = new Vector2D(0, 0);
		private double maxVelocity = 1;
		private double maxSpeed = 8;
		private double mass = 3;
		private Random rnd = new Random();
		private SeekBehavior seek;
		private ArrivalBehavior arrival;
		private WanderBehavior wander;

		public override void OnEnter()
		{
			//pursuitOffsetAngle = RandomPursuitAngleOffset(20, 30);
			velocity = new Vector2D(maxSpeed, maxSpeed);
			seek = new SeekBehavior(robot);
			arrival = new ArrivalBehavior(robot, 200);
			wander = new WanderBehavior(robot);
		}

		private Vector2D ranPos = new Vector2D();
		private long lastRndTime = -1000;

		public override string OnUpdate()
		{
			string ret = base.OnUpdate();

			if (robot.enemyData.Distance < robot.prefferedEnemyDistance * 0.8)
			{
				//ret = StateManager.StateCircleEnemy;
				//ret = StateManager.StateOffensiveSelect;
			}
			//else
			{
				if (robot.Time - lastRndTime > 35)
				{
					lastRndTime = robot.Time;
					ranPos = new Vector2D(rnd.Next(0, (int)robot.BattleFieldWidth), rnd.Next(0, (int)robot.BattleFieldHeight));
				}
				//ranPos = new Vector2D(400, 400);

				wander.Steer(ranPos);
				robot.drawing.DrawBox(Color.Yellow, ranPos, 127);
			}

			return ret;
		}

		private Vector2D Seek(Vector2D endTargetPos)
		{
			Vector2D position = new Vector2D(robot.Position);
			Vector2D desiredVelocity = Vector2D.Normalize(endTargetPos - position) * maxVelocity;
			//Vector2D desiredVelocity = Vector2D.Normalize(robot.enemyData.Position - position) * maxVelocity;
			Vector2D steering = desiredVelocity - velocity;

			steering.Truncate(maxVelocity);
			steering = steering / mass;

			velocity = velocity + steering;
			velocity.Truncate(maxSpeed);

			position = position + velocity;
			return position;
		}

		private Vector2D GetEnemyPos()
		{
			return robot.enemyData.Position;
		}

		private double RandomPursuitAngleOffset(double min, double max)
		{
			return (rnd.NextDouble() * (max - min) + min) * rnd.RandomSign();
		}
	}
}

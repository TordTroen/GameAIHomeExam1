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
		private double pursuitOffsetAngle = 0.0;
		private SeekBehavior seek;
		private ArrivalBehavior arrival;

		public override void OnEnter()
		{
			//pursuitOffsetAngle = RandomPursuitAngleOffset(20, 30);
			velocity = new Vector2D(maxSpeed, maxSpeed);
			seek = new SeekBehavior(robot);
			arrival = new ArrivalBehavior(robot, 200);
		}

		private Vector2D ranPos = new Vector2D();
		private long lastRndTime = -1000;

		public override string OnUpdate()
		{
			string ret = base.OnUpdate();

			if (robot.enemyData.Distance < robot.prefferedEnemyDistance * 0.8)
			{
				//ret = StateManager.StateCircleEnemy;
			}
			//else
			{
				if (robot.Time - lastRndTime > 35)
				{
					lastRndTime = robot.Time;
					//ranPos = new Vector2D(rnd.Next(0, (int)robot.BattleFieldWidth), rnd.Next(0, (int)robot.BattleFieldHeight));
				}
				ranPos = new Vector2D(350, 350);
				//Vector2D targetPos = Seek(ranPos);
				////Vector2D targetPos = Seek(robot.enemyData.Position);
				

				//// Translating into robocode
				//double absDeg = Vector2D.AbsoluteDegrees(robot.Position, targetPos);
				//double angle = Utils.NormalRelativeAngleDegrees(absDeg - robot.Heading);
				////double userAngle = angle*0.1;
				////userAngle = (angle/360)*20;
				Behavior b = arrival.GetBehavior(ranPos);
				robot.drawing.DrawBox(Color.Red, b.position, 127);
				robot.drawing.DrawBox(Color.Yellow, ranPos, 127);
				//robot.drawing.DrawString(Color.Black, string.Format("Angle: {0}", b.angle), new Vector2D(0, -30));
				robot.SetTurnRight(b.angle);
				//robot.SetTurnRight((angle + pursuitOffsetAngle));
				double ahead = b.position.Length; // TODO FIX THIS
												  // http://gamedev.stackexchange.com/questions/44400/arrive-steering-behavior


				robot.drawing.DrawString(System.Drawing.Color.Black, string.Format("Ahead: {0:0.0}", ahead), new Vector2D(0, -50));
				robot.SetAhead(ahead);
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

using System;
using System.Drawing;
using Drot.Helpers;
using Robocode;
using Robocode.Util;

namespace Drot.States
{
	public class StateAttack : State
	{
		private double firingPower;

		public override string OnUpdate()
		{
			string ret = base.OnUpdate();
			
			// Control the robot
			double angle = LinearTargeting();
			robot.SetTurnGunRight(angle);
			robot.Fire(firingPower);

			return ret;
		}

		private double LinearTargeting()
		{
			// TODO
			// Solve enemy's position for (t)
			// use the time the bullet wil take to enemys distance in that function
			// turn the gun that way so we can shoot
			// shoot small bullets until we have finished turning (then shoot large)

			// It hits most of the time... 


			// Firing calculations
			double dist = robot.enemyData.Distance;
			firingPower = Math.Min(500 / dist, 3);

			// TODO Take min() of the below and 1.75 (used if we miss a lot (maybe less than 50% accuracy?))
			if (robot.ConsecutiveHits < 2 || robot.enemyData.Energy <= 3)
			{
				firingPower = 1;
			}

			double bulletSpeed = Rules.GetBulletSpeed(firingPower);
			double hitTime = dist / bulletSpeed;

			// TODO Account for enemy acceleration when pedicting the position

			// Get the positions
			Vector2D efPos = robot.enemyData.GetFuturePosition(hitTime);
			efPos.Clamp(0, 0, robot.BattleFieldWidth, robot.BattleFieldHeight);
			Vector2D pos = new Vector2D(robot.Position);

			// Find the angle to the enemy predicted position
			//double dx = efPos.X - pos.X;
			//double dy = efPos.Y - pos.Y;
			//double absDeg = Utility.RadToDeg(Math.Atan2(dx, dy));
			double absDeg = Vector2D.AbsoluteDegrees(pos, efPos);
			double angle = Utils.NormalRelativeAngleDegrees(absDeg - robot.GunHeading);

			// Debug
			robot.drawing.DrawBox(Color.DeepPink, efPos, 128);
			//robot.drawing.DrawBox(Color.Gold, pos, 128);
			robot.drawing.DrawLine(Color.Cyan, pos, pos.ProjectForTime(Utility.DegToRad(Utils.NormalRelativeAngleDegrees(absDeg)), 100, 100));

			return angle;
		}
	}
}

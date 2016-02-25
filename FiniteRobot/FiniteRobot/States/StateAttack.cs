using System;
using System.Drawing;
using Drot.Helpers;
using Robocode;
using Robocode.Util;

namespace Drot.States
{
	public class StateAttack : State
	{
		public override string OnUpdate()
		{
			string ret = base.OnUpdate();

			// TODO
			// Solve enemy's position for (t)
			// use the time the bullet wil take to enemys distance in that function
			// turn the gun that way so we can shoot
			// shoot small bullets until we have finished turning (then shoot large)

			// It hits most of the time... 


			// Firing calculations
			double dist = robot.enemyData.Distance;
			double power = Math.Min(500 / dist, 3);

			// TODO Take min() of the below and 1.75 (used if we miss a lot (maybe less than 50% accuracy?))
			if (robot.enemyData.Energy <= 3) // If enemy has energy below 3, use firepower to take just enough energy
			{
				power = 1;
			}

			double bulletSpeed = Rules.GetBulletSpeed(power);
			double hitTime = dist / bulletSpeed;

			// TODO Account for enemy acceleration when pedicting the position

			// Get the positions
			Vector2D efPos = robot.enemyData.GetFuturePosition(hitTime);
			efPos.Clamp(0, 0, robot.BattleFieldWidth, robot.BattleFieldHeight);
			Vector2D pos = new Vector2D(robot.X, robot.Y);

			// Find the angle to the enemy predicted position
			double dx = efPos.X - pos.X;
			double dy = efPos.Y - pos.Y;
			double absDeg = Utility.RadToDeg(Math.Atan2(dx, dy));
			double angle = Utils.NormalRelativeAngleDegrees(absDeg - robot.GunHeading);

			// Control the robot
			robot.SetTurnGunRight(angle);
			robot.SetFire(power);

			// Debug
			robot.drawing.DrawBox(Color.DeepPink, efPos, 128);
			robot.drawing.DrawBox(Color.Gold, pos, 128);
			robot.drawing.DrawLine(Color.Cyan, pos, pos.ProjectForTime(Utility.DegToRad(Utils.NormalRelativeAngleDegrees(absDeg)), 100, 100));

			return ret;
		}
	}
}

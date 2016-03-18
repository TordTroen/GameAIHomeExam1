using System;
using System.Drawing;
using Drot.Helpers;
using Robocode;
using Robocode.Util;

namespace Drot.States
{
	/// <summary>
	/// State that uses linear target prediction to try to hit the target.
	/// </summary>
	public class GunStateLinearAttack : State
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
			// Firing calculations
			double dist = robot.enemyData.Distance;
			firingPower = Math.Min(500 / dist, 3);

			// Limit the firingpower if we 
			if (robot.ConsecutiveHits < 2 || robot.enemyData.Energy <= 3)
			{
				firingPower = 1;
			}

			double bulletSpeed = Rules.GetBulletSpeed(firingPower);
			double hitTime = dist / bulletSpeed;

			// Get the positions
			Vector2D efPos = robot.enemyData.GetFuturePosition(hitTime);
			efPos.Clamp(0, 0, robot.BattleFieldWidth, robot.BattleFieldHeight);
			Vector2D pos = new Vector2D(robot.Position);

			// Find the angle to the enemy predicted position
			double angle = Vector2D.RotationAngleFromVectors(pos, efPos, robot.GunHeading);

			// Debug
			robot.Drawing.DrawBox(Color.DeepPink, efPos, 128);
			//robot.drawing.DrawBox(Color.Gold, pos, 128);
			robot.Drawing.DrawLine(Color.Cyan, pos, pos.ProjectForTime(Utils.ToRadians(angle), 100, 100));

			return angle;
		}
	}
}

using System;
using System.Drawing;
using Drot.Helpers;
using Robocode;

namespace Drot.States
{
	public class StateAttack : State
	{
		private double _startAttackTime;

		public override void OnEnter()
		{
			base.OnEnter();

			// Turn gun towards enemy
			// TODO Replace this with turning to where the enemy is going to be
			//robot.SetTurnGunRight(robot.Heading - robot.GunHeading + robot.enemyData.Bearing);

			_startAttackTime = robot.Time;
		}

		public override string OnUpdate()
		{
			string ret = base.OnUpdate();

			// TODO
			// Solve enemy's position for (t)
			// use the time the bullet wil take to enemys distance in that function
			// turn the gun that way so we can shoot
			// shoot small bullets until we have finished turning (then shoot large)

			double power = 3;
			double bulletSpeed = Rules.GetBulletSpeed(power);
			double dist = robot.enemyData.Distance;
			double hitTime = dist / bulletSpeed;

			// TODO Clamp to screen
			Vector2D enemyFuturePos = robot.enemyData.GetFuturePosition(hitTime);
			robot.drawing.DrawBox(Color.DeepPink, enemyFuturePos, 128);
			Vector2D pos = new Vector2D(robot.X, robot.Y);
			robot.drawing.DrawBox(Color.Gold, pos, 128);

			double dx = enemyFuturePos.X - pos.X;
			double dy = enemyFuturePos.Y - pos.Y;


			//double absBearingDeg = (robot.Heading + robot.enemyData.Bearing);
			//while (absBearingDeg < 0) absBearingDeg += 360;
			double absDeg = AbsBearing(pos, enemyFuturePos);

			//double angle = Math.Atan2(enemyFuturePos.Y - pos.Y, enemyFuturePos.X - pos.X) * 180 / Math.PI;
			double angle = NormBearing(absDeg - robot.GunHeading);
			robot.SetTurnGunRight(angle);

			robot.drawing.DrawLine(Color.Cyan, pos, pos.ProjectForTime(Utils.DegToRad(NormBearing(absDeg)), 100, 100));

			//robot.SetAhead(100);
			robot.SetFire(power);

			// TODO Probably not necessary as we scan all the time
			if (robot.Time - _startAttackTime > 100)
			{
				ret = "Idle";
			}
			return ret;
		}

		double NormBearing(double angle)
		{
			while (angle > 180) angle -= 360;
			while (angle < -180) angle += 360;
			return angle;
		}

		double AbsBearing(Vector2D a, Vector2D b)
		{
			//double absB = (robot.Heading + robot.enemyData.Bearing);
			//if (absB < 0) absB += 360;
			//return absB;
			double xo = b.X - a.X;
			double yo = b.Y - b.X;
			double hyp = a.Distance(b);
			double arcSin = Utils.RadToDeg(Math.Asin(xo / hyp));//Math.toDegrees(Math.asin(xo / hyp));
			double bearing = 0;

			if (xo > 0 && yo > 0)
			{ // both pos: lower-Left
				bearing = arcSin;
			}
			else if (xo < 0 && yo > 0)
			{ // x neg, y pos: lower-right
				bearing = 360 + arcSin; // arcsin is negative here, actually 360 - ang
			}
			else if (xo > 0 && yo < 0)
			{ // x pos, y neg: upper-left
				bearing = 180 - arcSin;
			}
			else if (xo < 0 && yo < 0)
			{ // both neg: upper-right
				bearing = 180 - arcSin; // arcsin is negative here, actually 180 + ang
			}

			return bearing;
		}
	}
}

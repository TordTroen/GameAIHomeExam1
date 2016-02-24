using System;
using Drot.Helpers;
using Robocode;

namespace Drot
{
	public class EnemyData
	{
		public string Name { get; set; }
		public double Bearing { get; set; }
		public double Heading { get; set; }
		public double Distance { get; set; }
		public double Velocity { get; set; }
		public long UpdateTime { get; set; } // The time we last set this data
		public Vector2D Position { get; set; }
		private readonly FSMRobot robot;

		public EnemyData(FSMRobot robot)
		{
			this.robot = robot;
			Position = new Vector2D();
			Reset();
		}

		public void Reset()
		{
			//SetData("", 0.0, 0.0, 0);
			SetData(null);
		}

		public void SetData(ScannedRobotEvent scanEvnt)
		{
			if (scanEvnt != null)
			{
				Name = scanEvnt.Name;
				Bearing = scanEvnt.Bearing;
				Heading = scanEvnt.Heading;
				Distance = scanEvnt.Distance;
				Velocity = scanEvnt.Velocity;
				UpdateTime = scanEvnt.Time;

				if (robot != null)
				{
					double b = robot.HeadingRadians + scanEvnt.BearingRadians;
					Position.Set(
						robot.X + Distance * Math.Sin(b),
						robot.Y + Distance * Math.Cos(b));
				}
			}
			else
			{
				Name = "";
				Bearing = 0.0;
				Heading = 0.0;
				Distance = 0.0;
				Velocity = 0.0;
				UpdateTime = 0;
				Position.Set(0.0, 0.0);
			}
		}

		public Vector2D GetFuturePosition(double time)
		{
			return Position.ProjectForTime(Utils.DegToRad(Heading), Velocity, time);
		}
	}
}

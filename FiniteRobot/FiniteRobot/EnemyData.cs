﻿using System;
using Drot.Helpers;
using Robocode;
using Robocode.Util;

namespace Drot
{
	public class EnemyData
	{
		public string Name { get; set; }
		public double Bearing { get; set; }
		public double Heading { get; set; }
		public double Distance { get; set; }
		public double Velocity { get; set; }
		public double Energy { get; set; }
		public double OldEnergy { get; set; }
		public long UpdateTime { get; set; } // The time we last set this data
		public Vector2D Position { get; set; }
		public Vector2D LastPosition { get; set; }

		public bool EnergyChanged { get { return !Utils.IsNear(OldEnergy, Energy); } }
		private readonly FSMRobot robot;
		private const long ValidDataTime = 10;

		public EnemyData(FSMRobot robot)
		{
			this.robot = robot;
			Position = new Vector2D();
			LastPosition = new Vector2D();
			Reset();
		}

		public void Reset()
		{
			SetData(null);
		}

		public void SetData(ScannedRobotEvent scanEvnt)
		{
			OldEnergy = Energy;
			LastPosition = new Vector2D(Position);

			if (scanEvnt != null)
			{
				Name = scanEvnt.Name;
				Bearing = scanEvnt.Bearing;
				Heading = scanEvnt.Heading;
				Distance = scanEvnt.Distance;
				Velocity = scanEvnt.Velocity;
				UpdateTime = scanEvnt.Time;
				Energy = scanEvnt.Energy;

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
			return Position.ProjectForTime(Utility.DegToRad(Heading), Velocity, time);
		}

		public bool ValidData()
		{
			return true;//robot.Time - UpdateTime > ValidDataTime;
		}
	}
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drot.States
{
	public class RadarStateScanSweep : State
	{
		public override void OnEnter()
		{
			robot.SetTurnRight(double.PositiveInfinity);
		}

		public override void OnExit()
		{
			robot.SetTurnRight(0.0);
		}
	}
}

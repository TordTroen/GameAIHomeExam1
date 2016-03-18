using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drot.States
{
	/// <summary>
	/// State that does a simple radar sweep.
	/// </summary>
	public class RadarStateScanSweep : State
	{
		public override void OnEnter()
		{
			robot.SetTurnRadarRight(double.PositiveInfinity);
		}

		public override void OnExit()
		{
			robot.SetTurnRadarRight(0.0);
		}
	}
}

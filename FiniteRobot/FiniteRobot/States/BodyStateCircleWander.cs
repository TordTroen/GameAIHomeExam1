using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drot.States
{
	/// <summary>
	/// Moves in circles around itself.
	/// </summary>
	public class BodyStateCircleWander : State
	{
		public override string OnUpdate()
		{
			string ret = null;

			robot.SetTurnRight(100);
			robot.SetAhead(1000);

			if (robot.enemyData.GetDistanceLevel() != DistanceLevel.Preffered)
			{
				ret = StateManager.StateMovementSelect;
			}

			return ret;
		}
	}
}

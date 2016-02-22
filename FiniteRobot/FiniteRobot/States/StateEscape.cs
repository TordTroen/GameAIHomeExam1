using System;

namespace Drot.States
{
	public class StateEscape : State
	{
		Random rand = new Random();

		public override void OnEnter()
		{
			base.OnEnter();

			int flip = 1;
			if (rand.NextDouble() > 0.5)
			{
				flip = -1;
			}
			robot.SetTurnLeft((robot.Heading - robot.bulletData.Heading) * flip);
		}

		public override string OnUpdate()
		{
			string ret = null;
			robot.SetAhead(30);
			if (robot.TurnRemaining.IsZero())
			{
				ret = "Idle";
			}

			return ret;
		}
	}
}

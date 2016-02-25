using System;

namespace Drot.States
{
	public class StateDodge : State
	{
		public override void OnEnter()
		{
			base.OnEnter();

			// TODO Figure out if we are too close to a wall to dodge (else just pick a random direction)
			// TODO Save if we were hit while dodging last time (if so; dodge the other way)??
			int direction = Utility.RandomSign();
			// TODO Maybe figure out if we are aprox. perpendicular to the enemy robot so we know 
			//		if we should turn so we can properly dodge
			
		}

		public override string OnUpdate()
		{
			string ret = base.OnUpdate();



			//robot.SetAhead(30);
			if (robot.DistanceRemaining.IsZero())
			{
				ret = "Pursuit";
			}

			return ret;
		}
	}
}

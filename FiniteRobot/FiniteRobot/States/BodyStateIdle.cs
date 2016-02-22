namespace Drot.States
{
	public class BodyStateIdle : State
	{
		public BodyStateIdle()
			: base("Idle")
		{

		}

		public override void OnEnter()
		{
			base.OnEnter();
		}

		public override StateID OnUpdate()
		{
			StateID ret = base.OnUpdate();

			/*robot.SetTurnRadarLeft(45);
			if (robot.RadarTurnRemaining.IsZero())
			{
				
			}*/

			robot.SetAhead(20);
			robot.SetTurnLeft(45);

			return ret;
		}

		public override void OnExit()
		{
			base.OnExit();
		}
	}
}

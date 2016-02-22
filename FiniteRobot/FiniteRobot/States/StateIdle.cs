namespace Drot.States
{
	public class StateIdle : State
	{
		public StateIdle()
			: base(StateID.Idle)
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

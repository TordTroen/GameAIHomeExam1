namespace Drot.States
{
	public class StateIdle : State
	{
		public override string OnUpdate()
		{
			string ret = null;

			/*robot.SetTurnRadarLeft(45);
			if (robot.RadarTurnRemaining.IsZero())
			{
				
			}*/

			robot.SetAhead(20);
			robot.SetTurnLeft(45);

			return ret;
		}
	}
}

namespace Drot.States
{
	public class StateAttack : State
	{
		private double _startAttackTime;

		public override void OnEnter()
		{
			base.OnEnter();

			// Turn gun towards enemy
			// TODO Replace this with turning to where the enemy is going to be
			robot.SetTurnGunRight(robot.Heading - robot.GunHeading + robot.enemyData.Bearing);

			_startAttackTime = robot.Time;
		}

		public override string OnUpdate()
		{
			string ret = null;

			//robot.SetAhead(100);
			robot.SetFire(200);
			if (robot.Time - _startAttackTime > 10)
			{
				ret = "Idle";
			}
			return ret;
		}
	}
}

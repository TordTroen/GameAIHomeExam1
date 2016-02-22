namespace Drot.States
{
	public class StateAttack : State
	{
		private double _startAttackTime;

		public StateAttack()
			: base(StateID.Attack)
		{

		}

		public override void OnEnter()
		{
			base.OnEnter();

			// Turn gun towards enemy
			// TODO Replace this with turning to where the enemy is going to be
			robot.SetTurnGunRight(robot.Heading - robot.GunHeading + robot.enemyData.Bearing);

			_startAttackTime = robot.Time;
		}

		public override StateID OnUpdate()
		{
			StateID ret = base.OnUpdate();

			//robot.SetAhead(100);
			robot.SetFire(200);
			if (robot.Time - _startAttackTime > 10)
			{
				ret = StateID.Idle;
			}
			return ret;
		}

		public override void OnExit()
		{
			base.OnExit();
		}
	}
}

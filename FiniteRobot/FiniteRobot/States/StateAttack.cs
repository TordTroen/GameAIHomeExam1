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
			string ret = base.OnUpdate();

			// TODO
			// Solve enemy's position for (t)
			// use the time the bullet wil take to enemys distance in that function
			// turn the gun that way so we can shoot
			// shoot small bullets until we have finished turning (then shoot large)

			//robot.SetAhead(100);
			robot.SetFire(10);
			if (robot.Time - _startAttackTime > 10)
			{
				ret = "Idle";
			}
			return ret;
		}
	}
}

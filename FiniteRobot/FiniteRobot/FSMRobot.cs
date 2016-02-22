using Robocode;

namespace Drot
{
    public class FSMRobot : AdvancedRobot
    {
		public EnemyData enemyData;
		public BulletData bulletData;
		private FiniteStateMachine _stateMachine;
		private FiniteStateMachine _bodyFSM;
		private FiniteStateMachine _gunFSM;
		private int radarDir = 1;

		public override void Run()
		{
			InitializeBot();
			
			SetHeading(0);

			while (true)
			{
				_stateMachine.Update();

				SetTurnRadarLeft(360 * radarDir);

				Execute();
			}
		}

	    public void SetHeading(double degrees)
	    {
		    SetTurnRight((-Heading) + degrees);
	    }

		private void InitializeBot()
		{
			_stateMachine = new FiniteStateMachine(this);
			_bodyFSM = new FiniteStateMachine(this);
			_gunFSM = new FiniteStateMachine(this);
			enemyData = new EnemyData();
			bulletData = new BulletData();
		}

		// ROBOCODE EVENTS // 

		public override void OnScannedRobot(ScannedRobotEvent evnt)
		{
			enemyData.SetData(evnt.Name, evnt.Bearing, Time);
			_stateMachine.EnqueueState("Attack");
			radarDir *= -1;
			//SetHeading(evnt.Bearing);
			SetTurnRight(evnt.Bearing);
		}

	    public override void OnHitByBullet(HitByBulletEvent evnt)
	    {
		    bulletData.SetData(evnt.Heading, Time);
			_stateMachine.EnqueueState("Escape");
		}

	    public override void OnRobotDeath(RobotDeathEvent evnt)
	    {
		    if (evnt.Name == enemyData.Name)
		    {
			    enemyData.Reset();
		    }
	    }

	    public override void OnDeath(DeathEvent evnt)
	    {
		    Out.WriteLine("I'll be back.");
	    }
    }
}

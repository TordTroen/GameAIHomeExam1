using System;
using System.Drawing;
using Drot.Helpers;
using Robocode;

namespace Drot
{
    public class FSMRobot : AdvancedRobot
    {
		public EnemyData enemyData;
		public BulletData bulletData;
		private FiniteStateMachine bodyFSM;
		private FiniteStateMachine gunFSM;
		private int radarDir = 1;
	    private bool hitEnemy = false;

	    public Drawing drawing;

		public override void Run()
		{
			InitializeBot();
			IsAdjustRadarForGunTurn = false;
			IsAdjustGunForRobotTurn = false;
			IsAdjustRadarForRobotTurn = false;
			SetHeading(0);
			while (true)
			{
				bodyFSM.Update();
				gunFSM.Update();
				Vector2D pos = new Vector2D(X, Y);
				drawing.DrawLine(Color.White, pos, pos.ProjectForTime(HeadingRadians, 10, 10));
				SetTurnRadarLeft(double.PositiveInfinity * radarDir);

				drawing.DrawString(Color.Red, "HitEnemy: " + hitEnemy, new Vector2D(0, -30));
				if (!hitEnemy && enemyData.EnergyChanged)
				{
					// Assume enemy bullet will hit where we are now
					bodyFSM.EnqueueState("Dodge");
				}

				hitEnemy = false;
				Execute();
			}
		}

	    public void SetHeading(double degrees)
	    {
		    SetTurnRight((-Heading) + degrees);
	    }

		private void InitializeBot()
		{
			drawing = new Drawing(this);
			bodyFSM = new FiniteStateMachine(this);
			gunFSM = new FiniteStateMachine(this);
			enemyData = new EnemyData(this);
			bulletData = new BulletData();
		}

		// ROBOCODE EVENTS // 

		public override void OnScannedRobot(ScannedRobotEvent evnt)
		{
			//enemyData.SetData(evnt.Name, evnt.Distance, evnt.Bearing, Time);
			enemyData.SetData(evnt);
			gunFSM.EnqueueState("Attack");
			bodyFSM.EnqueueState("Pursuit");
			radarDir *= -1;
			//SetHeading(evnt.Bearing);
			//SetTurnRight(evnt.Bearing * 180);
			drawing.DrawBox(Color.Brown, enemyData.Position, 200);
		}

	    public override void OnHitByBullet(HitByBulletEvent evnt)
	    {
		    bulletData.SetData(evnt.Heading, Time);
			bodyFSM.EnqueueState("Dodge");
		}

	    public override void OnBulletHit(BulletHitEvent evnt)
	    {
		    hitEnemy = true;
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

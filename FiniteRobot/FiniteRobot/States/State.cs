﻿using PG4500_2016_Exam1;

namespace Drot
{
	public class State
	{
		public string Id { get; private set; }
		protected Trotor14 robot;

		public void Initialize(string id, Trotor14 robot)
		{
			Id = id;
			this.robot = robot;
			OnStart();
		}

		/// <summary>
		/// Called just once after the state is initialized.
		/// </summary>
		public virtual void OnStart() { }

		public virtual void OnEnter()
		{
			//robot.Out.WriteLine("{0:000}: Entered state '{1}'", robot.Time, Id); // DEBUG
		}

		public virtual string OnUpdate()
		{
			//robot.Out.WriteLine("{0:000}: Updated state '{1}'", robot.Time, Id); // DEBUG
			return null;
		}

		public virtual void OnExit()
		{
			//robot.Out.WriteLine("{0:000}: Exited state '{1}'", robot.Time, Id); // DEBUG
		}
	}
}

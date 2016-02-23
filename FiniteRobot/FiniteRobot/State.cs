namespace Drot
{
	public class State
	{
		public string Id { get; set; }
		protected FSMRobot robot;

		public State()
		{
			
		}

		public void Initialize(string id, FSMRobot robot)
		{
			Id = id;
			this.robot = robot;
		}

		public virtual void OnEnter()
		{
			robot.Out.WriteLine("{0:000}: Entered state '{1}'", robot.Time, Id); // DEBUG
		}

		/// <summary>
		/// Called continiously when this state is the current state. 
		/// Returns a StateID that isn't StateID.None when a new state is triggered in the call.
		/// </summary>
		/// <returns>The StateID of the state to transition to.</returns>
		//public virtual StateID OnUpdate()
		//{
		//	robot.Out.WriteLine("Updated state '{0}'", ID); // DEBUG
		//	return StateID.None;
		//}

		public virtual string OnUpdate()
		{
			robot.Out.WriteLine("{0:000}: Updated state '{1}'", robot.Time, Id); // DEBUG
			return null;
		}

		public virtual void OnExit()
		{
			robot.Out.WriteLine("{0:000}: Exited state '{1}'", robot.Time, Id); // DEBUG
		}
	}
}

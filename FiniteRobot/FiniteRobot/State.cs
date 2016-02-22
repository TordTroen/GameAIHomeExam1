namespace Drot
{
	public class State
	{
		public StateID ID { get; set; }
		public string Id { get; set; }
		protected FSMRobot robot;

		public State()
		{
			
		}

		public State(StateID id)
		{
			ID = id;
		}

		public void Initialize(string id, FSMRobot robot)
		{
			Id = id;
			this.robot = robot;
		}

		public virtual void OnEnter()
		{
			robot.Out.WriteLine("Entered state '{0}'", ID); // DEBUG
		}

		/// <summary>
		/// Called continiously when this state is the current state. 
		/// Returns a StateID that isn't StateID.None when a new state is triggered in the call.
		/// </summary>
		/// <returns>The StateID of the state to transition to.</returns>
		public virtual StateID OnUpdate()
		{
			robot.Out.WriteLine("Updated state '{0}'", ID); // DEBUG
			return StateID.None;
		}

		public virtual string OnUpdate(int i)
		{
			robot.Out.WriteLine("Updated state '{0}'", ID); // DEBUG
			return "";
		}

		public virtual void OnExit()
		{
			robot.Out.WriteLine("Exited state '{0}'", ID); // DEBUG
		}
	}
}

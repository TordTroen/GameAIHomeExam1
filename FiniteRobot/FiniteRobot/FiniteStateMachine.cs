using System.Collections.Generic;
using System.IO;
using Drot.States;
using PG4500_2016_Exam1;

namespace Drot
{
	public class FiniteStateMachine
	{
		public State curState;
		public string CurrentStateID { get { return curState.Id; } }
		public Queue<State> stateQueue = new Queue<State>();
		//private readonly State[] _allStates; // Contains all possible states, so we don't have to make several instances of the same state
		private const int MaxTransitionsPerFrame = 10;
		//private Dictionary<string, State> states;
		private readonly StateManager states;

		public FiniteStateMachine(trotor14 robot)
		{
			states = new StateManager(robot);

			// Start in the idle state
			SetCurrentState(states.GetState(StateManager.StateIdle));
		}

		/// <summary>
		/// Enqueues a state with the gives stateID. 
		/// Checks to make sure stateID isn't null or the same as the current state.
		/// </summary>
		/// <param name="stateId">The State to enqueue.</param>
		public void EnqueueState(string stateId)
		{
			if (stateId != null && states.HasState(stateId) && curState != states.GetState(stateId))
			{
				stateQueue.Enqueue(states.GetState(stateId));
			}
		}

		public void Update()
		{
			int processCount = 0;

			do
			{
				// Make sure we don't spin outta control
				processCount++;
				if (processCount > MaxTransitionsPerFrame)
				{
					break;
				}

				if (stateQueue.Count > 0)
				{
					SetCurrentState(stateQueue.Dequeue());
				}

				string queuedState = curState.OnUpdate();
				EnqueueState(queuedState);

			} while (stateQueue.Count > 0);
		}

		private void SetCurrentState(State newState)
		{
			if (curState != null)
			{
				curState.OnExit();
			}

			curState = newState;

			if (curState != null)
			{
				curState.OnEnter();
			}
		}
	}
}

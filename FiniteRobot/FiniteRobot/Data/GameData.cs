using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robocode;
using PG4500_2016_Exam1;
using Drot.States;

namespace Drot
{
	/// <summary>
	/// This whole setup depends on the fact that all states that can be sampled are added to the dictionary in the AddSamples() function
	/// </summary>
	public class GameData
	{
		private const string DataFile = "trotor14_data.txt";
		public Dictionary<string, RoundData> samples = new Dictionary<string, RoundData>();
		private readonly Trotor14 robot;
		private readonly Random rnd = new Random();
		public int NumberOfSamples { get; private set; }

		public GameData(Trotor14 robot)
		{
			this.robot = robot;
			Init();
		}

		private void Init()
		{
			// Wipe the data in the file before we start
			if (robot.RoundNum <= 0)
			{
				WipeData();
			}

			AddSamples();
			ReadData();
		}

		private void AddSamples()
		{
			// Add all states that are going to be sampled
			samples.Add(StateManager.StateCircleEnemy, new RoundData());
			samples.Add(StateManager.StateCircleWander, new RoundData());
			NumberOfSamples = samples.Count;
		}

		/// <summary>
		/// Reads data from the data file into the samples Dictionary.
		/// </summary>
		public void ReadData()
		{
			try
			{
				StreamReader sr = new StreamReader(robot.GetDataFile(DataFile));
				while (sr.Peek() >= 0)
				{
					string[] values = sr.ReadLine().Split(';');
					if (values.Length != 3) continue;

					string key = values[0];
					if (!samples.ContainsKey(key)) continue;

					int robEnergy = 0;
					int eneEnergy = 0;
					int.TryParse(values[1], out robEnergy);
					int.TryParse(values[2], out eneEnergy);
					samples[key] = samples[key].AddToData(robEnergy, eneEnergy, samples[key].SampleCount);
					robot.Out.WriteLine("Read: " + string.Format("{0}, {1}", samples[key].RobotEnergy, samples[key].EnemyEnergy));
				}
				sr.Close();
			}
			catch (Exception e)
			{
				robot.Out.WriteLine("Failed to read from '" + DataFile + "'... " + e.ToString());
			}
		}

		/// <summary>
		/// Saves the data in the samples Dictionary into the data file.
		/// </summary>
		private void SaveData()
		{
			try
			{
				StreamWriter sw = new StreamWriter(robot.GetDataFile(DataFile));
				foreach (var item in samples)
				{
					string line = string.Format("{0};{1};{2}", item.Key, item.Value.RobotEnergy, item.Value.EnemyEnergy);
					robot.Out.WriteLine("Write: " + line);
					sw.WriteLine(line);
				}
				robot.Out.WriteLine("Writing");
				sw.Close();
			}
			catch (Exception e)
			{
				robot.Out.WriteLine("Failed to write to '" + DataFile + "'... " + e.ToString());
			}
		}

		/// <summary>
		/// Resets the data in the file.
		/// </summary>
		private void WipeData()
		{
			samples = new Dictionary<string, RoundData>();
			AddSamples();
			SaveData();
			samples = new Dictionary<string, RoundData>();
		}

		/// <summary>
		/// Called when the round is over.
		/// </summary>
		/// <param name="state">Body movement state that was used that round.</param>
		public void OnRoundOver(string state)
		{
			if (samples.ContainsKey(state))
			{
				samples[state] = samples[state].AddToData((int)robot.Energy, (int)robot.enemyData.Energy, samples[state].SampleCount);
			}
			SaveData();
		}

		/// <summary>
		/// Returns the best state depening on wheter we have sampled everything or not, and the state that has the best score.
		/// </summary>
		public string GetBestState()
		{
			if (NumberOfSamples * BodySelectMovementState.SampleIterations > robot.RoundNum)
			{
				return GetRandomState(GetLowestSampledStates());
			}
			else
			{
				return GetRandomState(GetBestStates());
			}
		}

		private string GetRandomState(List<string> states)
		{
			int ranIndex = rnd.Next(0, states.Count);
			return states[ranIndex];
		}

		/// <summary>
		/// Returns a list of the states with the highest score.
		/// </summary>
		private List<string> GetBestStates()
		{
			List<string> states = new List<string>();

			// Sort the dictionary by the score of the RoundData in the value
			var list = from pair in samples
					   orderby pair.Value.Score 
					   descending select pair;

			// Get the state(s) with the highest score
			int highestScore = int.MaxValue;
			foreach (var item in list)
			{
				int score = item.Value.Score;
				if (highestScore == int.MaxValue)
				{
					highestScore = score;
				}
				if (highestScore == score)
				{
					states.Add(item.Key);
				}
			}
			return states;
		}

		/// <summary>
		/// Return a list of the states that are sampled the fewest times.
		/// </summary>
		private List<string> GetLowestSampledStates()
		{
			List<string> states = new List<string>();
			// Sort the dictionary by the samplecount of the RoundData in the value
			var list = from pair in samples
					   orderby pair.Value.SampleCount
					   ascending
					   select pair;

			// Get the state(s) with the lowest sample count
			int lowestCount = int.MinValue;
			foreach (var item in list)
			{
				int score = item.Value.Score;
				if (lowestCount == int.MinValue)
				{
					lowestCount = item.Value.SampleCount;
				}
				if (lowestCount == item.Value.SampleCount)
				{
					states.Add(item.Key);
				}
			}
			return states;
		}

		/// <summary>
		/// Holds some data for a round.
		/// </summary>
		public class RoundData
		{
			public int RobotEnergy { get; private set; }
			public int EnemyEnergy { get; private set; }
			public int Score {
				get {
					int divisor = (SampleCount == 0) ? 1 : SampleCount;
					return (RobotEnergy - EnemyEnergy) / divisor;
				}
			}
			public int SampleCount { get; private set; }

			public RoundData() { }

			public RoundData(int robotEnergy, int enemyEnergy)
			{
				RobotEnergy = robotEnergy;
				EnemyEnergy = enemyEnergy;
			}

			public RoundData AddToData(int robotEnergy, int enemyEnergy, int oldSampleCount)
			{
				RoundData data = new RoundData(RobotEnergy + robotEnergy, EnemyEnergy + enemyEnergy);
				SampleCount = oldSampleCount + 1; // TODO unnecessary parameter
				return data;
			}
		}
	}
}

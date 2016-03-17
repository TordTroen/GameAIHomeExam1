using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robocode;
using PG4500_2016_Exam1;

namespace Drot
{
	/// <summary>
	/// This whole setup depends on the fact that all states that can be sampled are added to the dictionary in the AddSamples() function
	/// </summary>
	public class GameData
	{
		private const string DataFile = "trotor14_data.txt";
		//public Dictionary<string, int> stateOverview = new Dictionary<string, int>();
		public Dictionary<string, RoundData> samples = new Dictionary<string, RoundData>();
		private readonly Trotor14 robot;
		private readonly Random rnd = new Random();
		public int SampleCount { get; private set; }

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

			//stateOverview.Add(StateManager.StateCircleEnemy, 0);
			//stateOverview.Add(StateManager.StateCircleWander, 0);
			AddSamples();
			//overview.Add(StateManager.StateWander, new RoundData());
			ReadData();
		}

		private void AddSamples()
		{
			samples.Add(StateManager.StateCircleEnemy, new RoundData());
			samples.Add(StateManager.StateCircleWander, new RoundData());
			SampleCount = samples.Count;
		}

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
					//stateOverview.Add(key, num);
					//samples[key] = new RoundData(robEnergy, eneEnergy, samples[key].IncrementedSampleCount());
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

		private void WipeData()
		{
			samples = new Dictionary<string, RoundData>();
			AddSamples();
			SaveData();
			samples = new Dictionary<string, RoundData>();
		}

		public void OnRoundOver(string state)
		{
			if (samples.ContainsKey(state))
			{
				//stateOverview[state]++;
				//samples[state] = new RoundData((int)robot.Energy, (int)robot.enemyData.Energy, samples[state].IncrementedSampleCount());
				samples[state] = samples[state].AddToData((int)robot.Energy, (int)robot.enemyData.Energy, samples[state].SampleCount);
			}
			SaveData();
		}

		public string GetBestState()
		{
			if (SampleCount > robot.RoundNum)
			{
				// return one of getbeststates()
				return GetRandomState(GetUnsampledStates());
			}
			else
			{
				// return get random unsampled
				return GetRandomState(GetBestStates());
			}
		}

		private string GetRandomState(List<string> states)
		{
			int ranIndex = rnd.Next(0, states.Count);
			return states[ranIndex];
		}

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

		private List<string> GetUnsampledStates()
		{
			List<string> states = new List<string>();
			foreach (var item in samples)
			{
				if (item.Value.SampleCount <= 0)
				{
					states.Add(item.Key);
				}
			}
			return states;
		}

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

			public RoundData()
			{

			}

			public RoundData(int robotEnergy, int enemyEnergy)
			{
				this.RobotEnergy = robotEnergy;
				this.EnemyEnergy = enemyEnergy;
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

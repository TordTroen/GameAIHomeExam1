namespace Drot
{
	public class EnemyData
	{
		public double Bearing { get; set; }
		public long UpdateTime { get; set; } // The time we last set this data
		public string Name { get; set; }

		public EnemyData()
		{
			Reset();
		}

		public void Reset()
		{
			SetData("", 0.0, 0);
		}

		public void SetData(string name, double bearing, long updateTime)
		{
			Name = name;
			Bearing = bearing;
			UpdateTime = updateTime;
		}
	}
}

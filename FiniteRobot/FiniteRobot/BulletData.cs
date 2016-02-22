namespace Drot
{
	public class BulletData
	{
		public double Heading { get; set; }
		public long UpdateTime { get; set; }

		public BulletData()
		{
			SetData(0.0, 0);
		}

		public void SetData(double heading, long updateTime)
		{
			Heading = heading;
			UpdateTime = updateTime;
		}
	}
}

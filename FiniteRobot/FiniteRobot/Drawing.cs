using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Drot.Helpers;
using Robocode.Util;

namespace Drot
{
	public class Drawing
	{
		private FSMRobot robot;

		public Drawing(FSMRobot robot)
		{
			this.robot = robot;
		}

		public void DrawBox(Color color, Vector2D pos, int alpha = 255,
			float width = 50f, float height = 50f)
		{
			Color col = Color.FromArgb(alpha, color);
			robot.Graphics.FillRectangle(new SolidBrush(col),
				(int)(pos.X - (width / 2)),
				(int)(pos.Y - (height / 2)),
				width, height);
		}
	}
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Drot.Helpers;
using Robocode.Util;
using PG4500_2016_Exam1;

namespace Drot
{
	public class Drawing
	{
		private Trotor14 robot;
		private Font font;

		public Drawing(Trotor14 robot)
		{
			this.robot = robot;
			font = new Font(new FontFamily("Arial"), 1);
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

		public void DrawLine(Color color, Vector2D start, Vector2D end, int alpha = 255, float width = 10f)
		{
			Color col = Color.FromArgb(alpha, color);
			robot.Graphics.DrawLine(new Pen(col, width), (float)start.X, (float)start.Y, (float)end.X, (float)end.Y);
		}

		public void DrawString(Color color, string s, Vector2D pos, int alpha = 255)
		{
			Color col = Color.FromArgb(alpha, color);
			robot.Graphics.DrawString(s, font, new SolidBrush(col), (int)pos.X, (int)pos.Y);
		}

		public void DrawCircle(Color color, Vector2D pos, float width, float height, float stroke = 6f, int alpha = 255)
		{
			Color col = Color.FromArgb(alpha, color);
			robot.Graphics.DrawEllipse(new Pen(col, stroke), (float)pos.X - width / 2, 
										(float)pos.Y - height / 2, width, height);
		}
	}
}

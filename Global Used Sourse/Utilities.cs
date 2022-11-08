using System;
using Global.Data.GameSystem;
using Godot;
using Array = Godot.Collections.Array;

namespace Global
{
	public static class Utilities
	{
		private static readonly Random random = new Random();
		public enum Parameter
		{
			NULL,
			WITH_OUT_ZERO
		}
		public static float GetRandomNumberFloat(float minimum, float maximum)
		{
			float number = (float)(random.NextDouble() * (maximum - minimum) + minimum);
			return number;
		}
		public static int GetRandomNumberInt(int minimum, int maximum, Parameter parameter = Parameter.NULL)
		{
			int number;
			try
			{
				number = random.Next(minimum,maximum);
			}
			catch (ArgumentOutOfRangeException e)
			{
				number = random.Next(minimum, Int32.MaxValue);
			}

			if (parameter == Parameter.WITH_OUT_ZERO)
			{
				if (number == 0)
				{
					if (random.Next() > 0) number += 1;
					else number -= 1;
				}
			}
			return number;
		}
		
		/// <summary>
		/// Pause mode = TRUE - node is PAUSED. <br/>
		/// Pause mode = FALSE - node is UNPAUSED
		/// </summary>
		public static void SetNodePause(Node node, bool isNodeNeedToPause = true, bool needPauseChildren = false)
		{
			node.SetProcess(!isNodeNeedToPause);
			node.SetPhysicsProcess(!isNodeNeedToPause);
			node.SetProcessInput(!isNodeNeedToPause);
			node.SetProcessUnhandledInput(!isNodeNeedToPause);
			node.SetProcessUnhandledKeyInput(!isNodeNeedToPause);
			node.SetBlockSignals(isNodeNeedToPause);
			
			if(node is IPauseable pauseable)
				pauseable.SetPause(isNodeNeedToPause);
			
			if (needPauseChildren)
			{
				foreach (Node child in node.GetChildren())
				{
					SetNodePause(child, isNodeNeedToPause, needPauseChildren);
				}
			}
		}
	}
}

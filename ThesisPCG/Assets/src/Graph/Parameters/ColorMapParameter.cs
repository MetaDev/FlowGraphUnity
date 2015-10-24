using System;
using Graph.Parameters;
using UnityEngine;

namespace Graph.Parameters
{
	public class ColorMapParameter : Parameter, I2DParameter<Color>
	{
		public ColorMapParameter (string name) : base (name)
		{
		}

		public Color[][] GetValue ()
		{
			return this.GetValue<Color[][]> ();
		}

		public void SetValue (Color[][] value)
		{
			this.SetValue<Color[][]> (value);
		}

		public Color GetValue (int i, int j)
		{
			return this.GetValue<Color[][]> () [i] [j];
		}

		public void SetValue (int i, int j, Color value)
		{
			this.GetValue<Color[][]> () [i] [j] = value;
		}
	}
}


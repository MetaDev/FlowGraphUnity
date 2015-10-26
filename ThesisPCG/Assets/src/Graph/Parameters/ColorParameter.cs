using System;
using Graph.Parameters;
using UnityEngine;

namespace Graph.Parameters
{
	public class ColorParameter : Parameter, IParameter<Color>
	{
		public ColorParameter (string name) : base (name)
		{
		}

		public Color GetValue ()
		{
			return this.GetValue<Color> ();
		}

		public void SetValue (Color value)
		{
			this.SetValue<Color> (value);
		}

	
	}
}


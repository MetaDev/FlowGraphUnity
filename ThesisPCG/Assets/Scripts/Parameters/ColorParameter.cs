using System;
using Graph.Parameters;
using UnityEngine;

namespace Graph.Parameters
{
	public class ColorParameter : Parameter, IParameter<Color>
	{
		public ColorParameter (string name,Color value=default(Color)) : base (name,value)
		{
		}

		public Color GetValue ()
		{
			return this.GetValue<Color> ();
		}

		

	
	}
}


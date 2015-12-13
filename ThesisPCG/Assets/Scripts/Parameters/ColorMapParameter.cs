using System;
using Graph.Parameters;
using UnityEngine;
using UniRx;

namespace Graph.Parameters
{
	public class ColorMapParameter : Parameter, I2DParameter<Color>
	{
		public ColorMapParameter (string name, Color[,] value=default(Color[,])) : base (name,value)
		{
		}
       
        

        public Color[,] GetValue ()
		{
			return this.GetValue<Color[,]> ();
		}

	
		public Color GetValue (int i, int j)
		{
			return this.GetValue<Color[,]> () [i, j];
		}

        
    }
}


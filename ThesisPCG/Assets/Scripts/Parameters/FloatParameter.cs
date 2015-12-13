using System;


namespace Graph.Parameters
{
	public class FloatParameter : Parameter, IParameter<float>
	{
		public FloatParameter (string name,float value=default(float)) : base (name,value)
		{
		}

		public float GetValue ()
		{
			return this.GetValue<float> ();
		}

		
		
	}
}


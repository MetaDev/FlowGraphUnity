using System;


namespace Graph.Parameters
{
	public class FloatParameter : Parameter, IParameter<float>
	{
		public FloatParameter (string name) : base (name)
		{
		}

		public float GetValue ()
		{
			return this.GetValue<float> ();
		}

		public void SetValue (float value)
		{
			this.SetValue<float> (value);
		}

		
	}
}


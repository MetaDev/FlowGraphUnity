using System;
using Graph.Parameters;
using MathNet.Numerics;

namespace Graph.Parameters
{
    public class IntegerVector2Parameter : Parameter, IParameter<Tuple<int, int>>
	{
       
    public IntegerVector2Parameter (string name) : base (name)
		{
		}

		

		public Tuple<int, int> GetValue ()
		{
			return this.GetValue<Tuple<int,int>> ();
		}

		public void SetValue (Tuple<int, int> value)
		{
			this.SetValue<Tuple<int,int>> (value);
		}


	}
}


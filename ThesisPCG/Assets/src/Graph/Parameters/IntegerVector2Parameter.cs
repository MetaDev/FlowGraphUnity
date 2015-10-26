using System;
using Graph.Parameters;
using MathNet.Numerics;

namespace Graph.Parameters
{
	public class IntegerVector2Parameter : Parameter, IVector2Parameter<int>
	{
		public IntegerVector2Parameter (string name) : base (name)
		{
		}

		public int GetValue1 ()
		{
			return this.GetValue ().Item1;
		}

		public int GetValue2 ()
		{
			return this.GetValue ().Item2;
		}

		public void SetValue1 (int value)
		{
			this.GetValue ().Item1 = value;
		}

		public void SetValue2 (int value)
		{
			this.GetValue ().Item2 = value;
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


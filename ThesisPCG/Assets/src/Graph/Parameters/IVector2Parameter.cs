using System;
using Graph.Parameters;
using MathNet.Numerics;
using System.Threading.Tasks;

namespace Graph.Parameters
{
	public interface IVector2Parameter<T> : IParameter<Tuple<T,T>>
	{
		T GetValue1 ();

		T GetValue2 ();

		void SetValue1 (T value);

		void SetValue2 (T value);
	}
}


using System;
using System.Threading.Tasks;
using UniRx;

namespace Graph.Parameters
{
	public interface IParameter<T> : ISubject<T>
	{
		T GetValue ();

		void SetValue (T value);
	}


}


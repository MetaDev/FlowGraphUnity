using System;
using System.Threading.Tasks;

namespace Graph.Parameters
{
	public interface IParameter<T>
	{
		T GetValue ();

		void SetValue (T value);
	}


}


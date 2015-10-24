using System;
using System.Threading.Tasks;

namespace Graph.Parameters
{
	public interface IParameter<T>
	{
		string Name ();

		T GetValue ();

		void SetValue (T value);
	}


}


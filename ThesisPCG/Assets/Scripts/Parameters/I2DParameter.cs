using System;
using Graph.Parameters;
using System.Threading.Tasks;

namespace Graph.Parameters
{
	public interface I2DParameter<T> : IParameter<T[,]>
	{
		T GetValue (int i, int j);

		
	}
}


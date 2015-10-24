using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Graph;
using System.Collections;
using Graph.Parameters;

namespace Graph.Process
{
	
	public interface ITransform<TIn,TOut> : IProcess
	{
		TOut Transform (IParameter<TIn> input, IEnumerable<Parameter> auxArguments);
	}
}


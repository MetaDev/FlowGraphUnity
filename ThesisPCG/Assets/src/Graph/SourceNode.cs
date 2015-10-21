using System;
using Graph;
using UniRx;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using System.Runtime.CompilerServices;

namespace Graph
{
	public abstract class SourceNode: Node, ISourceNode
	{
		Parameter OutputParameter;



		//if the source is requested as observable, return just the result
		//here also a check should be done whether the task that is responsible for calculating the result is finished
		//and if the result is up to date
		//if not a promise should returned of the result after the task finishes
		public IObservable<Parameter> AsObservable ()
		{
			return Observable.Return<Parameter> (OutputParameter);

		}

		public Parameter GetOutputParameter ()
		{
			return OutputParameter;

		}

		public bool IsType<T> () where T :Parameter
		{
			return OutputParameter.IsType<T> ();
		}






	}
}


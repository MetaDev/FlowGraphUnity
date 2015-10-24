using System;
using Graph;
using UniRx;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using System.Runtime.CompilerServices;
using Graph.Parameters;

namespace Graph
{
	public abstract class SourceNode: Node, ISourceNode<Parameter>
	{
		
		protected Parameter OutputParameter;

		public virtual Parameter GetOutputParameter ()
		{
			return OutputParameter;

		}

		public T GetOutputParameter<T> () where T : Parameter
		{
			return OutputParameter.Cast<T> ();
		}


		public SourceNode (string name, Parameter outputParameter) : base (name)
		{
			this.OutputParameter = outputParameter;
		}

		public SourceNode (string name) : base (name)
		{
		}
		


		//if the source is requested as observable, return just the result
		//here also a check should be done whether the task that is responsible for calculating the result is finished
		//and if the result is up to date
		//if not a promise should returned of the result after the task finishes
		public virtual IObservable<Parameter> AsObservable ()
		{
			return Observable.Return<Parameter> (OutputParameter);
		}



	





	}
}


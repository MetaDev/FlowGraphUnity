using System;
using Graph;
using UniRx;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using System.Runtime.CompilerServices;
using Graph.Parameters;
using UnityEngine;

namespace Graph
{
	public class SourceNode: Node, ISourceNode<Parameter>
	{
		
		protected Parameter OutputParameter;

		public virtual Parameter GetOutputParameter ()
		{
			return OutputParameter;

		}


		public T GetOutputParameter<T> () where T : Parameter
		{
			return OutputParameter.AsTypedParameter<T> ();
		}


		public SourceNode (string name, Parameter outputParameter, Action loadParameter) : base (name, loadParameter)
		{
			this.OutputParameter = outputParameter;
		}

		public SourceNode (string name, Parameter outputParameter) : base (name)
		{
			this.OutputParameter = outputParameter;
		}




		//if the source is requested as observable, return just the result
		//here also a check should be done whether the task that is responsible for calculating the result is finished
		//and if the result is up to date
		//if not a promise should returned of the result after the task finishes
		public virtual IObservable<Parameter> AsObservable ()
		{
			return Observable.Start (() => {
				Complete ();
				return OutputParameter;
			});
		}

		public  IObservable<T> AsObservable<T> () where T : Parameter
		{
			return Observable.Start (() => {
				Complete ();
				return OutputParameter.AsTypedParameter<T> ();
			});
		}



	





	}
}


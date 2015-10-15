using System;
using Graph;
using UniRx;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using System.Runtime.CompilerServices;

namespace Graph
{
	public abstract class SourceNode: ISourceNode
	{
		Dictionary<string,Parameter> OutputParameters;
		Dictionary<string,object> OutputParameterResults;
		//saved ongoing process
		Task Process;

		//action that loads sources and saves it in
		public abstract Task Completion ();

		//if the source is requested as observable, return just the result
		//here also a check should be done whether the task that is responsible for calculating the result is finished
		//and if the result is up to date
		//if not a promise should returned of the result after the task finishes
		public IObservable<T> AsObservable<T> (string outputParameter)
		{
			//cast result
			T castedResult = default(T);
			if (IsType<T> (outputParameter)) {
				castedResult = (T)OutputParameterResults [outputParameter];
			} else {
			}

			return Observable.Return<T> (castedResult);

		}

		public bool IsType<T> (string outputParameter)
		{
			return OutputParameters [outputParameter].IsType<T> ();
		}


		public void Complete ()
		{
			Process = Completion ();
			Process.Start ();
		}


		public void Lock (float lockingFactor)
		{
			throw new NotImplementedException ();
		}




	}
}


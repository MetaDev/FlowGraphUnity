using System;
using Graph;
using UniRx;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using System.Runtime.CompilerServices;
using Graph.Parameters;
using UnityEngine;
using System.Security.Cryptography;

namespace Graph
{
	public abstract class SourceNode: Node, ISourceNode<Parameter>
	{
		//the source node port
		public  Parameter OutputParameter{ get; private set; }
		//the sequence of values
		protected List<Parameter> OutputParameterSequence { get; set; }

		protected List<IConnectableObservable<Parameter>> ObservableSources = new List<IConnectableObservable<Parameter>> ();

		public int Size{ get { return OutputParameterSequence.Capacity; } }

		public SourceNode (string name, int size, Parameter outputParameter) : base (name)
		{
			this.OutputParameter = outputParameter;
			OutputParameterSequence = new List<Parameter> (size);
		}

		public SourceNode (string name, int size, Action complete, Parameter outputParameter) : base (name, complete)
		{
			this.OutputParameter = outputParameter;
			OutputParameterSequence = new List<Parameter> (size);

		}

		public Parameter GetOutputParameter ()
		{
			return OutputParameter;
		}

		public int GetSize ()
		{
			return Size;
		}
		//if the source is requested as observable, return just the result
		//here also a check should be done whether the task that is responsible for calculating the result is finished
		//and if the result is up to date
		//if not a promise should returned of the result after the task finishe

		//different observables need to be returned for array and matrix parameters
		//in this method check if a new iteration is started, if so return new observable
		public  IConnectableObservable<Parameter> AsObservable ()
		{
			var ObservableSource = AsObservable (this.Size);
			ObservableSources.Add (ObservableSource);
			return  ObservableSource;
		}


		public  IConnectableObservable<Parameter> AsObservable (int size)
		{
			//check size
			if (size % Size != 0) {
				throw new ArgumentOutOfRangeException ("The size of the sequence doesnt match the requested sequence size");
			}
			var repeatSize = size / this.Size;
			var ObservableSource = Observable.ToObservable (OutputParameterSequence);
			IConnectableObservable<Parameter> HotObservableSource = ObservableSource.Repeat<Parameter> ().Take (repeatSize * Size).Publish ();
			ObservableSources.Add (HotObservableSource);
			return  HotObservableSource;
		}
		//start sending sources
		public void Start ()
		{
			//load data
			Complete ();
			//start emitting saved values
			foreach (IConnectableObservable<Parameter> hot in ObservableSources) {
				hot.Connect ();
			}
		}





	}
}


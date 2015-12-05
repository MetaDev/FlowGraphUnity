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
	

		protected IConnectableObservable<Parameter> ObservableSource;



		public SourceNode (string name) : base (name)
		{

        }
		
		//default implementation of a single value observable, only usable for static single value data sources
		public virtual IObservable<Parameter> AsObservable ()
		{
            //load default parameter and transform into stream
            if (ObservableSource == null)
            {

                    ObservableSource = Observable.ToObservable(StreamParameter()).Publish();
       
            }
            
            return ObservableSource;
        }
       


        //start sending sources
        public void Push ()
		{
            ObservableSource.Connect();
        }

        //returns parameter type as well, we'll use this to check the parameter type until generics are sorted out
		public abstract Parameter PortParameter ();
        public abstract IEnumerable<Parameter> StreamParameter();
      

    }
}


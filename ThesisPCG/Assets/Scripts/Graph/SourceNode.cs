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
    public abstract class SourceNode : Node, ISourceNode<Parameter>
    {


        protected IConnectableObservable<Parameter> HotStream;
        protected Parameter _OutputParameter;

        public SourceNode(string name,Parameter portParameter) : base(name)
        {
            this._OutputParameter = portParameter;
        }

        //default implementation of a single value observable, only usable for static single value data sources
        public IObservable<Parameter> Source()
        {
            if (HotStream == null)
            {
                HotStream = CreateHotStream();
            }
            return HotStream;
        }

        public abstract IConnectableObservable<Parameter> CreateHotStream();
        public IConnectableObservable<Parameter> GetHotStream()
        {
            if (HotStream == null)
            {
                HotStream = CreateHotStream();
            }
            return HotStream;
        }



        //start sending sources
        public void StartOutput()
        {
            //TODO
            if (HotStream == null)
            {
                Debug.Log("Stream: "  + GetName() +" is not yet created, which means no target node is linked to this source.");
            }
            else
            {
                HotStream.Connect();
            }
    
        }

        //returns parameter type as well, we'll use this to check the parameter type until generics are sorted out
        public  Parameter OutputParameter()
        {
            return _OutputParameter;
        }
      


        public abstract int GetSize();
        
    }
}


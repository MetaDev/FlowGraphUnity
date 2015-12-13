using Graph.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx;

namespace Graph
{
    abstract class FiniteSourceNode : SourceNode
    {
     
        public FiniteSourceNode(string name, Parameter outputParameter) : base(name, outputParameter)
        {
        }
        public override int GetSize()
        {
            return GetShape().Aggregate(1, (a, b) => a * b);
        }

        //default implementation of a single value observable, only usable for static single value data sources
        public override IConnectableObservable<Parameter> CreateHotStream()
        {
            //load sequence and transform into stream
            return Observable.ToObservable(CreateSequence()).Publish();

        }
        public abstract IEnumerable<Parameter> CreateSequence();
    
        //contains the optional shape of the stream, this method returns the dimensions of the datatype represented by the stream
        public abstract int[] GetShape();
    }
}

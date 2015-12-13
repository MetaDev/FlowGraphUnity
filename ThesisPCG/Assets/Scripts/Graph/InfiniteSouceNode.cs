using Graph.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx;

namespace Graph
{
   abstract class InfiniteSourceNode : SourceNode
    {

        public InfiniteSourceNode(string name, Parameter outputParameter) : base(name,outputParameter)
        {
        }
        public abstract Parameter CreateParameter();
        public abstract override IConnectableObservable<Parameter> CreateHotStream();
        public override int GetSize()
        {
            return int.MaxValue;
        }

    }
}

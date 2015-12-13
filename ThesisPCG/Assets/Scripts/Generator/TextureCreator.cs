using System;
using System.Collections.Generic;
using Graph;
using Graph.Parameters;
using UniRx;

namespace Generator
{
    class TextureCreator : TargetNode
    {
        public TextureCreator() : base ("Debug",false)
		{
           // ObservableLogger.Listener.LogToUnityDebug();
        }
        public override void Sink(IObservable<IList<Parameter>> observable)
        {
            throw new NotImplementedException();
        }
    }
}

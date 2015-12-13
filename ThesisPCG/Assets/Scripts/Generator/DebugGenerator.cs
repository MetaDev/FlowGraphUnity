using Graph;
using Graph.Parameters;
using System;
using System.Collections.Generic;
using UniRx;
using UniRx.Diagnostics;
using UnityEngine;

namespace Generator
{
    class DebugGenerator : TargetNode
    {
        static readonly UniRx.Diagnostics.Logger logger = new UniRx.Diagnostics.Logger("Debug");
        public DebugGenerator() : base ("Debug",false)
		{
            ObservableLogger.Listener.LogToUnityDebug();
        }

        public override void Sink(IObservable<IList<Parameter>> observable)
        {
            observable.Subscribe((parameters)=> {
                //print out each parameter
                foreach (Parameter parameter in parameters)
                {
                    logger.Log(parameter.Name + " = " + parameter.GetValue<object>());
                }
            });
            

        }
    }
}

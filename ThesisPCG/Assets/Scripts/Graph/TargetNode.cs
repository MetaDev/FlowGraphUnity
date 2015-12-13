using System;
using System.Collections.Generic;

using UniRx;
using System.Linq;

using UnityEngine;
using Graph.Parameters;


namespace Graph
{
    public abstract class TargetNode : Node, ITargetNode
    {


        private Dictionary<String, Parameter> InputParameters = new Dictionary<String, Parameter>();
        private bool UnityMainThread;
        protected IDisposable InputSourcesConnection;

        public TargetNode(string name, params Parameter[] inputParameters) : this(name, true, inputParameters)
        {

        }
        public TargetNode(string name, bool unityMainThead, params Parameter[] inputParameters) : base(name)
        {
            SetParametersInDictionary(InputParameters, inputParameters);
            this.UnityMainThread = unityMainThead;
        }



        //static parameter helper methods
        public static void SetParametersInDictionary(Dictionary<String, Parameter> InputDict, Parameter[] inputParameters)
        {
            foreach (Parameter param in inputParameters)
            {
                InputDict.Add(param.Name, param);
            }
        }
        public static Parameter GetParameterFromList(String name, IList<Parameter> parameters)
        {
            foreach (Parameter parameter in parameters)
            {
                if (parameter.Name.Equals(name))
                {
                    return parameter;
                }
            }
            Debug.Log("parameter not found");
            //TODO add to log, parameter not found
            return null;
        }



        
        public void LinkTo(params ISourceNode<Parameter>[] sources)
        {
            LinkTo(this,this.UnityMainThread, sources);
        }
       
        //check if parameters compatible
        public static void LinkTo(ITargetNode target,bool unityMainThread, params ISourceNode<Parameter>[] sources)
        {
            //check if all are matching
            int matchings = 0;
            foreach (ISourceNode<Parameter> source in sources)
            {
                String sourceParamName = source.OutputParameter().Name;
                if (target.GetInputParameters().ContainsKey(sourceParamName))
                {
                    if (source.OutputParameter().Match(target.GetInputParameters()[sourceParamName]))
                    {
                        matchings++;
                    }
                    else
                    {
                        Debug.Log("parameter type mismatch. expected: " + target.GetInputParameters()[sourceParamName].GetType() + "received: " + source.OutputParameter().GetType());
                    }
                }
                else
                {
                    Debug.Log("parameter name mismatch." + source.OutputParameter().Name);
                }

            }
            //zip if all sources match

            IEnumerable<IObservable<Parameter>> zip = sources.Select((source) => (source.Source()));
            IObservable<IList<Parameter>> parameters = Observable.Zip<Parameter>(zip);
            //The target node will be drawing so it has to be on miain thread
            if (unityMainThread)
            {
                parameters = parameters.ObserveOnMainThread();
            }
            target.Sink(parameters);

        }
        public abstract void Sink(IObservable<IList<Parameter>> observable);


        public Dictionary<String, Parameter> GetInputParameters()
        {
            return InputParameters;
        }





    }
}


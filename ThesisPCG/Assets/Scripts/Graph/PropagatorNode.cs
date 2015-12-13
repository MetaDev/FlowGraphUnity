using System;
using Graph;
using UniRx;
using Graph.Parameters;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Graph
{
    public abstract class PropagatorNode : Node, IPropagatorNode<Parameter>
    {

        protected IObservable<Parameter> ObservableSource;
        private Dictionary<String, Parameter> InputParameters = new Dictionary<String, Parameter>();

        //the source node port
        private Parameter _OutputParameter;



        public PropagatorNode(string name, Parameter[] inputParameters, Parameter outputParameter) : base(name)
        {
            TargetNode.SetParametersInDictionary(InputParameters, inputParameters);
            this._OutputParameter = outputParameter;
        }
        private int _Size = int.MaxValue;


        public void LinkTo(params ISourceNode<Parameter>[] sources)
        {
            TargetNode.LinkTo(this,false,sources);

            foreach (ISourceNode<Parameter> source in sources)
            {
                //the size of the propagator is the minimum of all its sources
                this._Size = Math.Min(this._Size, source.GetSize());
           }
           
        }
        public void Sink(IObservable<IList<Parameter>> observable)
        {
            ObservableSource = Propagate(observable);
        }

        public IObservable<Parameter> Source()
        {
            //TODO check if stream has been created yet (by linking)
            return ObservableSource;
        }




        //Calculate ouputparameter of the node with inputparameters from the target
        //save new value in outputparameter and retrun it
        protected abstract IObservable<Parameter> Propagate(IObservable<IList<Parameter>> observable);

        
        public  Parameter OutputParameter()
        {
            return _OutputParameter;
        }
        public int GetSize()
        {
            return _Size;
        }

       

        public Dictionary<string, Parameter> GetInputParameters()
        {
            return InputParameters;
        }
    }
}


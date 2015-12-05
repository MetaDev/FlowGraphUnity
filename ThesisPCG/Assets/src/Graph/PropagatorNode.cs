using System;
using Graph;
using UniRx;
using Graph.Parameters;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Graph
{
	public abstract class PropagatorNode : Node,IPropagatorNode<Parameter>
	{
		
		private Dictionary<String,Parameter> InputParameters = new Dictionary<String,Parameter> ();
        protected IObservable<Parameter> ObservableSource;
        //the source node port
        private  Parameter OutputParameter;

		public PropagatorNode (string name,  params Parameter[] inputParameters) : base (name)
		{
			TargetNode.SaveInputParameters (InputParameters, inputParameters);
		}


		IConnectableObservable<Parameter> obs;

		public void LinkTo (params ISourceNode<Parameter>[] sources)
		{
			//check if all are matching
            //TODO
            //check if all are present
            //check if the order is correct
			int matchings = 0;
			foreach (ISourceNode<Parameter> source in sources) {
				String sourceParamName = source.PortParameter().Name;
				if (InputParameters.ContainsKey (sourceParamName)) {
					if (source.PortParameter().Match (InputParameters [sourceParamName])) {
                        //save source default value
                        InputParameters[source.PortParameter().Name]= source.PortParameter();
                        matchings++;
					} else {
                        
                        Debug.Log ("parameter type mismatch." + source.PortParameter().GetType ());
					}
				} else {
					Debug.Log ("parameter name mismatch: " + source.PortParameter().Name);
				}
			
			}
            //zip if all sources match
            //default values allowed
            IEnumerable<IObservable<Parameter>> zip = sources.Select((source) => (source.AsObservable()));
            IObservable<IList<Parameter>> parameters = Observable.Zip<Parameter>(zip);
            //transform 
            ObservableSource = parameters.Select ((list) => {
               
                
                foreach (Parameter p in list){
                    InputParameters[p.Name]= p;
                }
                //side effect, cache output value
               // OutputParameter = TransformParameter(InputParameters);
				//transmit to next source
				return TransformParameter(InputParameters); ;
			});
            //the source is a hot observable thus subcribing to the transformed zip won't output until sources output
            ObservableSource.Subscribe ();
		}

		public IObservable<Parameter> AsObservable ()
		{
			return ObservableSource;
		}

		


		//Calculate ouputparameter of the node with inputparameters from the target
		//save new value in outputparameter and retrun it
		protected abstract Parameter TransformParameter (Dictionary<String, Parameter> inputParameters);

		

        public abstract Parameter PortParameter();

       
    }
}


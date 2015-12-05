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
		

		private Dictionary<String,Parameter> InputParameters = new Dictionary<String,Parameter> ();

		protected IDisposable InputSourcesConnection;

		public TargetNode (string name, params Parameter[] inputParameters) : base (name)
		{
			SaveInputParameters (InputParameters, inputParameters);
		}

	

		public static void SaveInputParameters (Dictionary<String,Parameter> InputDict, Parameter[] inputParameters)
		{
			foreach (Parameter param in inputParameters) {
                InputDict.Add (param.Name, param);
			}
		}

	

		//check if parameters compatible
		public void LinkTo (params ISourceNode<Parameter>[] sources)
		{
			//check if all are matching
			int matchings = 0;
			foreach (ISourceNode<Parameter> source in sources) {
				String sourceParamName = source.PortParameter().Name;
				if (InputParameters.ContainsKey (sourceParamName)) {
					if (source.PortParameter ().Match (InputParameters [sourceParamName])) {
						matchings++;
					} else {
						Debug.Log ("parameter type mismatch." + source.PortParameter ().GetType ());
					}
				} else {
					Debug.Log ("parameter name mismatch." + source.PortParameter ().Name);
				}
			
			}
            //zip if all sources match

			IEnumerable<IObservable<Parameter>> zip = sources.Select ((source) => (source.AsObservable ()));
            IObservable<IList<Parameter>> parameters = Observable.Zip<Parameter> (zip);
            //The target node will be drawing so it has to be on miain thread
            InputSourcesConnection = parameters.ObserveOnMainThread().Select((list) => {
				//update parameters
				foreach (Parameter param in list) {
					InputParameters [param.Name]= (param);
				}
				ConsumeParameters (InputParameters);
                return Unit.Default;
			}).Subscribe ();

        }

		public Dictionary<String,Parameter> GetInputParameters ()
		{
			return InputParameters;
		}

		protected abstract void ConsumeParameters (Dictionary<String, Parameter> inputParameters);
	

		//get each source of a parameter as an observable and subscribe
		//when all sources finished consumeParameters


	


	}
}


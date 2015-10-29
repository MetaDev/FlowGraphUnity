using System;
using Graph;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UniRx;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;
using Graph.Parameters;
using System.Net.Sockets;
using UnityEditor;

namespace Graph
{
	public abstract class TargetNode : Node, ITargetNode
	{
		

		private Dictionary<String,Parameter> InputParameters = new Dictionary<String,Parameter> ();

		protected Dictionary<String,IDisposable> InputSourcesConnection = new Dictionary<String,IDisposable> ();

		public TargetNode (string name, params Parameter[] inputParameters) : base (name)
		{
			SaveInputParameters (inputParameters);
		}

		public TargetNode (string name, Action consumeParameters, params Parameter[] inputParameters) : base (name, consumeParameters)
		{
			SaveInputParameters (inputParameters);

		}

		private void SaveInputParameters (Parameter[] inputParameters)
		{
			foreach (Parameter param in inputParameters) {
				InputParameters.Add (param.Name, param);
			}
		}

	

		//check if parameters compatible
		public void LinkTo (params ISourceNode<Parameter>[] sources)
		{
			//check if all are matching
			int matchings = 0;
			int longestSourceSequence = 1;
			foreach (ISourceNode<Parameter> source in sources) {
				String sourceParamName = source.GetOutputParameter ().Name;
				if (InputParameters.ContainsKey (sourceParamName)) {
					if (source.GetOutputParameter ().Match (InputParameters [sourceParamName])) {
						longestSourceSequence = Math.Max (source.GetSize (), longestSourceSequence);
						matchings++;
					} else {
						Debug.Log ("parameter type mismatch." + source.GetOutputParameter ().GetType ());
					}
				} else {
					Debug.Log ("parameter name mismatch." + source.GetOutputParameter ().Name);
				}
			
			}
			//zip if all sources match
			//default values allowed
			//if (matchings == sources.Count ()) {
			var zip = sources.Select ((source) => (source.AsObservable (longestSourceSequence))).ToArray ();
			var parameters = Observable.Zip<Parameter> (zip);

			var obs = parameters.Select ((list) => {
				ConsumeParameters (list);
				return Unit.Default;
			}).Publish ();
			//start hot observable on first received parameter
			parameters.Take (2).Subscribe ((list) => {
				ConsumeParameters (list);
				obs.Connect ();
				Debug.Log ("test");
			});

		}

		public Dictionary<String,Parameter> GetInputParameters ()
		{
			return InputParameters;
		}

		public abstract void ConsumeParameters (IList<Parameter> parameters);
	

		//get each source of a parameter as an observable and subscribe
		//when all sources finished consumeParameters


	


	}
}


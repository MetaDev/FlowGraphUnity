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

namespace Graph
{
	public abstract class TargetNode : Node, ITargetNode
	{
		

		protected Dictionary<String,Parameter> InputParameters = new Dictionary<String,Parameter> ();

		protected Dictionary<String,IDisposable> InputSourcesConnection = new Dictionary<String,IDisposable> ();

		public TargetNode (string name) : base (name)
		{
		}

		public TargetNode (string name, Action consumeParameters) : base (name, consumeParameters)
		{
		}


		//check if parameters compatible
		public void LinkTo (params ISourceNode<Parameter>[] sources)
		{
			//check if all are matching
			int matchings = 0;
			int maxSourceSize = 1;
			foreach (ISourceNode<Parameter> source in sources) {
				String sourceParamName = source.GetOutputParameter ().Name;
				if (InputParameters.ContainsKey (sourceParamName)) {
					if (source.GetOutputParameter ().Match (InputParameters [sourceParamName])) {
						maxSourceSize = Math.Max (source.GetSize (), maxSourceSize);
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
			Debug.Log (sources.Length);
			var parameters = Observable.Zip<Parameter> (sources.Select ((source) => ((IObservable<Parameter>)source.AsObservable (maxSourceSize))));
			//due to cast, we have to reconvert to hot observable
			var hotParameters = parameters.Publish ();
			hotParameters.Subscribe ((list) => {
				//copy parameters
				foreach (Parameter param in list) {
					this.InputParameters [param.Name].Copy (param);
				}
				//consume them
				Complete ();
			});
			//sources.Select ((source) => (source.AsObservable (maxSourceSize))).
			//}

		}



		//get each source of a parameter as an observable and subscribe
		//when all sources finished consumeParameters


	


	}
}


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

namespace Graph
{
	public abstract class TargetNode : Node, ITargetNode
	{
		

		Dictionary<String,Parameter> InputParameters;
		Dictionary<String,SourceNode> InputSources;

		public TargetNode (string name) : base (name)
		{
			this.InputParameters = new Dictionary<String,Parameter> ();
			InputSources = new Dictionary<String,SourceNode> ();
		}
		//check if parameters compatible
		public void LinkTo (SourceNode source, Parameter targetedParameter)
		{
			//check if targeted parameter matches
			if (targetedParameter.Match (source.GetOutputParameter ())) {
				InputSources.Add (targetedParameter.Name, source);
			}

		}

		public Parameter GetInputParameter (String parameterName)
		{
			if (InputParameters.ContainsKey (parameterName)) {
				return InputParameters [parameterName];
			}	
			return null;
		}
		//get each source of a parameter as an observable and subscribe
		//when all sources finished consumeParameters
		public override void Complete ()
		{
			Observable.WhenAll<Parameter> (InputSources.Values.Select (source => {
				source.Complete ();
				return source.AsObservable ();
			})).Subscribe (sourceParameters => {
				//copy source paramters
				//TODO
			},
				(er) => Debug.Log (er),
				() => {
					this._Process = ConsumeParameters ();
					Process ().Start ();
				}
			);
		
		}


		public abstract Task ConsumeParameters ();

	


	}
}


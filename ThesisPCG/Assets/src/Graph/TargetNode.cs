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
	public class TargetNode : Node, ITargetNode
	{
		

		Dictionary<String,Parameter> InputParameters;

		Dictionary<String,ISourceNode<Parameter>> InputSources;

		public TargetNode (string name) : base (name)
		{
			init ();
		}

		public TargetNode (string name, Action consumeParameters) : base (name, consumeParameters)
		{
			init ();
		}

		private void init ()
		{
			this.InputParameters = new Dictionary<String,Parameter> ();
			InputSources = new Dictionary<String,ISourceNode<Parameter>> ();
		}

		//check if parameters compatible
		public void LinkTo (ISourceNode<Parameter> source)
		{
			//check if targeted parameter matches
			if (source.GetOutputParameter ().Match (source.GetOutputParameter ())) {
				InputSources.Add (source.GetOutputParameter ().Name, source);

			}

		}

		public void AddInputParameter (Parameter inputParameter)
		{
			InputParameters [inputParameter.Name] = inputParameter;
		}

		public Parameter GetInputParameter (String parameterName)
		{
			if (InputParameters.ContainsKey (parameterName)) {
				return InputParameters [parameterName];
			} else {
				Debug.Log ("Tring to acces unavailable parameter in Target");
				return null;
			}
		}

		public T GetInputParameter<T> (String parameterName) where T : Parameter
		{
			if (InputParameters.ContainsKey (parameterName)) {
				return InputParameters [parameterName].AsTypedParameter<T> ();
			} else {
				Debug.Log ("Tring to acces unavailable parameter in Target");
				return null;
			}
		}
		//get each source of a parameter as an observable and subscribe
		//when all sources finished consumeParameters
		public override void Complete ()
		{
			Observable.WhenAll<Parameter> (InputSources.Values.Select (source => {
				return source.AsObservable ();
			})).Subscribe (sourceParameters => {
				//copy all values form parameters
				foreach (Parameter sourceParameter in sourceParameters) {
					InputParameters [sourceParameter.Name].Copy (sourceParameter);
				}
				//consume them
				Complete ();
			}
			);
		
		}

	


	}
}


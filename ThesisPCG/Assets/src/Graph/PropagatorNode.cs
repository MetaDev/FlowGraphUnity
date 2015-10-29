using System.Threading.Tasks;
using System;
using Graph;
using UniRx;
using Graph.Parameters;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using UnityEngine;
using System.Runtime.InteropServices;

namespace Graph
{
	public abstract class PropagatorNode : Node,IPropagatorNode<Parameter>
	{
		private TargetNode Target;

		private SourceNode Source;

		public PropagatorNode (string name, TargetNode target, SourceNode source) : base (name)
		{
			this.Target = target;
			this.Source = source;
		}





		public Parameter GetOutputParameter ()
		{
			return Source.GetOutputParameter ();
		}

		IConnectableObservable<Parameter> obs;

		//Calculate ouputparameter of the node with inputparameters from the target

	
		
		public abstract Parameter Transform ();

		public void LinkTo (params ISourceNode<Parameter>[] sources)
		{
			//check if all are matching
			int matchings = 0;
			int longestSourceSequence = 1;
			foreach (ISourceNode<Parameter> source in sources) {
				String sourceParamName = source.GetOutputParameter ().Name;
				if (Target.GetInputParameters ().ContainsKey (sourceParamName)) {
					if (source.GetOutputParameter ().Match (Target.GetInputParameters () [sourceParamName])) {
						longestSourceSequence = Math.Max (source.GetSize (), longestSourceSequence);
						matchings++;
					} else {
						Debug.Log ("parameter type mismatch." + source.GetOutputParameter ().GetType ());
					}
				} else {
					Debug.Log ("parameter name mismatch." + source.GetOutputParameter ().Name);
				}
			
			}
			Source.Size = longestSourceSequence;
			//zip if all sources match
			//default values allowed
			//if (matchings == sources.Count ()) {
			var zip = new List<IConnectableObservable<Parameter>> ();
			foreach (ISourceNode<Parameter> t in sources) {
				zip.Add (t.AsObservable (longestSourceSequence));
			}
			var tets = zip.ToArray ();
			var parameters = Observable.Zip<Parameter> (tets);
			//transform 
			obs = parameters.Select ((list) => {
				ConsumeParameters (list);
				return GetOutputParameter ();
			}).Publish ();
			//start hot observable on first received parameter
			parameters.Take (1).Do ((list) => {
				obs.Connect ();
			});

		
		}

		public IConnectableObservable<Parameter> AsObservable ()
		{
			return obs;
		}

		public IConnectableObservable<Parameter> AsObservable (int size)
		{
			throw new NotImplementedException ();
		}




		public void ConsumeParameters (IList<Parameter> parameters)
		{
			TransformParameter (parameters, GetOutputParameter ());
		}

		public abstract Parameter TransformParameter (IList<Parameter> inputParameters, Parameter outputParameter);

		public int GetSize ()
		{
			return Source.GetSize ();
		}


	}
}


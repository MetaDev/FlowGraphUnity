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
		
		private Dictionary<String,Parameter> InputParameters = new Dictionary<String,Parameter> ();

		private int Size;

		//the source node port
		private  Parameter OutputParameter;

		public PropagatorNode (string name, Parameter outputParameter, params Parameter[] inputParameters) : base (name)
		{
			this.OutputParameter = outputParameter;
			TargetNode.SaveInputParameters (InputParameters, inputParameters);
		}





		public Parameter GetOutputParameter ()
		{
			return OutputParameter;
		}

		IObservable<Parameter> obs;


	
		

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
			this.Size = longestSourceSequence;
			//zip if all sources match
			//default values allowed
			//if (matchings == sources.Count ()) {
			var zip = new List<IObservable<Parameter>> ();
			foreach (ISourceNode<Parameter> t in sources) {
				zip.Add (t.AsObservable (longestSourceSequence));
			}
			var tets = zip.ToArray ();
			var parameters = Observable.Zip<Parameter> (tets);
			//transform 
			obs = parameters.Select ((list) => {
				//side effect, cache output value
				GetOutputParameter ().Copy (TransformParameter (list));
				//transmit to next source
				return GetOutputParameter ();
			});
			//the source is a hot observable thus subcribing to the transformed zip won't output until sources output
			obs.Subscribe ();
		}

		public IObservable<Parameter> AsObservable ()
		{
			return obs;
		}

		public IObservable<Parameter> AsObservable (int size)
		{
			//check size
			if (size % Size != 0) {
				throw new ArgumentOutOfRangeException ("The size of the sequence doesnt match the requested sequence size");
			}
			var repeatSize = size / this.Size;
			return obs.Repeat<Parameter> ().Take (repeatSize * Size);
		}


		//Calculate ouputparameter of the node with inputparameters from the target
		//save new value in outputparameter and retrun it
		protected abstract Parameter TransformParameter (IList<Parameter> inputParameters);

		public int GetSize ()
		{
			return Size;
		}


	}
}


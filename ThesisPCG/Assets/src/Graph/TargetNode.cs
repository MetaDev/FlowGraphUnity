using System;
using Graph;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UniRx;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;

namespace Graph
{
	public abstract class TargetNode : ITargetNode
	{
		Dictionary<String,Parameter> InputParameters;
		Dictionary<String,ISourceNode> InputSources;

		public TargetNode ()
		{
			InputParameters = new Dictionary<String,Parameter> ();
			InputSources = new Dictionary<String,ISourceNode> ();
		}
		//check if parameters compatible
		public void LinkTo (ISourceNode source, Parameter sourceParameter, String targetedParameter)
		{
			//check if targeted parameter exists
			if (InputParameters.ContainsKey (targetedParameter)) {
				
				if (InputParameters [targetedParameter].IsType (sourceParameter)) {
					InputSources.Add (targetedParameter, source);
				}
			}

		}
		//get each source of a parameter as an observable and subscribe
		public void Pull ()
		{
			/*List<Observable> list =
			foreach(DictionaryEntry entry in InputParameters){
				if(InputSources.ContainsKey(entry.Key)){
					
				}
			}
			 InputSources.Values.ToList ().Select (source => {
				
				source.AsObservable<> ();}).ToList ();
			return list ();*/
		}


		//do something with the available sources
		public abstract Task Completion ();


		public void Lock (float lockingFactor)
		{
			throw new NotImplementedException ();
		}
	}
}


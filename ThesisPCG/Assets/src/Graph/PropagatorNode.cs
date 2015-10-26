using System.Threading.Tasks;
using System;
using Graph;
using UniRx;
using Graph.Parameters;
using System.Collections.Generic;
using System.CodeDom.Compiler;

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
		public PropagatorNode (string name,Parameter outputParameter) : base (name)
		{
			this.Target = new TargetNode(name);
			this.Source = new SourceNode(name,outputParameter);
		}




		public Parameter GetOutputParameter ()
		{
			return Source.GetOutputParameter ();
		}
		public T GetOutputParameter<T> () where T : Parameter
		{
			return Source.GetOutputParameter<T> ();
		}
		public Parameter GetInputParameter (String parameterName)
		{
			return Target.GetInputParameter (parameterName);
		}

		public void LinkTo (ISourceNode<Parameter> source)
		{
			Target.LinkTo (source);
		}

		//Calculate ouputparameter of the node with inputparameters from the target

		public IObservable<Parameter> AsObservable ()
		{
			return Observable.Start (() => {
				Complete ();
				return Source.GetOutputParameter ();
			});
		}

		public T GetInputParameter<T> (String parameterName) where T : Parameter
		{
			return Target.GetInputParameter<T> (parameterName);
		}

		public void AddInputParameter (Parameter inputParameter)
		{
			Target.AddInputParameter (inputParameter);
		}

		public override void Complete ()
		{
			// the target part of the node completes to load neccesary sources
			Target.Complete ();
			Transform();
			Source.Complete ();

		}
		protected abstract void Transform();

		//to override method that process' all data from source andsaves it in the input parameters of the target


	}
}


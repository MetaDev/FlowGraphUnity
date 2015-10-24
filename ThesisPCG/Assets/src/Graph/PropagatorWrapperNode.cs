using System.Threading.Tasks;
using System;
using Graph;
using UniRx;
using Graph.Parameters;

namespace AssemblyCSharp
{
	public abstract class PropagatorWrapperNode : Node,IPropagatorNode
	{
		readonly ITargetNode Target;
		readonly ISourceNode Source;


		public PropagatorWrapperNode (ITargetNode target, ISourceNode source)
		{
			if (target == null)
				throw new ArgumentNullException ("target");
			if (source == null)
				throw new ArgumentNullException ("source");

			this.Target = target;
			this.Source = source;
		}

		public Parameter GetOutputParameter ()
		{
			return Source.GetOutputParameter ();
		}

		public Parameter GetInputParameter (String parameterName)
		{
			return Target.GetInputParameter (parameterName);
		}

		public void LinkTo (ISourceNode source, Parameter targetedParameter)
		{
			Target.LinkTo (source, targetedParameter);
		}

		public Task ConsumeParameters ()
		{
			return Target.ConsumeParameters ();
		}

		public IObservable<Parameter> AsObservable ()
		{
			return Source.AsObservable ();
		}

	

		public override void Complete ()
		{
			//fill data in parameters of source
			Target.Complete ();
			//continue with transformation of parameters from source node
			Target.Completion ().ContinueWith ((t) => {
				this.Process = Transformation ();
				this.Process.Start ();
			});
		

		}
		//to override method that process' all data from source andsaves it in the input parameters of the target
		public abstract Task Transformation ();


	}
}


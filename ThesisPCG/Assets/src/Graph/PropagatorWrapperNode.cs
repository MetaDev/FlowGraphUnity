using System;
using Graph;
using UniRx;

namespace AssemblyCSharp
{
	public abstract class PropagatorWrapperNode : IPropagatorNode
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

		public void LinkTo (SourceNode source, String parameterName)
		{
			throw new NotImplementedException ();
		}



		public UniRx.IObservable<object> AsObservable ()
		{
			throw new NotImplementedException ();
		}

		public T Result<T> ()
		{
			throw new NotImplementedException ();
		}

		public bool IsOutputType (Type t)
		{
			throw new NotImplementedException ();
		}

		public Action Completion ()
		{
			throw new NotImplementedException ();
		}

		public void Complete ()
		{
			throw new NotImplementedException ();
		}

		public void Lock (float lockingFactor)
		{
			throw new NotImplementedException ();
		}
	}
}


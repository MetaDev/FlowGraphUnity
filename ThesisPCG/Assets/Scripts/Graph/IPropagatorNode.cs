using System.Collections;
using Graph.Parameters;

namespace Graph
{
	//the reason we use multiple interfaces is to be able to define such a propagator
	public interface IPropagatorNode<Tout>: ISourceNode<Tout>, ITargetNode where Tout: Parameter
	{
	}
}

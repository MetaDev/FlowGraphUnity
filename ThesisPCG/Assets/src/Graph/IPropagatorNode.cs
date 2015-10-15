using System.Collections;

namespace Graph
{
	//the reason we use multiple interfaces is to be able to define such a propagator
	public interface IPropagatorNode: ISourceNode, ITargetNode
	{


	}
}

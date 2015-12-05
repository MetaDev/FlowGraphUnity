using UniRx;
using Graph.Parameters;


namespace Graph
{
	public interface ISourceNode<Tout> : INode where Tout : Parameter
	{
        //return source as observable based on its output parameter
        //maybe use generic observable to hide casting inside sourcenode,
        IObservable<Tout> AsObservable ();
        //int GetSize ();

		Tout PortParameter ();

	}

}
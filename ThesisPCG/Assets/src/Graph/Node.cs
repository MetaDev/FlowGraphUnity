using System.Threading.Tasks;

namespace Graph
{
	public abstract class Node : INode
	{
		
		//saved ongoing process
		protected Task Process;

		protected int _NodeType;

		public Task Completion ()
		{
			return Process;
		}

		public abstract void Complete ();

		//for inheritance purpose we use classes to define the Node Type
		public static class NodeType
		{
			public const int GENERATOR = 1;
			public const int DATA = 2;
		}
	}
}


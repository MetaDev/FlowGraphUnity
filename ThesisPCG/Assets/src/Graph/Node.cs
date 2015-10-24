using System.Threading.Tasks;

namespace Graph
{
	public abstract class Node : INode
	{


		//saved ongoing process
		protected Task _Process;

		public Task Process ()
		{
			return _Process;
		}

		protected int _NodeType;

		public string Name{ get; }

		public Node (string name)
		{
			this.Name = name;
		}



		public abstract void Complete ();

		//for inheritance purpose we use classes to define the Node Type
		public class NodeType
		{
			public const int GENERATOR = 1;
			public const int DATA = 2;
		}
	}
}


using System.Threading.Tasks;

namespace Graph
{
	public abstract class Node : INode
	{
		
		//saved ongoing process
		protected Task Process;

		public Task Completion ()
		{
			return Process;
		}

		public abstract void Complete ();
	}
}


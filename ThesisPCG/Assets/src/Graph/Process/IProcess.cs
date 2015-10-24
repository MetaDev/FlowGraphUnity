using System;
using System.Threading.Tasks;

namespace Graph.Process
{
	public interface IProcess
	{
		//Gets a Task that represents the asynchronous operation and completion of the dataflow node.
		Task Completion ();
		//After Complete has been called on a dataflow block, that block will complete,
		//and its Completion task will enter a final state after it has processed all previously available data.
		//Complete will not block waiting for completion to occur, but rather will initiate the request; to wait for completion to occur, the Completion task may be used.

		//this method has to be overriden to create the new task
		void Complete ();

	}
}


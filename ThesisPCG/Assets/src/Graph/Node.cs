using System;
using UnityEngine;

namespace Graph
{
	public abstract class Node : INode
	{

		protected int _NodeType;

		public string Name{ get; private set; }

		public Node (string name)
		{
			this.Name = name;
		}



		//public abstract void Complete ();
		//for inheritance purpose we use classes to define the Node Type
		public class NodeType
		{
			public const int GENERATOR = 1;
			public const int DATA = 2;
		}
	}
}


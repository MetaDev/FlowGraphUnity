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

	}
}


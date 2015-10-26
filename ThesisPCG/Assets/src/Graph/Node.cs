using System;
using UnityEngine;

namespace Graph
{
	public abstract class Node : INode
	{

		protected int _NodeType;
		protected Action _Complete;

		public string Name{ get; private set; }

		public Node (string name)
		{
			this.Name = name;
		}

		public Node (string name, Action complete)
		{
			this.Name = name;
			this._Complete = complete;
		}



		public virtual void Complete ()
		{
			if (_Complete != null) {
				_Complete ();
			} else {
				Debug.Log ("No completion method assigned.");
			}

		}

		//for inheritance purpose we use classes to define the Node Type
		public class NodeType
		{
			public const int GENERATOR = 1;
			public const int DATA = 2;
		}
	}
}


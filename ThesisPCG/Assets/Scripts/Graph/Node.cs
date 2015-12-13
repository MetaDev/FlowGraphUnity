using System;
using UnityEngine;

namespace Graph
{
	public abstract class Node : INode
	{


        private string _Name;
		public string GetName(){
            return _Name;
        }

		public Node (string name)
		{
			this._Name = name;
		}

	}
}


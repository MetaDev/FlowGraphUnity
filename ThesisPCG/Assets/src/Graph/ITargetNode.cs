using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using System.Threading.Tasks;
using System;
using Graph.Parameters;

namespace Graph
{
	public interface ITargetNode : INode
	{
		void LinkTo (params ISourceNode<Parameter>[] sources);
	}
}

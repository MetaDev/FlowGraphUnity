using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UniRx;

namespace Graph
{
	public interface ITargetNode : INode
	{
		void LinkTo (ISourceNode source, string parameterName);

		void Pull ();


	}
}

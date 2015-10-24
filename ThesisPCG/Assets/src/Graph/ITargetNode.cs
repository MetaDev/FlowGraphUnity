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
		void LinkTo (ISourceNode source, Parameter targetedParameter);

		Task ConsumeParameters ();

		Parameter GetInputParameter (String parameterName);
	}
}

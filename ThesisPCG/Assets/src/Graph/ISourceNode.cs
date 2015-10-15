using System.Collections;
using UniRx;
using System.Reflection;
using System;
using UnityEditor;
using System.Threading.Tasks;


namespace Graph
{
	public interface ISourceNode : INode
	{
		//return source as observable based on its output parameter
		//maybe use generic observable to hide casting inside sourcenode,
		IObservable<T> AsObservable<T> (string outputParameter);

		//check whether the output
		bool IsType<T> (string outputParameter);

	}

}
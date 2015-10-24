using System;
using UniRx;
using System.Threading.Tasks;

namespace Graph.Parameters
{
	public interface IReactiveParameter<T> : ISubject<T> , IParameter<T>
	{
	}
}


using System;

namespace Graph.Parameters
{
	public interface I1DParameter<T> : IParameter<T[]>
	{
        T GetValue(int i);

        void SetValue(int i, T value);
    }
}


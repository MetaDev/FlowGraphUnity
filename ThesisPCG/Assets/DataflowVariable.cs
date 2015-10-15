using System;
using System.Threading;

namespace AssemblyCSharp
{
	public class DataflowVariable<T>
	{
		private readonly object syncLock = new object();
		private volatile bool isInitialized = false;
		private volatile object value;

		private T Value
		{
			get
			{
				if(!isInitialized)
				{
					lock(syncLock)
					{
						while(!isInitialized)
							Monitor.Wait(syncLock);
					}
				}
				return (T)value;
			}
			set
			{
				lock (syncLock)
				{
					if (isInitialized)
						throw new System.InvalidOperationException
						("Dataflow variable can be set only once.");
					else
					{
						this.value = value;
						isInitialized = true;
						Monitor.PulseAll(syncLock);
					}
				}
			}
		}

		public void Bind(T newValue)
		{
			this.Value = newValue;
		}

		public static implicit operator T(DataflowVariable<T> myVar)
		{
			return myVar.Value;
		}
	}
}


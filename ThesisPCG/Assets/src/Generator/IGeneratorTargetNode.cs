using System.Collections;
using System.Collections.Concurrent;
using System;
using Graph;
using System.Threading.Tasks;

namespace Generator
{
	public abstract class IGeneratorTargetNode : TargetNode
	{
		

		public override Task ConsumeParameters ()
		{
			//do something with params from source
			//and generate shit
			return new Task (() => {
			});
		}



	}
}

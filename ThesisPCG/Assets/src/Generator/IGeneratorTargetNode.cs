using System.Collections;
using System.Collections.Concurrent;
using System;
using Graph;
using System.Threading.Tasks;
using Graph.Parameters;
using System.Collections.Generic;

namespace Generator
{
	public abstract class IGeneratorTargetNode : TargetNode
	{
		public IGeneratorTargetNode (string name) : base (name)
		{
		}


		public override Task ConsumeParameters ()
		{
			//do something with params from source
			//and generate shit
			return new Task (() => {
			});
		}



	}
}

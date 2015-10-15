using System.Collections;
using System.Collections.Concurrent;
using System;
using Graph;
using System.Threading.Tasks;

namespace Generator
{
	public class IGeneratorTargetNode : TargetNode, ILabeledGenerator
	{
		//use the parameters available from the target node to generate graphics
		public  void Generate ()
		{
			Pull ();
			throw new NotImplementedException ();
		}


		public override Task Completion ()
		{
			return  new Task (() => this.Generate ());
		}



	}
}

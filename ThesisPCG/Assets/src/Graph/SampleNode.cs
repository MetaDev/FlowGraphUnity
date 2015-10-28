using System;
using Graph;
using System.Collections;
using Graph.Parameters;
using System.Collections.Generic;
using MathNet.Numerics;
using UniRx;
using System.Linq;

namespace Graph
{
	public class SampleNode : SourceNode
	{
		private int RangeX;
		private int RangeY;

		public SampleNode (int rangeX, int rangeY) : base ("Sample Node", rangeX * rangeY, new IntegerVector2Parameter ("Position"))
		{
			this.RangeX = rangeX;
			this.RangeY = rangeY;
		}


		public override void Complete ()
		{
			for (int x = 0; x < RangeX; x++) {
				for (int y = 0; y < RangeY; y++) {
					IntegerVector2Parameter param = new IntegerVector2Parameter ("Position");
					param.SetValue (new MathNet.Numerics.Tuple<int,int> (x, y));
					this.OutputParameterSequence.Add (param);
				}
			}
		}



		//the range could be loaded from a config file
		//the loading would than be done in complete

	}
}


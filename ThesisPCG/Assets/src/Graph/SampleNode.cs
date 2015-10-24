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
		IEnumerable<IntegerTupleParameter> Samples;

		public SampleNode (int rangeX, int rangeY) : base ("Sample Node")
		{
			Samples = MakeSamples (rangeX, rangeY);
			this.OutputParameter = new IntegerTupleParameter ("Map Position");
		}

		public static IEnumerable<IntegerTupleParameter> MakeSamples (int rangeX, int rangeY)
		{
			for (int x = 0; x < rangeX; x++) {
				for (int y = 0; y < rangeX; y++) {
					IntegerTupleParameter param = new IntegerTupleParameter ("Map Position");
					param.SetValue (new MathNet.Numerics.Tuple<int,int> (x, y));
					yield return param;
				}
			}
		}

		public override UniRx.IObservable<Parameter> AsObservable ()
		{
			return Observable.ToObservable (Samples.Cast<Parameter> ());
		}

		//the range could be loaded from a config file
		//the loading would than be done in complete
		public override void Complete ()
		{
			//do nothing
		}

	}
}


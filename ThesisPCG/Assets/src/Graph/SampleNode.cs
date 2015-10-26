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
		IEnumerable<IntegerVector2Parameter> Samples;

		public SampleNode (int rangeX, int rangeY) : base ("Sample Node", new IntegerVector2Parameter ("position"))
		{
			Samples = MakeSamples (rangeX, rangeY);
			this.OutputParameter = new IntegerVector2Parameter ("Map Position");
		}

		public static IEnumerable<IntegerVector2Parameter> MakeSamples (int rangeX, int rangeY)
		{
			for (int x = 0; x < rangeX; x++) {
				for (int y = 0; y < rangeX; y++) {
					IntegerVector2Parameter param = new IntegerVector2Parameter ("Map Position");
					param.SetValue (new MathNet.Numerics.Tuple<int,int> (x, y));
					yield return param;
				}
			}
		}

		public override IObservable<Parameter> AsObservable ()
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


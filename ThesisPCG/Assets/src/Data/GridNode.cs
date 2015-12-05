using System;
using Graph;
using System.Collections;
using Graph.Parameters;
using System.Collections.Generic;
using MathNet.Numerics;
using UniRx;
using System.Linq;
using UnityEngine;


namespace Data
{
	public class GridNode : SourceNode
	{
		private int RangeX;
		private int RangeY;
        private IntegerVector2Parameter Outputype = new IntegerVector2Parameter("Position");
        public GridNode(int rangeX, int rangeY) :base ("Gridnode")
		{
			this.RangeX = rangeX;
			this.RangeY = rangeY;
		}

        

        public override Parameter PortParameter()
        {
            return Outputype;
        }

        public override IEnumerable<Parameter> StreamParameter()
        {
            var s = new List<Parameter>();
            for (int x = 0; x < RangeX; x++)
            {
                for (int y = 0; y < RangeY; y++)
                {

                    IntegerVector2Parameter param = new IntegerVector2Parameter("Position");
                    param.SetValue(new MathNet.Numerics.Tuple<int, int>(x, y));
                    s.Add(param);
                }
            }
            return s;
        }
    }
}


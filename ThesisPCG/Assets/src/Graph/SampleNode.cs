using System;
using Graph;
using System.Collections;
using Graph.Parameters;
using System.Collections.Generic;
using MathNet.Numerics;
using UniRx;
using System.Linq;
using UnityEngine;

namespace Graph
{
	public class SampleNode : SourceNode
	{
		private int RangeX;
		private int RangeY;
        private Parameter OutputYpe = new IntegerVector2Parameter("Position");
        public SampleNode (int rangeX, int rangeY) : base ("Sample Node", rangeX * rangeY)
		{
			this.RangeX = rangeX;
			this.RangeY = rangeY;
		}

        public override Parameter GetOutputParameterType()
        {
            return OutputYpe;
        }

        public override Parameter[] LoadParameters ()
		{
            IntegerVector2Parameter[] parameters = new IntegerVector2Parameter[this.Size];
            int i = 0;		
			for (int x = 0; x < RangeX; x++) {
				for (int y = 0; y < RangeY; y++) {

					IntegerVector2Parameter param = new IntegerVector2Parameter ("Position");
					param.SetValue (new MathNet.Numerics.Tuple<int,int> (x, y));
					parameters [i] = param;
					i++;
				}
			}
            return parameters;
		}


	}
}


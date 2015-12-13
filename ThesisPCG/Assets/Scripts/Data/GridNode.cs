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
    //return normalised resolution
	 class GridNode : FiniteSourceNode
	{
	
        private int Resolution;

        public GridNode(string OutputName, int resolution) :base ("Gridnode: " +  OutputName, new Vector3fParameter(OutputName))
		{
		
            this.Resolution = resolution;

        }


        public override IEnumerable<Parameter> CreateSequence()
        {
            var s = new List<Parameter>();
           
            
            for (float x = 0; x < Resolution; x++)
            {
                for (float y = 0; y < Resolution; y++)
                {
                    Vector3fParameter param = new Vector3fParameter(this.OutputParameter().Name,new Vector3( x/Resolution,0,y / Resolution));
                    s.Add(param);
                }
            }
            return s;
        }
        //column mayor
        public override int[] GetShape()
        {
            return new int[] { Resolution, Resolution };
        }
    }
}


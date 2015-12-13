using System;
using Graph;
using Graph.Parameters;
using UnityEngine;
using System.Collections.Generic;
using UniRx;

namespace Generator
{
	public class BlockGenerator : TargetNode
	{
        private int blockSize;

		
		public BlockGenerator () : base ("Block", new Vector3fParameter("Position"), new DoubleParameter("Height"), new ColorParameter("Color"))
		{
		
		}

		public override void Sink(IObservable<IList<Parameter>> observable)
		{
            //
            observable.Subscribe((parameters) =>
            {
                Vector3fParameter position = TargetNode.GetParameterFromList("Position", parameters).As<Vector3fParameter>();
                ColorParameter color = TargetNode.GetParameterFromList("Color", parameters).As<ColorParameter>();
                DoubleParameter height = TargetNode.GetParameterFromList("Height", parameters).As<DoubleParameter>();
                //set position
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = new Vector3(position.GetValue().x, (float)height.GetValue(), position.GetValue().z);
                //set color
                cube.GetComponent<Renderer>().material.color = color.GetValue();
            });
          
            
		}
		
	}
}


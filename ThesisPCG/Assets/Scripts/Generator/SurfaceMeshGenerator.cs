using System;
using System.Collections.Generic;
using Graph;
using Graph.Parameters;
using UnityEngine;
using UniRx;


namespace Generator
{
    class SurfaceMeshGenerator : TargetNode
    {
   
        //temporary vars to do calculation
        private int Resolution;
        


        public SurfaceMeshGenerator(int resolution, string name = "") : base("SurfaceMesh" + name, new Vector3fParameter("Position"), new DoubleParameter("Height"), new ColorParameter("Color"))
        {

            this.Resolution = resolution;


        }
        private void CreateMesh(List<float> heightList, List<Vector2> pointsOfHeightList)
        {


            GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
            MeshFilter meshFilter = (MeshFilter)plane.GetComponent<MeshFilter>();
            Transform transform = (Transform)plane.GetComponent<Transform>();

            MeshRenderer renderer = plane.GetComponent(typeof(MeshRenderer)) as MeshRenderer;
            Debug.Log(heightList.Count + " " + pointsOfHeightList.Count);
            //create mesh
          

        }
        public override void Sink(IObservable<IList<Parameter>> observable)
        {
            var heights = new List<float>();

            var points = new List<Vector2>();
            observable.Subscribe((parameters) =>
            {
                Vector3fParameter position = TargetNode.GetParameterFromList("Position", parameters).As<Vector3fParameter>();
                
                DoubleParameter height = TargetNode.GetParameterFromList("Height", parameters).As<DoubleParameter>();

                heights.Add((float)height.GetValue());
                points.Add(new Vector2(position.GetValue().x, position.GetValue().z));
               
            }, () =>
            {
                CreateMesh(heights, points);

            });
        }

    }
}

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
        private Mesh Mesh;
        //temporary vars to do calculation
        private int Resolution;
        private Vector3[] vertices;
        private Vector3[] normals;


        public SurfaceMeshGenerator(int resolution, string name = "") : base("SurfaceMesh" + name, new Vector3fParameter("Position"), new DoubleParameter("Height"), new ColorParameter("Color"))
        {

            this.Resolution = resolution;


        }
        private void CreateMesh(List<double> heightList)
        {

            Mesh = new Mesh();
            GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
            MeshFilter meshFilter = (MeshFilter)plane.GetComponent<MeshFilter>();
            Transform transform = (Transform)plane.GetComponent<Transform>();


            meshFilter.mesh = Mesh;

            MeshRenderer renderer = plane.GetComponent(typeof(MeshRenderer)) as MeshRenderer;
            // renderer.material.shader = Shader.Find("Custom/Surface Shader");
            var vertices = new Vector3[(Resolution + 1) * (Resolution + 1)];
            Vector2[] uv = new Vector2[vertices.Length];

            for (int i = 0, y = 0; y <= Resolution; y++)
            {
                for (int x = 0; x <= Resolution; x++, i++)
                {
                    float height = 0;
                    if(i < heightList.Count - 1)
                    {
                        height = (float)heightList[i];
                    }
                    vertices[i] = new Vector3(x, height, y);
                    uv[i] = new Vector2((float)x / Resolution, (float)y / Resolution);
                }
            }

            int[] triangles = new int[Resolution * Resolution * 6];
            for (int ti = 0, vi = 0, y = 0; y < Resolution; y++, vi++)
            {
                for (int x = 0; x < Resolution; x++, ti += 6, vi++)
                {
                    triangles[ti] = vi;
                    triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                    triangles[ti + 4] = triangles[ti + 1] = vi + Resolution + 1;
                    triangles[ti + 5] = vi + Resolution + 2;
                }
            }

            Mesh.vertices = vertices;
            Mesh.uv = uv;
            Mesh.triangles = triangles;
            Mesh.RecalculateNormals();

            // Mesh.Optimize();
        }
        public override void Sink(IObservable<IList<Parameter>> observable)
        {
            var heights = new List<double>();
            var colors = new List<Color>();
            var uvs = new List<Vector2>();
            observable.Subscribe((parameters) =>
            {
                Vector3fParameter position = TargetNode.GetParameterFromList("Position", parameters).As<Vector3fParameter>();
                ColorParameter color = TargetNode.GetParameterFromList("Color", parameters).As<ColorParameter>();
                DoubleParameter height = TargetNode.GetParameterFromList("Height", parameters).As<DoubleParameter>();

                heights.Add(height.GetValue());

                colors.Add(color.GetValue());
            }, () =>
            {
                CreateMesh(heights);

            });
        }

    }
}

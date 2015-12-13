﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Generator;
using Graph;
using UniRx;
using Graph.Parameters;

namespace Generator
{
     class BezierMesh 
    {
        public Mesh mesh;
        public List<Vector2> uvs;


        //Calculate UVs for our tessellated vertices
        private Vector2 BezCurveUV(float t, Vector2 p0, Vector2 p1, Vector2 p2)
        {
            Vector2 bezPoint = new Vector2();

            float a = 1f - t;
            float tt = t * t;

            float[] tPoints = new float[2];
            for (int i = 0; i < 2; i++)
            {
                tPoints[i] = ((a * a) * p0[i]) + (2 * a) * (t * p1[i]) + (tt * p2[i]);
            }

            bezPoint.Set(tPoints[0], tPoints[1]);

            return bezPoint;
        }

        // Calculate a vector3 at point t on a bezier curve between
        // p0 and p2 via p1.  
        private Vector3 BezCurve(float t, Vector3 p0, Vector3 p1, Vector3 p2)
        {
            Vector3 bezPoint = new Vector3();

            float a = 1f - t;
            float tt = t * t;

            float[] tPoints = new float[3];
            for (int i = 0; i < 3; i++)
            {
                tPoints[i] = ((a * a) * p0[i]) + (2 * a) * (t * p1[i]) + (tt * p2[i]);
            }

            bezPoint.Set(tPoints[0], tPoints[1], tPoints[2]);

            return bezPoint;
        }

        // This takes a tessellation level and three vector3
        // p0 is start, p1 is the midpoint, p2 is the endpoint
        // The returned list begins with p0, ends with p2, with
        // the tessellated verts in between.
        private List<Vector3> Tessellate(int level, Vector3 p0, Vector3 p1, Vector3 p2)
        {
            List<Vector3> vects = new List<Vector3>();

            float stepDelta = 1.0f / level;
            float step = stepDelta;

            vects.Add(p0);
            for (int i = 0; i < (level - 1); i++)
            {
                vects.Add(BezCurve(step, p0, p1, p2));
                step += stepDelta;
            }
            vects.Add(p2);
            return vects;
        }

        // Same as above, but for UVs
        private List<Vector2> TessellateUV(int level, Vector2 p0, Vector2 p1, Vector2 p2)
        {
            List<Vector2> vects = new List<Vector2>();

            float stepDelta = 1.0f / level;
            float step = stepDelta;

            vects.Add(p0);
            for (int i = 0; i < (level - 1); i++)
            {
                vects.Add(BezCurveUV(step, p0, p1, p2));
                step += stepDelta;
            }
            vects.Add(p2);
            return vects;
        }

      
        // Where the magic happens.
        public void CreateBezierMesh(int level, List<Vector3> control, List<Vector2> controlUvs)
        {
         
            // The mesh we're building
            Mesh mesh = new Mesh();
            mesh.name = "BSPmesh (bez)";
            GameObject plane = new GameObject("SurfaceMesh");
            MeshFilter meshFilter = (MeshFilter)plane.AddComponent(typeof(MeshFilter));
            meshFilter.mesh = mesh;

            MeshRenderer renderer = plane.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
            renderer.material.shader = Shader.Find("Custom/Surface Shader");

            // We'll use these two to hold our verts, tris, and uvs
            List<Vector3> vertex = new List<Vector3>();
            List<int> index = new List<int>();
            List<Vector2> uvs = new List<Vector2>();

            // The incoming list is 9 entires, 
            // referenced as p0 through p8 here.

            // Generate extra rows to tessellate
            // each row is three control points
            // start, curve, end
            // The "lines" go as such
            // p0s from p0 to p3 to p6 ''
            // p1s from p1 p4 p7
            // p2s from p2 p5 p8

            List<Vector2> p0suv;
            List<Vector3> p0s;
            p0s = Tessellate(level, control[0], control[3], control[6]);
            p0suv = TessellateUV(level, controlUvs[0], controlUvs[3], controlUvs[6]);

            List<Vector2> p1suv;
            List<Vector3> p1s;
            p1s = Tessellate(level, control[1], control[4], control[7]);
            p1suv = TessellateUV(level, controlUvs[1], controlUvs[4], controlUvs[7]);

            List<Vector2> p2suv;
            List<Vector3> p2s;
            p2s = Tessellate(level, control[2], control[5], control[8]);
            p2suv = TessellateUV(level, controlUvs[2], controlUvs[5], controlUvs[8]);

            // Tessellate all those new sets of control points and pack
            // all the results into our vertex array, which we'll return.
            // Make our uvs list while we're at it.
            for (int i = 0; i <= level; i++)
            {
                vertex.AddRange(Tessellate(level, p0s[i], p1s[i], p2s[i]));
                uvs.AddRange(TessellateUV(level, p0suv[i], p1suv[i], p2suv[i]));
            }

            // This will produce (tessellationLevel + 1)^2 verts
            int numVerts = (level + 1) * (level + 1);

            // Computer triangle indexes for forming a mesh.
            // The mesh will be tessellationlevel + 1 verts
            // wide and tall.
            int xStep = 1;
            int width = level + 1;
            for (int i = 0; i < numVerts - width; i++)
            {
                //on left edge
                if (xStep == 1)
                {
                    index.Add(i);
                    index.Add(i + width);
                    index.Add(i + 1);

                    xStep++;
                    continue;
                }
                else if (xStep == width) //on right edge
                {
                    index.Add(i);
                    index.Add(i + (width - 1));
                    index.Add(i + width);

                    xStep = 1;
                    continue;
                }
                else // not on an edge, so add two
                {
                    index.Add(i);
                    index.Add(i + (width - 1));
                    index.Add(i + width);


                    index.Add(i);
                    index.Add(i + width);
                    index.Add(i + 1);

                    xStep++;
                    continue;
                }
            }

            // Add the verts and tris
            mesh.vertices = vertex.ToArray();
            mesh.triangles = index.ToArray();
            mesh.uv = uvs.ToArray();
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();
            mesh.Optimize();

            //Return the mesh! Shazam!
            this.mesh = mesh;
        }

    }
}
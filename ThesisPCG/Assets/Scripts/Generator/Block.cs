using UnityEngine;


namespace Generator
{

    [RequireComponent(typeof(Renderer),typeof(Transform), typeof(MeshFilter))]
    class Block : MonoBehaviour
    {
        private Renderer rend;
        public virtual void SetScale(Vector3 scale)
        {
            Block.SetScale(GetMesh(), scale);
        }
        public virtual void SetColor(Color color)
        {
            SetColor(this.GetComponent<Renderer>().material,color);
        }
        public virtual Block CreateNew()
        {
           return Instantiate(this);
        }
        public virtual void SetPosition(Vector3 position)
        {
            this.transform.position = position;
        }

        public Mesh GetMesh()
        {
            return GetComponent<MeshFilter>().mesh;
        }
        public static void SetColor(Material material, Color color)
        {
            material.color = color;
        }
        public static void SetScale(Mesh mesh, Vector3 scale,bool recalculateNormals=false)
        {
            Vector3[] baseVertices = mesh.vertices;

            var vertices = new Vector3[baseVertices.Length];

            for (var i = 0; i < vertices.Length; i++)
            {
                var vertex = baseVertices[i];
                vertex.Scale(scale);
              
                vertices[i] = vertex;
            }

            mesh.vertices = vertices;

            if (recalculateNormals)
                mesh.RecalculateNormals();
            mesh.RecalculateBounds();
        }
    }
}

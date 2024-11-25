using UnityEngine;

namespace DemoProject.Misc
{
    public class DrawMesh : MonoBehaviour
    {
        private void Awake()
        {
            var mesh = new Mesh();

            var vertices = new Vector3[4];
            var uv = new Vector2[4];
            var triangles = new int[6];

            vertices[0] = new Vector3(-1, 1);
            vertices[1] = new Vector3(-1, -1);
            vertices[2] = new Vector3(1, -1);
            vertices[3] = new Vector3(1, 1);

            uv[0] = Vector2.zero;
            uv[1] = Vector2.zero;
            uv[2] = Vector2.zero;
            uv[3] = Vector2.zero;

            triangles[0] = 0;
            triangles[1] = 3;
            triangles[2] = 1;

            triangles[3] = 1;
            triangles[4] = 3;
            triangles[5] = 2;

            mesh.vertices = vertices;
            mesh.uv = uv;
            mesh.triangles = triangles;
            
            mesh.MarkModified();

            GetComponent<MeshFilter>().mesh = mesh;
        }
    }
}
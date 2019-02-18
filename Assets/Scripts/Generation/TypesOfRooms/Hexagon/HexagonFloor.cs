using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class HexagonFloor : MonoBehaviour
{
    Vector3[] newVertices;
    Vector2[] newUV;
    int[] newTriangles;

    void Start()
    {
        Mesh mesh = new Mesh();

        newVertices = new Vector3[] {
            new Vector3(-22.5f,0,-10),
            new Vector3(0, 0,-25),
            new Vector3(22.5f, 0, -10),
            new Vector3(22.5f,0,10),
            new Vector3(0, 0,25),
            new Vector3(-22.5f, 0, 10),
            new Vector3(0, 0, 0)
        };
        newTriangles = new int[] { 6, 1, 0, 6, 2, 1, 6, 3, 2,
                                   6, 4, 3, 6, 5, 4, 6, 0, 5};

        mesh.vertices = newVertices;
        mesh.triangles = newTriangles;
        GetComponent<MeshFilter>().mesh = mesh;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class OctagonFloor : MonoBehaviour {

    Vector3[] newVertices;
    Vector2[] newUV;
    int[] newTriangles;

    void Start()
    {
        Mesh mesh = new Mesh();


        newVertices = new Vector3[] {
            new Vector3(0, 0, 0),
            new Vector3(-10, 0, -35),
            new Vector3(10, 0, -35),
            new Vector3(35, 0, -10),
            new Vector3(35, 0, 10),
            new Vector3(10, 0, 35),
            new Vector3(-10, 0, 35),
            new Vector3(-35, 0, 10),
            new Vector3(-35, 0, -10)
        };
        newTriangles = new int[] {2, 1, 0, 3, 2, 0, 4, 3, 0,
                                  5, 4, 0, 6, 5, 0, 7, 6, 0,
                                  8, 7, 0, 1, 8, 0
        };


        mesh.vertices = newVertices;
        mesh.triangles = newTriangles;
        mesh.RecalculateNormals();
        GetComponent<MeshFilter>().mesh = mesh;
    }
}

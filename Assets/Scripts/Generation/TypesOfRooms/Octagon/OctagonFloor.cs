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
        newTriangles = new int[] {8, 1, 0, 8, 2, 1, 8, 3, 2,
                                  8, 4, 3, 8, 5, 4, 8, 6, 5,
                                  8, 7, 6, 8, 0, 7
        };


        mesh.vertices = newVertices;
        mesh.triangles = newTriangles;
        GetComponent<MeshFilter>().mesh = mesh;
    }
}

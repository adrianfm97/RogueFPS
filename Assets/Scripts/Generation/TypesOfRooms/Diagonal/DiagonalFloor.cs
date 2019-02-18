using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DiagonalFloor : MonoBehaviour {

    Vector3[] newVertices;
    Vector2[] newUV;
    int[] newTriangles;

    void Start()
    {
        Mesh mesh = new Mesh();


        newVertices = new Vector3[] {
            new Vector3(-12.5f, 0, 12.5f),
            new Vector3(12.5f, 0, -12.5f),
            new Vector3(-22.5f, 0, 22.5f),
            new Vector3(2.5f, 0, 22.5f),
            new Vector3(2.5f, 0, 15),
            new Vector3(15, 0, 2.5f),
            new Vector3(22.5f, 0, 2.5f),
            new Vector3(22.5f, 0, -22.5f),
            new Vector3(-2.5f, 0, -22.5f),
            new Vector3(-2.5f, 0, -15),
            new Vector3(-15, 0, -2.5f),
            new Vector3(-22.5f, 0, -2.5f),
        };
        newTriangles = new int[] {0, 2, 3, 0, 3, 4, 0, 4, 10,
                                  0, 10, 11,
                                  0, 11, 2, 4, 5, 10, 10, 5, 9,
                                  1, 5, 6, 1, 6, 7, 1, 7, 8, 1, 8, 9,
                                  1, 9, 5
        };


        mesh.vertices = newVertices;
        mesh.triangles = newTriangles;
        GetComponent<MeshFilter>().mesh = mesh;
    }
}

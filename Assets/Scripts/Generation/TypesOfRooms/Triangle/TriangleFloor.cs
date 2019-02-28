using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TriangleFloor : MonoBehaviour {

    Vector3[] newVertices;
    Vector2[] newUV;
    int[] newTriangles;

    void Start()
    {
        Mesh mesh = new Mesh();

        newVertices = new Vector3[] {
            new Vector3(-12,0,-10),
            new Vector3(0, 0,12),
            new Vector3(12, 0, -10)
        };
        newTriangles = new int[] { 0, 1, 2 };
       
        mesh.vertices = newVertices;
        mesh.triangles = newTriangles;
        mesh.RecalculateNormals();
        GetComponent<MeshFilter>().mesh = mesh;
    }


}

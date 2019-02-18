using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Properties : MonoBehaviour
{
    //Scene GameObjects
    [SerializeField]
    private GameObject rooms, corridors, mapRooms, mapCorridors;

    [SerializeField]
    private int numRoom;
    [SerializeField]
    private float size = 25;
    //Probabilities
    [SerializeField]
    private int probCorrid, probRect, probSq2, probTri, probHexa;

    private void Awake()
    {
        if (probRect + probSq2 + probTri + probHexa > 100){
            Debug.Break();
            throw new System.Exception("Posibilities error in Properties");
        }
    }

    //Walls
    [SerializeField]
    private GameObject sWall, sWallC, s2Wall, s2WallCR, s2Wall2C, s2WallCL,
                      rShortWall, rWallC, rLargeWall, rWallCL, rWallCR, rWall2C, tWallC, tWallLat;
    //Floors
    [SerializeField]
    private GameObject square, square2, rectangleV, rectangleH, triangle, hexagonal, octagon, corridor;

    //Map
    [SerializeField]
    private GameObject mapSquare, mapSquare2, mapRectV, mapRectH,
                       mapTriangle, mapCorridor, mapPlayerPosition;

    //Getters && Setters
    public int NumRoom
    {
        get
        {
            return numRoom;
        }
        set
        {
            numRoom = value;
        }
    }
    public int ProbCorrid
    {
        get
        {
            return probCorrid;
        }

        set
        {
            probCorrid = value;
        }
    }
    public int ProbRect
    {
        get
        {
            return probRect;
        }

        set
        {
            probRect = value;
        }
    }
    public int ProbSq2
    {
        get
        {
            return probSq2;
        }

        set
        {
            probSq2 = value;
        }
    }
    public float Size
    {
        get
        {
            return size;
        }

        set
        {
            size = value;
        }
    }
    public GameObject Rooms
    {
        get
        {
            return rooms;
        }

        set
        {
            rooms = value;
        }
    }
    public GameObject Corridors
    {
        get
        {
            return corridors;
        }

        set
        {
            corridors = value;
        }
    }
    public GameObject MapCorridors
    {
        get
        {
            return mapCorridors;
        }

        set
        {
            mapCorridors = value;
        }
    }
    public GameObject MapSquare
    {
        get
        {
            return mapSquare;
        }

        set
        {
            mapSquare = value;
        }
    }
    public GameObject MapSquare2
    {
        get
        {
            return mapSquare2;
        }

        set
        {
            mapSquare2 = value;
        }
    }
    public GameObject MapRectV
    {
        get
        {
            return mapRectV;
        }

        set
        {
            mapRectV = value;
        }
    }
    public GameObject MapRectH
    {
        get
        {
            return mapRectH;
        }

        set
        {
            mapRectH = value;
        }
    }
    public GameObject MapCorridor
    {
        get
        {
            return mapCorridor;
        }

        set
        {
            mapCorridor = value;
        }
    }
    public GameObject Square
    {
        get
        {
            return square;
        }

        set
        {
            square = value;
        }
    }
    public GameObject Square2
    {
        get
        {
            return square2;
        }

        set
        {
            square2 = value;
        }
    }
    public GameObject RectangleV
    {
        get
        {
            return rectangleV;
        }

        set
        {
            rectangleV = value;
        }
    }
    public GameObject RectangleH
    {
        get
        {
            return rectangleH;
        }

        set
        {
            rectangleH = value;
        }
    }
    public GameObject Corridor
    {
        get
        {
            return corridor;
        }

        set
        {
            corridor = value;
        }
    }
    public GameObject SWall
    {
        get
        {
            return sWall;
        }

        set
        {
            sWall = value;
        }
    }
    public GameObject SWallC
    {
        get
        {
            return sWallC;
        }

        set
        {
            sWallC = value;
        }
    }
    public GameObject S2Wall
    {
        get
        {
            return s2Wall;
        }

        set
        {
            s2Wall = value;
        }
    }
    public GameObject S2WallCR
    {
        get
        {
            return s2WallCR;
        }

        set
        {
            s2WallCR = value;
        }
    }
    public GameObject S2Wall2C
    {
        get
        {
            return s2Wall2C;
        }

        set
        {
            s2Wall2C = value;
        }
    }
    public GameObject S2WallCL
    {
        get
        {
            return s2WallCL;
        }

        set
        {
            s2WallCL = value;
        }
    }
    public GameObject RShortWall
    {
        get
        {
            return rShortWall;
        }

        set
        {
            rShortWall = value;
        }
    }
    public GameObject RWallC
    {
        get
        {
            return rWallC;
        }

        set
        {
            rWallC = value;
        }
    }
    public GameObject RLargeWall
    {
        get
        {
            return rLargeWall;
        }

        set
        {
            rLargeWall = value;
        }
    }
    public GameObject RWallCL
    {
        get
        {
            return rWallCL;
        }

        set
        {
            rWallCL = value;
        }
    }
    public GameObject RWallCR
    {
        get
        {
            return rWallCR;
        }

        set
        {
            rWallCR = value;
        }
    }
    public GameObject RWall2C
    {
        get
        {
            return rWall2C;
        }

        set
        {
            rWall2C = value;
        }
    }
    public GameObject MapPlayerPosition
    {
        get
        {
            return mapPlayerPosition;
        }

        set
        {
            mapPlayerPosition = value;
        }
    }
    public GameObject MapRooms
    {
        get
        {
            return mapRooms;
        }

        set
        {
            mapRooms = value;
        }
    }
    public GameObject Triangle
    {
        get
        {
            return triangle;
        }

        set
        {
            triangle = value;
        }
    }
    public int ProbTri
    {
        get
        {
            return probTri;
        }

        set
        {
            probTri = value;
        }
    }
    public GameObject TWallC
    {
        get
        {
            return tWallC;
        }

        set
        {
            tWallC = value;
        }
    }
    public GameObject TWallLat
    {
        get
        {
            return tWallLat;
        }

        set
        {
            tWallLat = value;
        }
    }
    public GameObject MapTriangle
    {
        get
        {
            return mapTriangle;
        }

        set
        {
            mapTriangle = value;
        }
    }
    public GameObject Hexagonal
    {
        get
        {
            return hexagonal;
        }

        set
        {
            hexagonal = value;
        }
    }
    public int ProbHexa
    {
        get
        {
            return probHexa;
        }

        set
        {
            probHexa = value;
        }
    }
    public GameObject Octagon
    {
        get
        {
            return octagon;
        }

        set
        {
            octagon = value;
        }
    }
}

public struct VectArrayBoolInt
{
    public Vector3 vector3;
    public bool[] arrayBool;
    public int innt;
}
public struct VectQuater {
    public Vector3 vector3;
    public Quaternion quaternion;
}

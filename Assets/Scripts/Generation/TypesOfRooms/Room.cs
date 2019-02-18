using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Room
{
    protected Properties properties;
    protected Generation generation;

    protected GameObject gameObjectScene;
    protected GameObject gameObjectMap;

    protected int num;                     
    protected Vector3 posRoom;              

    private List<int> colindantRooms;
    private List<int> colindantCorridors;

    private int numColiRooms = 0;

    protected bool[] prevRoom, corridors, canCreateSq,
                     canCreateRect, canCreateSq2, canCreateHexa;

    protected int posibilitiesSq = 0, posibilitiesRect = 0, posibilitiesSq2 = 0, 
                  posibilitiesHexa = 0;

    protected float size, sBy1dot5, sBy2, sBy2Dot5, sDiv2;

    public Room(Vector3 pos, bool[] preR, int n)
    {
        properties = GameObject.FindObjectOfType<Properties>();
        generation = GameObject.FindObjectOfType<Generation>();


        prevRoom = preR; posRoom = pos; Num = n;

        colindantRooms = new List<int>();
        colindantCorridors = new List<int>();

        size = generation.size;        
        sBy1dot5 = size * 1.5f;
        sBy2 = size * 2;
        sBy2Dot5 = size * 2.5f;
        sDiv2 = size / 2f;
    }

    public void AddColindantRoom(int num)
    {
        bool add = true;
        foreach (int n in colindantRooms) { if (n == num) add = false; }
        if (add) colindantRooms.Add(num);
    }

    public abstract void ActualizeCanCreate(ref Vector3[] posRooms, int posRInit);
    public abstract void ActualizeCorridors(ref Vector3[] posCorridors, int posCInit);

    //Creation methods
    public abstract VectArrayBoolInt SqCreator(int cardinal);
    public abstract VectArrayBoolInt RectCreator(int cardinal);
    public abstract VectArrayBoolInt Sq2Creator(int cardinal);
    public abstract VectArrayBoolInt TriCreator(int cardinal);
    public abstract VectArrayBoolInt HexaCreator(int cardinal);
    public abstract VectQuater CorridorCreator(int cardinal);
    public abstract void WallsCreator();

    //Getter && Setters

    public Vector3 PosRoom
    {
        get { return posRoom; }
        set { posRoom = value; }
    }
    public int PosibilitiesSq
    {
        get
        {
            return posibilitiesSq;
        }

        set
        {
            posibilitiesSq = value;
        }
    }
    public int PosibilitiesRect
    {
        get
        {
            return posibilitiesRect;
        }

        set
        {
            posibilitiesRect = value;
        }
    }
    public int PosibilitiesSq2
    {
        get
        {
            return posibilitiesSq2;
        }

        set
        {
            posibilitiesSq2 = value;
        }
    }
    public bool[] Corridors
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
    public bool[] CanCreateSq
    {
        get
        {
            return canCreateSq;
        }

        set
        {
            canCreateSq = value;
        }
    }
    public bool[] CanCreateRect
    {
        get
        {
            return canCreateRect;
        }

        set
        {
            canCreateRect = value;
        }
    }
    public bool[] CanCreateSq2
    {
        get
        {
            return canCreateSq2;
        }

        set
        {
            canCreateSq2 = value;
        }
    }
    public bool[] PrevRoom
    {
        get
        {
            return prevRoom;
        }

        set
        {
            prevRoom = value;
        }
    }
    public List<int> ColindantRooms
    {
        get
        {
            return colindantRooms;
        }

        set
        {
            colindantRooms = value;
        }
    }
    public int NumColiRooms
    {
        get
        {
            return numColiRooms;
        }

        set
        {
            numColiRooms = value;
        }
    }
    public int Num
    {
        get
        {
            return num;
        }

        set
        {
            num = value;
        }
    }
    public GameObject GameObjectScene
    {
        get
        {
            return gameObjectScene;
        }

        set
        {
            gameObjectScene = value;
        }
    }
    public List<int> ColindantCorridors
    {
        get
        {
            return colindantCorridors;
        }

        set
        {
            colindantCorridors = value;
        }
    }
    public int PosibilitiesHexa
    {
        get
        {
            return posibilitiesHexa;
        }

        set
        {
            posibilitiesHexa = value;
        }
    }
    public bool[] CanCreateHexa
    {
        get
        {
            return canCreateHexa;
        }

        set
        {
            canCreateHexa = value;
        }
    }
}
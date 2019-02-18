using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Corridor {
    private Generation generation;
    private Properties properties;

    private GameObject gameObject;

    private int num;
    protected Vector3 pos;

    protected int[] numRooms;

    protected float size, sBy1dot5, sBy2, sBy2Dot5, sDiv2;


    public Corridor(Vector3 p, Quaternion q, int n)
    {
        properties = GameObject.FindObjectOfType<Properties>();
        generation = GameObject.FindObjectOfType<Generation>();

        Num = n; Pos = p;
        numRooms = new int[2];

        gameObject = GameObject.Instantiate(properties.Corridor, p, q,
                                            properties.Corridors.transform);
        gameObject.name = "Corridor" + n;

        size = generation.size;
    }    

    public abstract void ActualizeColindant(ref List<Room> rooms);

    public int[] NumRooms
    {
        get
        {
            return numRooms;
        }

        set
        {
            numRooms = value;
        }
    }
    public Vector3 Pos
    {
        get
        {
            return pos;
        }

        set
        {
            pos = value;
        }
    }
    public GameObject GameObject
    {
        get
        {
            return gameObject;
        }

        set
        {
            gameObject = value;
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
}

public class CorrV : Corridor{
    public CorrV(Vector3 p, Quaternion q, int n) : base(p, q, n) { }

    public override void ActualizeColindant(ref List<Room> rooms) {
        Vector3[] auxT = new Vector3[6],
                  auxB = new Vector3[6];

        auxT[0] = Pos + new Vector3(0, 0, size / 2f);
        auxT[1] = Pos + new Vector3(-size / 2f, 0, size);
        auxT[2] = Pos + new Vector3(size / 2f, 0, size);
        auxT[3] = Pos + new Vector3(-size / 2f, 0, size / 2f);
        auxT[4] = Pos + new Vector3(size / 2f, 0, size / 2f);
        auxT[5] = Pos + new Vector3(0, 0, size);

        auxB[0] = Pos + new Vector3(0, 0, -size / 2f);
        auxB[1] = Pos + new Vector3(-size / 2f, 0, -size);
        auxB[2] = Pos + new Vector3(size / 2f, 0, -size);
        auxB[3] = Pos + new Vector3(-size / 2f, 0, -size / 2f);
        auxB[4] = Pos + new Vector3(size / 2f, 0, -size / 2f);
        auxB[5] = Pos + new Vector3(0, 0, -size);

        bool foundT = false, foundB = false;
        int counter = 0;

        foreach (Room room in rooms) {            
            if (!foundT) {
                for (int i = 0; i < 6; i++) {
                    if (auxT[i] == room.PosRoom) {
                        numRooms[0] = counter;
                        foundT = true;
                    }
                }            
            }
            if (!foundB) {
                for (int i = 0; i < 6; i++) {
                    if (auxB[i] == room.PosRoom) {
                        numRooms[1] = counter;
                        foundB = true;
                    }
                }
            }
            else if (foundT) { break; }

            counter++;
        }
    }
}

public class CorrH : Corridor {
    public CorrH(Vector3 p, Quaternion q, int n) : base(p, q, n) { }

    public override void ActualizeColindant(ref List<Room> rooms) {
        Vector3[] auxR = new Vector3[6],
                  auxL = new Vector3[6];

        auxR[0] = Pos + new Vector3(size / 2f, 0, 0);
        auxR[1] = Pos + new Vector3(size, 0,  size / 2f);
        auxR[2] = Pos + new Vector3(size, 0, -size / 2f);
        auxR[3] = Pos + new Vector3(size / 2f, 0, size / 2f);
        auxR[4] = Pos + new Vector3(size / 2f, 0, -size / 2f);
        auxR[5] = Pos + new Vector3(size, 0, 0);

        auxL[0] = Pos + new Vector3(-size / 2f, 0, 0);
        auxL[1] = Pos + new Vector3(-size, 0, size / 2f);
        auxL[2] = Pos + new Vector3(-size, 0, -size / 2f);
        auxL[3] = Pos + new Vector3(-size / 2f, 0, size / 2f);
        auxL[4] = Pos + new Vector3(-size / 2f, 0, -size / 2f);
        auxL[5] = Pos + new Vector3(-size, 0, 0);

        bool foundR = false, foundL = false;
        int counter = 0;

        foreach (Room room in rooms)
        {
            if (!foundR)
            {
                for (int i = 0; i < 6; i++)
                {
                    if (auxR[i] == room.PosRoom)
                    {
                        NumRooms[0] = counter;
                        foundR = true;
                    }
                }
            }
            if (!foundL)
            {
                for (int i = 0; i < 6; i++)
                {
                    if (auxL[i] == room.PosRoom)
                    {
                        NumRooms[1] = counter;
                        foundL = true;
                    }
                }
            }
            else if (foundR) { break; }

            counter++;
        }
    }
}


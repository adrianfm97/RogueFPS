using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triangle : Room
{
    private Vector3[] posCorridors= new Vector3[4];
    private Quaternion rotation;
    public Triangle(Vector3 pos, bool[] preR, int n, Quaternion rot) : base(pos, preR, n)
    {
        rotation = rot;
        posCorridors[0] = posRoom + new Vector3(0, 0, sDiv2);
        posCorridors[1] = posRoom + new Vector3(sDiv2, 0, 0);
        posCorridors[2] = posRoom + new Vector3(0, 0, -sDiv2);
        posCorridors[3] = posRoom + new Vector3(-sDiv2, 0, 0);

        gameObjectScene = GameObject.Instantiate(properties.Triangle,
                                            pos, rot,
                                            properties.Rooms.transform);
        gameObjectScene.name = "Room" + n;
    }

    public override void ActualizeCanCreate(ref Vector3[] posRooms, int posRInit)
    {
        throw new System.NotImplementedException();
    }
    public override void ActualizeCorridors(ref Vector3[] posCorridors, int posCInit)
    {
        throw new System.NotImplementedException();
    }

    public override VectArrayBoolInt SqCreator(int cardinal)
    {
        Debug.Log("Can't create a Square Room from Room" + num);
        throw new System.NotImplementedException();
    }
    public override VectArrayBoolInt Sq2Creator(int cardinal)
    {
        Debug.Log("Can't create a Square2 Room from Room" + num);
        throw new System.NotImplementedException();
    }
    public override VectArrayBoolInt RectCreator(int cardinal)
    {
        Debug.Log("Can't create a Rectangular Room from Room" + num);
        throw new System.NotImplementedException();
    }
    public override VectArrayBoolInt TriCreator(int cardinal)
    {
        Debug.Log("Can't create a Triangular Room from Room" + num);
        throw new System.NotImplementedException();
    }
    public override VectArrayBoolInt HexaCreator(int cardinal)
    {
        Debug.Log("Can't create an Hexagonal Room from Room" + num);
        throw new System.NotImplementedException();
    }
    public override VectArrayBoolInt OctoCreator(int cardinal)
    {
        throw new System.NotImplementedException();
    }
    public override VectQuater CorridorCreator(int cardinal)
    {
        VectQuater aux = new VectQuater();

        switch (cardinal)
        {
            case 0:
                aux.vector3 = PosCorridors[0];
                aux.quaternion = new Quaternion(0, -180, 0, 0);
                break;
            case 1:
                aux.vector3 = posRoom + new Vector3(sDiv2, 0, 0);
                aux.quaternion = new Quaternion(0, 90, 0, 90);
                break;
            case 2:
                aux.vector3 = posRoom + new Vector3(0, 0, -sDiv2);
                aux.quaternion = Quaternion.identity;
                break;
            case 3:
                aux.vector3 = posRoom + new Vector3(-sDiv2, 0, 0);
                aux.quaternion = new Quaternion(0, 90, 0, 90);
                break;
        }
        return aux;
    }
    public override void WallsCreator(){
        GameObject.Instantiate(properties.TWallC, posRoom, rotation, gameObjectScene.transform);

        GameObject.Instantiate(properties.TWallLat, posRoom, rotation, gameObjectScene.transform);

    }

    public Vector3[] PosCorridors
    {
        get
        {
            return posCorridors;
        }

        set
        {
            posCorridors = value;
        }
    }
}

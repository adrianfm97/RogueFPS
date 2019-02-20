using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Octagon : Room
{
    private Vector3[] posSubRooms = new Vector3[9];
    private Vector3[] posCorridors = new Vector3[8];    

    public Octagon(Vector3 pos, bool[] preR, int n) : base(pos, preR, n) {

        corridors = new bool[4];

        PosSubRooms[0] = new Vector3(-size, 0, size) + pos;
        PosSubRooms[1] = new Vector3(0, 0, size) + pos;
        PosSubRooms[2] = new Vector3(size, 0, size) + pos;
        PosSubRooms[3] = new Vector3(-size, 0, 0) + pos;
        PosSubRooms[4] = pos;
        PosSubRooms[5] = new Vector3(size, 0, 0) + pos;
        PosSubRooms[6] = new Vector3(-size, 0, -size) + pos;
        PosSubRooms[7] = new Vector3(0, 0, -size) + pos;
        PosSubRooms[8] = new Vector3(size, 0, -size) + pos;

        PosCorridors[0] = posRoom + new Vector3(-size, 0, sBy1dot5);
        PosCorridors[1] = posRoom + new Vector3(size, 0, sBy1dot5);
        PosCorridors[2] = posRoom + new Vector3(sBy1dot5, 0, size);
        PosCorridors[3] = posRoom + new Vector3(sBy1dot5, 0, -size);
        PosCorridors[4] = posRoom + new Vector3(size, 0, -sBy1dot5);
        PosCorridors[5] = posRoom + new Vector3(-size, 0, -sBy1dot5);
        PosCorridors[6] = posRoom + new Vector3(-sBy1dot5, 0, -size);
        PosCorridors[7] = posRoom + new Vector3(-sBy1dot5, 0, size);

        gameObjectScene = GameObject.Instantiate(properties.Octagon, pos,
                                            Quaternion.identity,
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
        throw new System.NotImplementedException();
    }
    public override VectArrayBoolInt RectCreator(int cardinal)
    {
        throw new System.NotImplementedException();
    }
    public override VectArrayBoolInt Sq2Creator(int cardinal)
    {
        throw new System.NotImplementedException();
    }
    public override VectArrayBoolInt TriCreator(int cardinal)
    {
        throw new System.NotImplementedException();
    }
    public override VectArrayBoolInt HexaCreator(int cardinal)
    {
        throw new System.NotImplementedException();
    }
    public override VectArrayBoolInt OctoCreator(int cardinal)
    {
        throw new System.NotImplementedException();
    }
    public override VectQuater CorridorCreator(int cardinal)
    {
        throw new System.NotImplementedException();
    }

    public override void WallsCreator()
    {
        throw new System.NotImplementedException();
    }

    public Vector3[] PosCorridors { get => posCorridors; set => posCorridors = value; }
    public Vector3[] PosSubRooms { get => posSubRooms; set => posSubRooms = value; }
}

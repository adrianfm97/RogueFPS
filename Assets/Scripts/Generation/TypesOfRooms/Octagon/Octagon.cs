﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Octagon : Room
{
    private Vector3[] posSubRooms = new Vector3[9];
    private Vector3[] posCorridors = new Vector3[8];    

    public Octagon(Vector3 pos, bool[] preR, int n) : base(pos, preR, n) {

        corridors = new bool[4];

        for (int i = 0; i <= 3; i++) corridors[i] = false;

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
        for (int i = 0; i < posCorridors.Length; i++)
        {
            if (posRoom + new Vector3(0, 0, sBy1dot5) == posCorridors[i]) corridors[0] = true;
            if (posRoom + new Vector3(sBy1dot5, 0, 0) == posCorridors[i]) corridors[1] = true;
            if (posRoom + new Vector3(0, 0, -sBy1dot5) == posCorridors[i]) corridors[2] = true;
            if (posRoom + new Vector3(-sBy1dot5, 0, 0) == posCorridors[i]) corridors[3] = true;
        }
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
        VectQuater aux = new VectQuater();

        switch (cardinal)
        {
            case 0:
                aux.vector3 = posRoom + new Vector3(0, 0, sBy1dot5);
                aux.quaternion = Quaternion.identity;
                break;
            case 1:
                aux.vector3 = posRoom + new Vector3(sBy1dot5, 0, 0);
                aux.quaternion = new Quaternion(0, 270, 0, 270);
                break;
            case 2:
                aux.vector3 = posRoom + new Vector3(0, 0, -sBy1dot5);
                aux.quaternion = Quaternion.identity;
                break;
            case 3:
                aux.vector3 = posRoom + new Vector3(-sBy1dot5, 0, 0);
                aux.quaternion = new Quaternion(0, 270, 0, 270);
                break;           
        }
        return aux;
    }

    public override void WallsCreator()
    {
        GameObject aux;
        if (corridors[0]) aux = GameObject.Instantiate(properties.OctaWallC,
                                posRoom, Quaternion.Euler(new Vector3(0, 180, 0)));
        else aux = GameObject.Instantiate(properties.OctaWallNC,
                   posRoom, Quaternion.Euler(new Vector3(0, 180, 0)));
        aux.transform.parent = gameObjectScene.transform;

        if (corridors[1]) aux = GameObject.Instantiate(properties.OctaWallC,
                                posRoom, Quaternion.Euler(new Vector3(0, -90, 0)));
        else aux = GameObject.Instantiate(properties.OctaWallNC,
                   posRoom, Quaternion.Euler(new Vector3(0, -90, 0)));
        aux.transform.parent = gameObjectScene.transform;

        if (corridors[2]) aux = GameObject.Instantiate(properties.OctaWallC,
                                posRoom, Quaternion.identity);
        else aux = GameObject.Instantiate(properties.OctaWallNC,
                   posRoom, Quaternion.identity);
        aux.transform.parent = gameObjectScene.transform;

        if (corridors[3]) aux = GameObject.Instantiate(properties.OctaWallC,
                                posRoom, Quaternion.Euler(new Vector3(0, 90, 0)));
        else aux = GameObject.Instantiate(properties.OctaWallNC,
                   posRoom, Quaternion.Euler(new Vector3(0, 90, 0)));
        aux.transform.parent = gameObjectScene.transform;

        aux = GameObject.Instantiate(properties.OctaWalls, posRoom, Quaternion.identity);
        aux.transform.parent = gameObjectScene.transform;
    }

    public Vector3[] PosCorridors { get => posCorridors; set => posCorridors = value; }
    public Vector3[] PosSubRooms { get => posSubRooms; set => posSubRooms = value; }
}

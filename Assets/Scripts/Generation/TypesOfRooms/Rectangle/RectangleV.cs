﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectangleV : Room
{
    //posSubRooms[0]=Arriba ... [1]=Abajo
    Vector3[] _posSubRooms = new Vector3[2];

    public RectangleV(Vector3 pos, bool[] preR, int n) : base(pos, preR, n)
    {
        canCreateSq = new bool[6];
        canCreateRect = new bool[16];
        canCreateSq2 = new bool[10];
        CanCreateHexa = new bool[6];

        corridors = new bool[6];

        for (int i = 0; i <= 5; i++)
        {
            canCreateSq[i] = true;
            corridors[i] = false;
            CanCreateHexa[i] = false;
        }
        for (int i = 0; i <= 15; i++) { canCreateRect[i] = true; }
        for (int i = 0; i <= 9; i++) { canCreateSq2[i] = true; }

        posSubRooms[0] = new Vector3(0, 0, sDiv2) + pos;
        posSubRooms[1] = new Vector3(0, 0, -size / 2) + pos;

        gameObjectScene = GameObject.Instantiate(properties.RectangleV, pos,
                                            Quaternion.identity, properties.Rooms.transform);
        gameObjectScene.name = "Room" + n;
    }

    public override void ActualizeCanCreate(ref Vector3[] posRooms, int posInit)
    {
        for (int i = 0; i < posInit; i++)
        {
            //
            if (posRoom + new Vector3(0, 0, sBy1dot5) == posRooms[i]) canCreateSq[0] = false;
            if (posRoom + new Vector3(size, 0, sDiv2) == posRooms[i]) canCreateSq[1] = false;
            if (posRoom + new Vector3(size, 0, -sDiv2) == posRooms[i]) canCreateSq[2] = false;
            if (posRoom + new Vector3(0, 0, -sBy1dot5) == posRooms[i]) canCreateSq[3] = false;
            if (posRoom + new Vector3(-size, 0, -sDiv2) == posRooms[i]) canCreateSq[4] = false;
            if (posRoom + new Vector3(-size, 0, sDiv2) == posRooms[i]) canCreateSq[5] = false;
            //
            if (canCreateSq[0])
            {
                if (posRoom + new Vector3(-size, 0, sBy1dot5) == posRooms[i]) canCreateRect[0] = false;
                if (posRoom + new Vector3(0, 0, sBy2Dot5) == posRooms[i]) canCreateRect[1] = false;
                if (posRoom + new Vector3(size, 0, sBy1dot5) == posRooms[i]) canCreateRect[2] = false;
            }
            else { canCreateRect[0] = false; canCreateRect[1] = false; canCreateRect[2] = false; }

            if (canCreateSq[1])
            {
                if (posRoom + new Vector3(size, 0, sBy1dot5) == posRooms[i]) canCreateRect[3] = false;
                if (posRoom + new Vector3(sBy2, 0, sDiv2) == posRooms[i]) canCreateRect[4] = false;
                if (!canCreateSq[2]) canCreateRect[5] = false;
            }
            else { canCreateRect[3] = false; canCreateRect[4] = false; canCreateRect[5] = false; }
            if (canCreateSq[2])
            {
                if (posRoom + new Vector3(sBy2, 0, -sDiv2) == posRooms[i]) canCreateRect[6] = false;
                if (posRoom + new Vector3(size, 0, -sBy1dot5) == posRooms[i]) canCreateRect[7] = false;
            }
            else { canCreateRect[6] = false; canCreateRect[7] = false; }

            if (canCreateSq[3])
            {
                if (posRoom + new Vector3(size, 0, -sBy1dot5) == posRooms[i]) canCreateRect[8] = false;
                if (posRoom + new Vector3(0, 0, -sBy2Dot5) == posRooms[i]) canCreateRect[9] = false;
                if (posRoom + new Vector3(-size, 0, -sBy1dot5) == posRooms[i]) canCreateRect[10] = false;
            }
            else { canCreateRect[8] = false; canCreateRect[9] = false; canCreateRect[10] = false; }

            if (canCreateSq[4])
            {
                if (posRoom + new Vector3(-size, 0, -sBy1dot5) == posRooms[i]) canCreateRect[11] = false;
                if (posRoom + new Vector3(-sBy2, 0, -sDiv2) == posRooms[i]) canCreateRect[12] = false;
                if (!canCreateSq[5]) canCreateRect[13] = false;
            }
            else { canCreateRect[11] = false; canCreateRect[12] = false; canCreateRect[13] = false; }
            if (canCreateSq[5])
            {
                if (posRoom + new Vector3(-sBy2, 0, sDiv2) == posRooms[i]) canCreateRect[14] = false;
                if (posRoom + new Vector3(-size, 0, sBy1dot5) == posRooms[i]) canCreateRect[15] = false;
            }
            else { canCreateRect[14] = false; canCreateRect[15] = false; }
            //
            if (canCreateRect[1])
            {
                if (!canCreateRect[0] ||
                    posRoom + new Vector3(-size, 0, sBy2Dot5) == posRooms[i]) canCreateSq2[0] = false;
                if (!canCreateRect[2] ||
                    posRoom + new Vector3(size, 0, sBy2Dot5) == posRooms[i]) canCreateSq2[1] = false;
            }
            else { canCreateSq2[0] = false; canCreateSq2[1] = false; }

            if (canCreateRect[3] && canCreateRect[4])
            {
                if (posRoom + new Vector3(sBy2, 0, sBy1dot5) == posRooms[i]) canCreateSq2[2] = false;
            }
            else { canCreateSq2[2] = false; }
            if (!canCreateRect[4] || !canCreateRect[6]) canCreateSq2[3] = false;
            if (canCreateRect[6] && canCreateRect[7])
            {
                if (posRoom + new Vector3(sBy2, 0, -sBy1dot5) == posRooms[i]) canCreateSq2[4] = false;
            }
            else { canCreateSq2[4] = false; }

            if (canCreateRect[9])
            {
                if (!canCreateRect[8] ||
                    posRoom + new Vector3(size, 0, -sBy2Dot5) == posRooms[i]) canCreateSq2[5] = false;
                if (!canCreateRect[10] ||
                    posRoom + new Vector3(-size, 0, -sBy2Dot5) == posRooms[i]) canCreateSq2[6] = false;
            }
            else { canCreateSq2[5] = false; canCreateSq2[6] = false; }

            if (canCreateRect[11] && canCreateRect[12])
            {
                if (posRoom + new Vector3(-sBy2, 0, -sBy1dot5) == posRooms[i]) canCreateSq2[7] = false;
            }
            else { canCreateSq2[7] = false; }
            if (!canCreateRect[12] || !canCreateRect[14]) canCreateSq2[8] = false;
            if (canCreateRect[14] && canCreateRect[15])
            {
                if (posRoom + new Vector3(-sBy2, 0, sBy1dot5) == posRooms[i]) canCreateSq2[9] = false;
            }
            else { canCreateSq2[9] = false; }
        }

        if (canCreateSq2[0] && canCreateSq2[1]) CanCreateHexa[0] = true;
        if (canCreateSq2[2] && canCreateSq2[3]) CanCreateHexa[1] = true;
        if (canCreateSq2[3] && canCreateSq2[4]) CanCreateHexa[2] = true;
        if (canCreateSq2[5] && canCreateSq2[6]) CanCreateHexa[3] = true;
        if (canCreateSq2[7] && canCreateSq2[8]) CanCreateHexa[4] = true;
        if (canCreateSq2[8] && canCreateSq2[9]) CanCreateHexa[5] = true;

        posibilitiesSq = 0;
        posibilitiesRect = 0;
        posibilitiesSq2 = 0;
        PosibilitiesHexa = 0;

        for (int i = 0; i <= 5; i++)
        {
            if (canCreateSq[i]) posibilitiesSq++;
            if (CanCreateHexa[i]) PosibilitiesHexa++;
        }
        for (int i = 0; i <= 15; i++) if (canCreateRect[i]) posibilitiesRect++;
        for (int i = 0; i <= 9; i++) if (canCreateSq2[i]) posibilitiesSq2++;
    }
    public override void ActualizeCorridors(ref Vector3[] posCorridors, int posCInit)
    {
        for (int i = 0; i < posCorridors.Length; i++)
        {
            if (posRoom + new Vector3(0, 0, size) == posCorridors[i]) corridors[0] = true;
            if (posRoom + new Vector3(sDiv2, 0, sDiv2) == posCorridors[i]) corridors[1] = true;
            if (posRoom + new Vector3(sDiv2, 0, -sDiv2) == posCorridors[i]) corridors[2] = true;
            if (posRoom + new Vector3(0, 0, -size) == posCorridors[i]) corridors[3] = true;
            if (posRoom + new Vector3(-sDiv2, 0, -sDiv2) == posCorridors[i]) corridors[4] = true;
            if (posRoom + new Vector3(-sDiv2, 0, sDiv2) == posCorridors[i]) corridors[5] = true;
        }
    }

    public override VectArrayBoolInt SqCreator(int cardinal)
    {
        VectArrayBoolInt auxStruct;
        auxStruct.arrayBool = new bool[8];
        auxStruct.vector3 = new Vector3(0, 0, 0);
        auxStruct.innt = 0;

        switch (cardinal)
        {
            case 0:
                auxStruct.vector3 = (new Vector3(posRoom.x, posRoom.y,
                                                 posRoom.z + sBy1dot5));
                auxStruct.arrayBool = new bool[] { false, false, true, false };
                break;
            case 1:
                auxStruct.vector3 = (new Vector3(posRoom.x + size,
                                                 posRoom.y, posRoom.z + sDiv2));
                auxStruct.arrayBool = new bool[] { false, false, false, true };
                break;
            case 2:
                auxStruct.vector3 = (new Vector3(posRoom.x + size,
                                                 posRoom.y, posRoom.z - sDiv2));
                auxStruct.arrayBool = new bool[] { false, false, false, true };
                break;
            case 3:
                auxStruct.vector3 = (new Vector3(posRoom.x, posRoom.y,
                                                 posRoom.z - sBy1dot5));
                auxStruct.arrayBool = new bool[] { true, false, false, false };
                break;
            case 4:
                auxStruct.vector3 = (new Vector3(posRoom.x - size,
                                                 posRoom.y, posRoom.z - sDiv2));
                auxStruct.arrayBool = new bool[] { false, true, false, false };
                break;
            case 5:
                auxStruct.vector3 = (new Vector3(posRoom.x - size,
                                                 posRoom.y, posRoom.z + sDiv2));
                auxStruct.arrayBool = new bool[] { false, true, false, false };
                break;
        }
        return auxStruct;
    }
    public override VectArrayBoolInt RectCreator(int cardinal)
    {
        VectArrayBoolInt auxStruct;
        auxStruct.arrayBool = new bool[6];
        auxStruct.vector3 = new Vector3(0, 0, 0);
        auxStruct.innt = 1;

        switch (cardinal)
        {
            case 0:
                auxStruct.vector3 = (new Vector3(posRoom.x - sDiv2, posRoom.y,
                                                 posRoom.z + sBy1dot5));
                auxStruct.arrayBool = new bool[] { false, false, false, true, false, false };
                auxStruct.innt = 2;
                break;
            case 1:
                auxStruct.vector3 = (new Vector3(posRoom.x, posRoom.y,
                                                 posRoom.z + sBy2));
                auxStruct.arrayBool = new bool[] { false, false, false, true, false, false };
                break;
            case 2:
                auxStruct.vector3 = (new Vector3(posRoom.x + sDiv2, posRoom.y,
                                                 posRoom.z + sBy1dot5));
                auxStruct.arrayBool = new bool[] { false, false, false, false, true, false };
                auxStruct.innt = 2;
                break;
            case 3:
                auxStruct.vector3 = (new Vector3(posRoom.x + size, posRoom.y,
                                                 posRoom.z + size));
                auxStruct.arrayBool = new bool[] { false, false, false, false, true, false };
                break;
            case 4:
                auxStruct.vector3 = (new Vector3(posRoom.x + sBy1dot5, posRoom.y,
                                                 posRoom.z + sDiv2));
                auxStruct.arrayBool = new bool[] { false, false, false, false, false, true };
                auxStruct.innt = 2;
                break;
            case 5:
                auxStruct.vector3 = (new Vector3(posRoom.x + size, posRoom.y,
                                                 posRoom.z));
                auxStruct.arrayBool = new bool[] { false, false, false, false, true, true };
                break;
            case 6:
                auxStruct.vector3 = (new Vector3(posRoom.x + sBy1dot5, posRoom.y,
                                                 posRoom.z - sDiv2));
                auxStruct.arrayBool = new bool[] { false, false, false, false, false, true };
                auxStruct.innt = 2;
                break;
            case 7:
                auxStruct.vector3 = (new Vector3(posRoom.x + size, posRoom.y,
                                                 posRoom.z - size));
                auxStruct.arrayBool = new bool[] { false, false, false, false, false, true };
                break;
            case 8:
                auxStruct.vector3 = (new Vector3(posRoom.x + sDiv2, posRoom.y,
                                                 posRoom.z - sBy1dot5));
                auxStruct.arrayBool = new bool[] { true, false, false, false, false, false };
                auxStruct.innt = 2;
                break;
            case 9:
                auxStruct.vector3 = (new Vector3(posRoom.x, posRoom.y,
                                                 posRoom.z - sBy2));
                auxStruct.arrayBool = new bool[] { true, false, false, false, false, false };
                break;
            case 10:
                auxStruct.vector3 = (new Vector3(posRoom.x - sDiv2, posRoom.y,
                                                 posRoom.z - sBy1dot5));
                auxStruct.arrayBool = new bool[] { false, true, false, false, false, false };
                auxStruct.innt = 2;
                break;
            case 11:
                auxStruct.vector3 = (new Vector3(posRoom.x - size, posRoom.y,
                                                 posRoom.z - size));
                auxStruct.arrayBool = new bool[] { false, true, false, false, false, false };
                break;
            case 12:
                auxStruct.vector3 = (new Vector3(posRoom.x - sBy1dot5, posRoom.y,
                                                 posRoom.z - sDiv2));
                auxStruct.arrayBool = new bool[] { false, false, true, false, false, false };
                auxStruct.innt = 2;
                break;
            case 13:
                auxStruct.vector3 = (new Vector3(posRoom.x - size, posRoom.y,
                                                 posRoom.z));
                auxStruct.arrayBool = new bool[] { false, true, true, false, false, false };
                break;
            case 14:
                auxStruct.vector3 = (new Vector3(posRoom.x - sBy1dot5, posRoom.y,
                                                 posRoom.z + sDiv2));
                auxStruct.arrayBool = new bool[] { false, false, true, false, false, false };
                auxStruct.innt = 2;
                break;
            case 15:
                auxStruct.vector3 = (new Vector3(posRoom.x - size, posRoom.y,
                                                 posRoom.z + size));
                auxStruct.arrayBool = new bool[] { false, false, true, false, false, false };
                break;
        }
        return auxStruct;
    }
    public override VectArrayBoolInt Sq2Creator(int cardinal)
    {
        VectArrayBoolInt auxStruct;
        auxStruct.arrayBool = new bool[8];
        auxStruct.vector3 = new Vector3(0, 0, 0);
        auxStruct.innt = 3;

        switch (cardinal)
        {
            case 0:
                auxStruct.vector3 = (new Vector3(posRoom.x - sDiv2, posRoom.y,
                                                 posRoom.z + sBy2));
                auxStruct.arrayBool = new bool[] { false, false, false, false, true, false, false, false };
                break;
            case 1:
                auxStruct.vector3 = (new Vector3(posRoom.x + sDiv2, posRoom.y,
                                                 posRoom.z + sBy2));
                auxStruct.arrayBool = new bool[] { false, false, false, false, false, true, false, false };
                break;
            case 2:
                auxStruct.vector3 = (new Vector3(posRoom.x + sBy1dot5, posRoom.y,
                                                 posRoom.z + size));
                auxStruct.arrayBool = new bool[] { false, false, false, false, false, false, true, false };
                break;
            case 3:
                auxStruct.vector3 = (new Vector3(posRoom.x + sBy1dot5, posRoom.y,
                                                 posRoom.z));
                auxStruct.arrayBool = new bool[] { false, false, false, false, false, false, true, true };
                break;
            case 4:
                auxStruct.vector3 = (new Vector3(posRoom.x + sBy1dot5, posRoom.y,
                                                 posRoom.z - size));
                auxStruct.arrayBool = new bool[] { false, false, false, false, false, false, false, true };
                break;
            case 5:
                auxStruct.vector3 = (new Vector3(posRoom.x + sDiv2, posRoom.y,
                                                 posRoom.z - sBy2));
                auxStruct.arrayBool = new bool[] { true, false, false, false, false, false, false, false };
                break;
            case 6:
                auxStruct.vector3 = (new Vector3(posRoom.x - sDiv2, posRoom.y,
                                                 posRoom.z - sBy2));
                auxStruct.arrayBool = new bool[] { false, true, false, false, false, false, false, false };
                break;
            case 7:
                auxStruct.vector3 = (new Vector3(posRoom.x - sBy1dot5, posRoom.y,
                                                 posRoom.z - size));
                auxStruct.arrayBool = new bool[] { false, false, true, false, false, false, false, false };
                break;
            case 8:
                auxStruct.vector3 = (new Vector3(posRoom.x - sBy1dot5, posRoom.y,
                                                 posRoom.z));
                auxStruct.arrayBool = new bool[] { false, false, true, true, false, false, false, false };
                break;
            case 9:
                auxStruct.vector3 = (new Vector3(posRoom.x - sBy1dot5, posRoom.y,
                                                 posRoom.z + size));
                auxStruct.arrayBool = new bool[] { false, false, false, true, false, false, false, false };
                break;
        }
        return auxStruct;
    }
    public override VectArrayBoolInt TriCreator(int cardinal)
    {
        VectArrayBoolInt auxStruct;
        auxStruct.arrayBool = new bool[8];
        auxStruct.vector3 = new Vector3(0, 0, 0);
        auxStruct.innt = 0;

        switch (cardinal)
        {
            case 0:
                auxStruct.vector3 = (new Vector3(posRoom.x, posRoom.y,
                                                 posRoom.z + sBy1dot5));
                auxStruct.arrayBool = new bool[] { false, false, true, false };
                auxStruct.innt = 10;
                break;
            case 1:
                auxStruct.vector3 = (new Vector3(posRoom.x + size,
                                                 posRoom.y, posRoom.z + sDiv2));
                auxStruct.arrayBool = new bool[] { false, false, false, true };
                auxStruct.innt = 11;
                break;
            case 2:
                auxStruct.vector3 = (new Vector3(posRoom.x + size,
                                                 posRoom.y, posRoom.z - sDiv2));
                auxStruct.arrayBool = new bool[] { false, false, false, true };
                auxStruct.innt = 11;
                break;
            case 3:
                auxStruct.vector3 = (new Vector3(posRoom.x, posRoom.y,
                                                 posRoom.z - sBy1dot5));
                auxStruct.arrayBool = new bool[] { true, false, false, false };
                auxStruct.innt = 12;
                break;
            case 4:
                auxStruct.vector3 = (new Vector3(posRoom.x - size,
                                                 posRoom.y, posRoom.z - sDiv2));
                auxStruct.arrayBool = new bool[] { false, true, false, false };
                auxStruct.innt = 13;
                break;
            case 5:
                auxStruct.vector3 = (new Vector3(posRoom.x - size,
                                                 posRoom.y, posRoom.z + sDiv2));
                auxStruct.arrayBool = new bool[] { false, true, false, false };
                auxStruct.innt = 13;
                break;
        }
        return auxStruct;
    }
    public override VectArrayBoolInt HexaCreator(int cardinal)
    {
        VectArrayBoolInt auxStruct;
        auxStruct.arrayBool = new bool[2];
        auxStruct.vector3 = new Vector3(0, 0, 0);
        auxStruct.innt = 5;

        switch (cardinal)
        {
            case 0:
                auxStruct.vector3 = (new Vector3(posRoom.x, posRoom.y,
                                                 posRoom.z + sBy2));
                auxStruct.arrayBool = new bool[] { false, true };
                auxStruct.innt = 4;
                break;
            case 1:
                auxStruct.vector3 = (new Vector3(posRoom.x + sBy1dot5,
                                                 posRoom.y, posRoom.z + sDiv2));
                auxStruct.arrayBool = new bool[] { false, true };
                break;
            case 2:
                auxStruct.vector3 = (new Vector3(posRoom.x + sBy1dot5,
                                                 posRoom.y, posRoom.z - sDiv2));
                auxStruct.arrayBool = new bool[] { false, true };
                break;
            case 3:
                auxStruct.vector3 = (new Vector3(posRoom.x, posRoom.y,
                                                 posRoom.z - sBy2));
                auxStruct.arrayBool = new bool[] { true, false };
                auxStruct.innt = 4;
                break;
            case 4:
                auxStruct.vector3 = (new Vector3(posRoom.x - sBy1dot5,
                                                 posRoom.y, posRoom.z - sDiv2));
                auxStruct.arrayBool = new bool[] { true, false };
                break;
            case 5:
                auxStruct.vector3 = (new Vector3(posRoom.x - sBy1dot5,
                                                 posRoom.y, posRoom.z + sDiv2));
                auxStruct.arrayBool = new bool[] { true, false };
                break;
        }
        return auxStruct;
    }
    public override VectQuater CorridorCreator(int cardinal)
    {
        VectQuater aux = new VectQuater();

        switch (cardinal)
        {
            case 0:
                aux.vector3 = posRoom + new Vector3(0, 0, size);
                aux.quaternion = new Quaternion(0, 180, 0, 0);
                break;
            case 1:
                aux.vector3 = posRoom + new Vector3(sDiv2, 0, sDiv2);
                aux.quaternion = new Quaternion(0, 270, 0, 270);
                break;
            case 2:
                aux.vector3 = posRoom + new Vector3(sDiv2, 0, -sDiv2);
                aux.quaternion = new Quaternion(0, 270, 0, 270);
                break;
            case 3:
                aux.vector3 = posRoom + new Vector3(0, 0, -size);
                aux.quaternion = Quaternion.identity;
                break;
            case 4:
                aux.vector3 = posRoom + new Vector3(-sDiv2, 0, -sDiv2);
                aux.quaternion = new Quaternion(0, 90, 0, 90);
                break;
            case 5:
                aux.vector3 = posRoom + new Vector3(-sDiv2, 0, sDiv2);
                aux.quaternion = new Quaternion(0, 90, 0, 90);
                break;
        }
        return aux;
    }
    public override void WallsCreator()
    {
        GameObject aux;
        if (corridors[0]) aux = GameObject.Instantiate(properties.RWallC, posRoom, Quaternion.Euler(new Vector3(0, 270, 0)));
        else aux = GameObject.Instantiate(properties.RShortWall, posRoom, Quaternion.Euler(new Vector3(0, 270, 0)));
        aux.transform.parent = gameObjectScene.transform;

        if (corridors[1])
        {
            if (corridors[2]) aux = GameObject.Instantiate(properties.RWall2C, posRoom, Quaternion.Euler(new Vector3(0, 90, 0)));
            else aux = GameObject.Instantiate(properties.RWallCL, posRoom, Quaternion.Euler(new Vector3(0, 90, 0)));
        }
        else if (corridors[2]) aux = GameObject.Instantiate(properties.RWallCR, posRoom, Quaternion.Euler(new Vector3(0, 90, 0)));
        else aux = GameObject.Instantiate(properties.RLargeWall, posRoom, Quaternion.Euler(new Vector3(0, 90, 0)));
        aux.transform.parent = gameObjectScene.transform;

        if (corridors[3]) aux = GameObject.Instantiate(properties.RWallC, posRoom, Quaternion.Euler(new Vector3(0, 90, 0)));
        else aux = GameObject.Instantiate(properties.RShortWall, posRoom, Quaternion.Euler(new Vector3(0, 90, 0)));
        aux.transform.parent = gameObjectScene.transform;

        if (corridors[4])
        {
            if (corridors[5]) aux = GameObject.Instantiate(properties.RWall2C, posRoom, Quaternion.Euler(new Vector3(0, 270, 0)));
            else aux = GameObject.Instantiate(properties.RWallCL, posRoom, Quaternion.Euler(new Vector3(0, 270, 0)));
        }
        else if (corridors[5]) aux = GameObject.Instantiate(properties.RWallCR, posRoom, Quaternion.Euler(new Vector3(0, 270, 0)));
        else aux = GameObject.Instantiate(properties.RLargeWall, posRoom, Quaternion.Euler(new Vector3(0, 270, 0)));
        aux.transform.parent = gameObjectScene.transform;
    }

    public Vector3[] posSubRooms
    {
        get { return _posSubRooms; }
        set { _posSubRooms = value; }
    }
}
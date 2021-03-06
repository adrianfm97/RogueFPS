﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectangleH : Room
{
    //posSubRooms[0]=Derecha ...[1]=Izquierda
    Vector3[] _posSubRooms = new Vector3[2];

    public RectangleH(Vector3 pos, bool[] preR, int n) : base(pos, preR, n)
    {
        canCreateSq = new bool[6];
        canCreateRect = new bool[16];
        canCreateSq2 = new bool[10];
        canCreateHexa = new bool[6];
        canCreateOcto = new bool[6];

        corridors = new bool[6];

        for (int i = 0; i <= 5; i++)
        {
            canCreateSq[i] = true;
            corridors[i] = false;
            canCreateHexa[i] = false;
            canCreateOcto[i] = true;
        }
        for (int i = 0; i <= 15; i++) { canCreateRect[i] = true; }
        for (int i = 0; i <= 9; i++) { canCreateSq2[i] = true; }

        _posSubRooms[0] = new Vector3(sDiv2, 0, 0) + pos;
        _posSubRooms[1] = new Vector3(-sDiv2, 0, 0) + pos;

        gameObjectScene = GameObject.Instantiate(properties.RectangleH, pos,
                                            Quaternion.identity, properties.Rooms.transform);
        gameObjectScene.name = "Room" + n;
    }

    public override void ActualizeCanCreate(ref Vector3[] posRooms, int posInit)
    {
        for (int i = 0; i < posInit; i++)
        {
            //
            if (posRoom + new Vector3(-sDiv2, 0, size) == posRooms[i]) canCreateSq[0] = false;
            if (posRoom + new Vector3(sDiv2, 0, size) == posRooms[i]) canCreateSq[1] = false;
            if (posRoom + new Vector3(sBy1dot5, 0, 0) == posRooms[i]) canCreateSq[2] = false;
            if (posRoom + new Vector3(sDiv2, 0, -size) == posRooms[i]) canCreateSq[3] = false;
            if (posRoom + new Vector3(-sDiv2, 0, -size) == posRooms[i]) canCreateSq[4] = false;
            if (posRoom + new Vector3(-sBy1dot5, 0, 0) == posRooms[i]) canCreateSq[5] = false;
            //
            if (canCreateSq[0])
            {
                if (posRoom + new Vector3(-sBy1dot5, 0, size) == posRooms[i]) canCreateRect[0] = false;
                if (posRoom + new Vector3(-sDiv2, 0, sBy2) == posRooms[i]) canCreateRect[1] = false;
                if (!canCreateSq[1]) canCreateRect[2] = false;
            }
            else { canCreateRect[0] = false; canCreateRect[1] = false; canCreateRect[2] = false; }
            if (canCreateSq[1])
            {
                if (posRoom + new Vector3(sDiv2, 0, sBy2) == posRooms[i]) canCreateRect[3] = false;
                if (posRoom + new Vector3(sBy1dot5, 0, size) == posRooms[i]) canCreateRect[4] = false;
            }
            else { canCreateRect[3] = false; canCreateRect[4] = false; }

            if (canCreateSq[2])
            {
                if (posRoom + new Vector3(sBy1dot5, 0, size) == posRooms[i]) canCreateRect[5] = false;
                if (posRoom + new Vector3(sBy2Dot5, 0, 0) == posRooms[i]) canCreateRect[6] = false;
                if (posRoom + new Vector3(sBy1dot5, 0, -size) == posRooms[i]) canCreateRect[7] = false;
            }
            else { canCreateRect[5] = false; canCreateRect[6] = false; canCreateRect[7] = false; }

            if (canCreateSq[3])
            {
                if (posRoom + new Vector3(sBy1dot5, 0, -size) == posRooms[i]) canCreateRect[8] = false;
                if (posRoom + new Vector3(sDiv2, 0, -sBy2) == posRooms[i]) canCreateRect[9] = false;
                if (!canCreateSq[4]) canCreateRect[10] = false;
            }
            else { canCreateRect[8] = false; canCreateRect[9] = false; canCreateRect[10] = false; }
            if (canCreateSq[4])
            {
                if (posRoom + new Vector3(-sDiv2, 0, -sBy2) == posRooms[i]) canCreateRect[11] = false;
                if (posRoom + new Vector3(-sBy1dot5, 0, -size) == posRooms[i]) canCreateRect[12] = false;
            }
            else { canCreateRect[11] = false; canCreateRect[12] = false; }

            if (canCreateSq[5])
            {
                if (posRoom + new Vector3(-sBy1dot5, 0, -size) == posRooms[i]) canCreateRect[13] = false;
                if (posRoom + new Vector3(-sBy2Dot5, 0, 0) == posRooms[i]) canCreateRect[14] = false;
                if (posRoom + new Vector3(-sBy1dot5, 0, size) == posRooms[i]) canCreateRect[15] = false;
            }
            else { canCreateRect[13] = false; canCreateRect[14] = false; canCreateRect[15] = false; }
            //
            if (canCreateRect[0] && canCreateRect[1])
            {
                if (posRoom + new Vector3(-sBy1dot5, 0, sBy2) == posRooms[i]) canCreateSq2[0] = false;
            }
            else { canCreateSq2[0] = false; }
            if (!canCreateRect[1] || !canCreateRect[3]) canCreateSq2[1] = false;
            if (canCreateRect[3] && canCreateRect[4])
            {
                if (posRoom + new Vector3(sBy1dot5, 0, sBy2) == posRooms[i]) canCreateSq2[2] = false;
            }
            else { canCreateSq2[2] = false; }

            if (canCreateRect[6])
            {
                if (!canCreateRect[5] ||
                    posRoom + new Vector3(sBy2Dot5, 0, size) == posRooms[i]) canCreateSq2[3] = false;
                if (!canCreateRect[7] ||
                    posRoom + new Vector3(sBy2Dot5, 0, -size) == posRooms[i]) canCreateSq2[4] = false;
            }
            else { canCreateSq2[3] = false; canCreateSq2[4] = false; }

            if (canCreateRect[8] && canCreateRect[9])
            {
                if (posRoom + new Vector3(sBy1dot5, 0, -sBy2) == posRooms[i]) canCreateSq2[5] = false;
            }
            else { canCreateSq2[5] = false; }
            if (!canCreateRect[9] || !canCreateRect[11]) canCreateSq2[6] = false;
            if (canCreateRect[11] && canCreateRect[12])
            {
                if (posRoom + new Vector3(-sBy1dot5, 0, -sBy2) == posRooms[i]) canCreateSq2[7] = false;
            }
            else { canCreateSq2[7] = false; }

            if (canCreateRect[14])
            {
                if (!canCreateRect[13] ||
                    posRoom + new Vector3(-sBy2Dot5, 0, -size) == posRooms[i]) canCreateSq2[8] = false;
                if (!canCreateRect[15] ||
                    posRoom + new Vector3(-sBy2Dot5, 0, size) == posRooms[i]) canCreateSq2[9] = false;
            }
            else { canCreateSq2[8] = false; canCreateSq2[9] = false; }

            //Octo
            if (posRoom + new Vector3(sDiv2, 0, sBy3) == posRooms[i] ||
                posRoom + new Vector3(-sDiv2, 0, sBy3) == posRooms[i]) { canCreateOcto[0] = false; canCreateOcto[1] = false; }
            else if (posRoom + new Vector3(-sBy1dot5, 0, sBy3) == posRooms[i]) { canCreateOcto[0] = false; }
            else if (posRoom + new Vector3(sBy1dot5, 0, sBy3) == posRooms[i]) { canCreateOcto[1] = false; }

            if (posRoom + new Vector3(sBy3Dot5, 0, size) == posRooms[i] ||
                posRoom + new Vector3(sBy3Dot5, 0, 0) == posRooms[i]||
                posRoom + new Vector3(sBy3Dot5, 0, -size) == posRooms[i]) { canCreateOcto[2] = false; }

            if (posRoom + new Vector3(sDiv2, 0, -sBy3) == posRooms[i] ||
                posRoom + new Vector3(-sDiv2, 0, -sBy3) == posRooms[i]) { canCreateOcto[3] = false; canCreateOcto[4] = false; }
            else if (posRoom + new Vector3(sBy1dot5, 0, -sBy3) == posRooms[i]) { canCreateOcto[3] = false; }
            else if (posRoom + new Vector3(-sBy1dot5, 0, -sBy3) == posRooms[i]) { canCreateOcto[4] = false; }
            

            if (posRoom + new Vector3(-sBy3Dot5, 0, size) == posRooms[i] ||
                posRoom + new Vector3(-sBy3Dot5, 0, 0) == posRooms[i] ||
                posRoom + new Vector3(-sBy3Dot5, 0, -size) == posRooms[i]) { canCreateOcto[2] = false; }

        }

        if (canCreateOcto[0] && !canCreateSq2[0] || !canCreateSq2[1]) { canCreateOcto[0] = false; }
        if (canCreateOcto[1] && !canCreateSq2[1] || !canCreateSq2[2]) { canCreateOcto[1] = false; }
        if (canCreateOcto[2] && !canCreateSq2[3] || !canCreateSq2[4]) { canCreateOcto[2] = false; }
        if (canCreateOcto[3] && !canCreateSq2[5] || !canCreateSq2[6]) { canCreateOcto[3] = false; }
        if (canCreateOcto[4] && !canCreateSq2[6] || !canCreateSq2[7]) { canCreateOcto[4] = false; }
        if (canCreateOcto[5] && !canCreateSq2[8] || !canCreateSq2[9]) { canCreateOcto[5] = false; }

        //Hexa
        for (int i = 0; i < 6; i++) canCreateHexa[i] = false;
        if (canCreateSq2[0] && canCreateSq2[1]) CanCreateHexa[0] = true;
        if (canCreateSq2[1] && canCreateSq2[2]) CanCreateHexa[1] = true;
        if (canCreateSq2[3] && canCreateSq2[4]) CanCreateHexa[2] = true;
        if (canCreateSq2[5] && canCreateSq2[6]) CanCreateHexa[3] = true;
        if (canCreateSq2[6] && canCreateSq2[7]) CanCreateHexa[4] = true;
        if (canCreateSq2[8] && canCreateSq2[9]) CanCreateHexa[5] = true;

        posibilitiesSq = 0;
        posibilitiesRect = 0;
        posibilitiesSq2 = 0;
        posibilitiesHexa = 0;
        posibilitiesOcto = 0;

        for (int i = 0; i <= 5; i++)
        {
            if (canCreateSq[i]) posibilitiesSq++;
            if (canCreateHexa[i]) posibilitiesHexa++;
            if (canCreateOcto[i]) posibilitiesOcto++;
        }
        for (int i = 0; i <= 15; i++) if (canCreateRect[i]) posibilitiesRect++;
        for (int i = 0; i <= 9; i++) if (canCreateSq2[i]) posibilitiesSq2++;
    }
    public override void ActualizeCorridors(ref Vector3[] posCorridors, int posCInit)
    {
        for (int i = 0; i < posCorridors.Length; i++)
        {
            if (posRoom + new Vector3(-sDiv2, 0, sDiv2) == posCorridors[i]) corridors[0] = true;
            if (posRoom + new Vector3(sDiv2, 0, sDiv2) == posCorridors[i]) corridors[1] = true;
            if (posRoom + new Vector3(size, 0, 0) == posCorridors[i]) corridors[2] = true;
            if (posRoom + new Vector3(sDiv2, 0, -sDiv2) == posCorridors[i]) corridors[3] = true;
            if (posRoom + new Vector3(-sDiv2, 0, -sDiv2) == posCorridors[i]) corridors[4] = true;
            if (posRoom + new Vector3(-size, 0, 0) == posCorridors[i]) corridors[5] = true;
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
                auxStruct.vector3 = (new Vector3(posRoom.x - sDiv2, posRoom.y,
                                                 posRoom.z + size));
                auxStruct.arrayBool = new bool[] { false, false, true, false };
                break;
            case 1:
                auxStruct.vector3 = (new Vector3(posRoom.x + sDiv2, posRoom.y,
                                                 posRoom.z + size));
                auxStruct.arrayBool = new bool[] { false, false, true, false };
                break;
            case 2:
                auxStruct.vector3 = (new Vector3(posRoom.x + sBy1dot5,
                                                 posRoom.y, posRoom.z));
                auxStruct.arrayBool = new bool[] { false, false, false, true };
                break;
            case 3:
                auxStruct.vector3 = (new Vector3(posRoom.x + sDiv2, posRoom.y,
                                                 posRoom.z - size));
                auxStruct.arrayBool = new bool[] { true, false, false, false };
                break;
            case 4:
                auxStruct.vector3 = (new Vector3(posRoom.x - sDiv2, posRoom.y,
                                                 posRoom.z - size));
                auxStruct.arrayBool = new bool[] { true, false, false, false };
                break;
            case 5:
                auxStruct.vector3 = (new Vector3(posRoom.x - sBy1dot5,
                                                 posRoom.y, posRoom.z));
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
                auxStruct.vector3 = (new Vector3(posRoom.x - size, posRoom.y,
                                                 posRoom.z + size));
                auxStruct.arrayBool = new bool[] { false, false, false, true, false, false };
                auxStruct.innt = 2;
                break;
            case 1:
                auxStruct.vector3 = (new Vector3(posRoom.x - sDiv2, posRoom.y,
                                                 posRoom.z + sBy1dot5));
                auxStruct.arrayBool = new bool[] { false, false, false, true, false, false };
                break;
            case 2:
                auxStruct.vector3 = (new Vector3(posRoom.x, posRoom.y,
                                                 posRoom.z + size));
                auxStruct.arrayBool = new bool[] { false, false, false, true, true, false };
                auxStruct.innt = 2;
                break;
            case 3:
                auxStruct.vector3 = (new Vector3(posRoom.x + sDiv2, posRoom.y,
                                                 posRoom.z + sBy1dot5));
                auxStruct.arrayBool = new bool[] { false, false, false, true, false, false };
                break;
            case 4:
                auxStruct.vector3 = (new Vector3(posRoom.x + size, posRoom.y,
                                                 posRoom.z + size));
                auxStruct.arrayBool = new bool[] { false, false, false, false, true, false };
                auxStruct.innt = 2;
                break;
            case 5:
                auxStruct.vector3 = (new Vector3(posRoom.x + sBy1dot5, posRoom.y,
                                                 posRoom.z + sDiv2));
                auxStruct.arrayBool = new bool[] { false, false, false, false, true, true };
                break;
            case 6:
                auxStruct.vector3 = (new Vector3(posRoom.x + sBy2, posRoom.y,
                                                 posRoom.z));
                auxStruct.arrayBool = new bool[] { false, false, false, false, false, true };
                auxStruct.innt = 2;
                break;
            case 7:
                auxStruct.vector3 = (new Vector3(posRoom.x + sBy1dot5, posRoom.y,
                                                 posRoom.z - sDiv2));
                auxStruct.arrayBool = new bool[] { false, false, false, false, false, true };
                break;
            case 8:
                auxStruct.vector3 = (new Vector3(posRoom.x + size, posRoom.y,
                                                 posRoom.z - size));
                auxStruct.arrayBool = new bool[] { true, false, false, false, false, false };
                auxStruct.innt = 2;
                break;
            case 9:
                auxStruct.vector3 = (new Vector3(posRoom.x + sDiv2, posRoom.y,
                                                 posRoom.z - sBy1dot5));
                auxStruct.arrayBool = new bool[] { true, false, false, false, false, false };
                break;
            case 10:
                auxStruct.vector3 = (new Vector3(posRoom.x, posRoom.y,
                                                 posRoom.z - size));
                auxStruct.arrayBool = new bool[] { true, true, false, false, false, false };
                auxStruct.innt = 2;
                break;
            case 11:
                auxStruct.vector3 = (new Vector3(posRoom.x - sDiv2, posRoom.y,
                                                 posRoom.z - sBy1dot5));
                auxStruct.arrayBool = new bool[] { true, false, false, false, false, false };
                break;
            case 12:
                auxStruct.vector3 = (new Vector3(posRoom.x - size, posRoom.y,
                                                 posRoom.z - size));
                auxStruct.arrayBool = new bool[] { false, true, false, false, false, false };
                auxStruct.innt = 2;
                break;
            case 13:
                auxStruct.vector3 = (new Vector3(posRoom.x - sBy1dot5, posRoom.y,
                                                 posRoom.z - sDiv2));
                auxStruct.arrayBool = new bool[] { false, true, false, false, false, false };
                break;
            case 14:
                auxStruct.vector3 = (new Vector3(posRoom.x - sBy2, posRoom.y,
                                                 posRoom.z));
                auxStruct.arrayBool = new bool[] { false, false, true, false, false, false };
                auxStruct.innt = 2;
                break;
            case 15:
                auxStruct.vector3 = (new Vector3(posRoom.x - sBy1dot5, posRoom.y,
                                                 posRoom.z + sDiv2));
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
                auxStruct.vector3 = (new Vector3(posRoom.x - size, posRoom.y,
                                                 posRoom.z + sBy1dot5));
                auxStruct.arrayBool = new bool[] { false, false, false, false, true, false, false, false };
                break;
            case 1:
                auxStruct.vector3 = (new Vector3(posRoom.x, posRoom.y,
                                                 posRoom.z + sBy1dot5));
                auxStruct.arrayBool = new bool[] { false, false, false, false, true, true, false, false };
                break;
            case 2:
                auxStruct.vector3 = (new Vector3(posRoom.x + size, posRoom.y,
                                                 posRoom.z + sBy1dot5));
                auxStruct.arrayBool = new bool[] { false, false, false, false, false, true, false, false };
                break;
            case 3:
                auxStruct.vector3 = (new Vector3(posRoom.x + sBy2, posRoom.y,
                                                 posRoom.z + sDiv2));
                auxStruct.arrayBool = new bool[] { false, false, false, false, false, false, true, false };
                break;
            case 4:
                auxStruct.vector3 = (new Vector3(posRoom.x + sBy2, posRoom.y,
                                                 posRoom.z - sDiv2));
                auxStruct.arrayBool = new bool[] { false, false, false, false, false, false, false, true };
                break;
            case 5:
                auxStruct.vector3 = (new Vector3(posRoom.x + size, posRoom.y,
                                                 posRoom.z - sBy1dot5));
                auxStruct.arrayBool = new bool[] { true, false, false, false, false, false, false, false };
                break;
            case 6:
                auxStruct.vector3 = (new Vector3(posRoom.x, posRoom.y,
                                                 posRoom.z - sBy1dot5));
                auxStruct.arrayBool = new bool[] { true, true, false, false, false, false, false, false };
                break;
            case 7:
                auxStruct.vector3 = (new Vector3(posRoom.x - size, posRoom.y,
                                                 posRoom.z - sBy1dot5));
                auxStruct.arrayBool = new bool[] { false, true, false, false, false, false, false, false };
                break;
            case 8:
                auxStruct.vector3 = (new Vector3(posRoom.x - sBy2, posRoom.y,
                                                 posRoom.z - sDiv2));
                auxStruct.arrayBool = new bool[] { false, false, true, false, false, false, false, false };
                break;
            case 9:
                auxStruct.vector3 = (new Vector3(posRoom.x - sBy2, posRoom.y,
                                                 posRoom.z + sDiv2));
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
                auxStruct.vector3 = (new Vector3(posRoom.x - sDiv2, posRoom.y,
                                                 posRoom.z + size));
                auxStruct.arrayBool = new bool[] { false, false, true, false };
                auxStruct.innt = 10;
                break;
            case 1:
                auxStruct.vector3 = (new Vector3(posRoom.x + sDiv2, posRoom.y,
                                                 posRoom.z + size));
                auxStruct.arrayBool = new bool[] { false, false, true, false };
                auxStruct.innt = 10;
                break;
            case 2:
                auxStruct.vector3 = (new Vector3(posRoom.x + sBy1dot5,
                                                 posRoom.y, posRoom.z));
                auxStruct.arrayBool = new bool[] { false, false, false, true };
                auxStruct.innt = 11;
                break;
            case 3:
                auxStruct.vector3 = (new Vector3(posRoom.x + sDiv2, posRoom.y,
                                                 posRoom.z - size));
                auxStruct.arrayBool = new bool[] { true, false, false, false };
                auxStruct.innt = 12;
                break;
            case 4:
                auxStruct.vector3 = (new Vector3(posRoom.x - sDiv2, posRoom.y,
                                                 posRoom.z - size));
                auxStruct.arrayBool = new bool[] { true, false, false, false };
                auxStruct.innt = 12;
                break;
            case 5:
                auxStruct.vector3 = (new Vector3(posRoom.x - sBy1dot5,
                                                 posRoom.y, posRoom.z));
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
        auxStruct.innt = 4;

        switch (cardinal)
        {
            case 0:
                auxStruct.vector3 = (new Vector3(posRoom.x - sDiv2, posRoom.y,
                                                 posRoom.z + sBy1dot5));
                auxStruct.arrayBool = new bool[] { false, true};
                break;
            case 1:
                auxStruct.vector3 = (new Vector3(posRoom.x + sDiv2, posRoom.y,
                                                 posRoom.z + sBy1dot5));
                auxStruct.arrayBool = new bool[] { false, true };
                break;
            case 2:
                auxStruct.vector3 = (new Vector3(posRoom.x + sBy2,
                                                 posRoom.y, posRoom.z));
                auxStruct.arrayBool = new bool[] { false, true };
                auxStruct.innt = 5;
                break;
            case 3:
                auxStruct.vector3 = (new Vector3(posRoom.x + sDiv2, posRoom.y,
                                                 posRoom.z - sBy1dot5));
                auxStruct.arrayBool = new bool[] { true, false };
                break;
            case 4:
                auxStruct.vector3 = (new Vector3(posRoom.x - sDiv2, posRoom.y,
                                                 posRoom.z - sBy1dot5));
                auxStruct.arrayBool = new bool[] { true, false };
                break;
            case 5:
                auxStruct.vector3 = (new Vector3(posRoom.x - sBy2,
                                                 posRoom.y, posRoom.z));
                auxStruct.arrayBool = new bool[] { true, false };
                auxStruct.innt = 5;
                break;
        }
        return auxStruct;
    }
    public override VectArrayBoolInt OctoCreator(int cardinal)
    {
        VectArrayBoolInt auxStruct;
        auxStruct.arrayBool = new bool[4];
        auxStruct.vector3 = new Vector3(0, 0, 0);
        auxStruct.innt = 6;

        switch (cardinal)
        {
            case 0:
                auxStruct.vector3 = (new Vector3(posRoom.x - sDiv2, posRoom.y,
                                                 posRoom.z + sBy2));
                auxStruct.arrayBool = new bool[] { false, false,true,false };
                break;
            case 1:
                auxStruct.vector3 = (new Vector3(posRoom.x + sDiv2, posRoom.y,
                                                 posRoom.z + sBy2));
                auxStruct.arrayBool = new bool[] { false, false, true, false };
                break;
            case 2:
                auxStruct.vector3 = (new Vector3(posRoom.x + sBy2Dot5,
                                                 posRoom.y, posRoom.z));
                auxStruct.arrayBool = new bool[] { false, false, false, true };
                break;
            case 3:
                auxStruct.vector3 = (new Vector3(posRoom.x + sDiv2, posRoom.y,
                                                 posRoom.z - sBy2));
                auxStruct.arrayBool = new bool[] { true, false, false, false };
                break;
            case 4:
                auxStruct.vector3 = (new Vector3(posRoom.x - sDiv2, posRoom.y,
                                                 posRoom.z - sBy2));
                auxStruct.arrayBool = new bool[] { true, false, false, false };
                break;
            case 5:
                auxStruct.vector3 = (new Vector3(posRoom.x - sBy2Dot5,
                                                 posRoom.y, posRoom.z));
                auxStruct.arrayBool = new bool[] { false, true, false, false };
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
                aux.vector3 = posRoom + new Vector3(-sDiv2, 0, sDiv2);
                aux.quaternion = new Quaternion(0, 180, 0, 0);
                break;
            case 1:
                aux.vector3 = posRoom + new Vector3(sDiv2, 0, sDiv2);
                aux.quaternion = new Quaternion(0, 180, 0, 0);
                break;
            case 2:
                aux.vector3 = posRoom + new Vector3(size, 0, 0);
                aux.quaternion = new Quaternion(0, 270, 0, 270);
                break;
            case 3:
                aux.vector3 = posRoom + new Vector3(sDiv2, 0, -sDiv2);
                aux.quaternion = new Quaternion(0, 0, 0, 0);
                break;
            case 4:
                aux.vector3 = posRoom + new Vector3(-sDiv2, 0, -sDiv2);
                aux.quaternion = new Quaternion(0, 0, 0, 0);
                break;
            case 5:
                aux.vector3 = posRoom + new Vector3(-size, 0, 0);
                aux.quaternion = new Quaternion(0, 90, 0, 90);
                break;
        }
        return aux;
    }
    public override void WallsCreator()
    {
        GameObject aux;
        if (corridors[0])
        {
            if (corridors[1]) aux = GameObject.Instantiate(properties.RWall2C, posRoom, Quaternion.identity);
            else aux = GameObject.Instantiate(properties.RWallCL, posRoom, Quaternion.identity);
        }
        else if (corridors[1]) aux = GameObject.Instantiate(properties.RWallCR, posRoom, Quaternion.identity);
        else aux = GameObject.Instantiate(properties.RLargeWall, posRoom, Quaternion.identity);
        aux.transform.parent = gameObjectScene.transform;

        if (corridors[2]) aux = GameObject.Instantiate(properties.RWallC, posRoom, Quaternion.identity);
        else aux = GameObject.Instantiate(properties.RShortWall, posRoom, Quaternion.identity);
        aux.transform.parent = gameObjectScene.transform;

        if (corridors[3])
        {
            if (corridors[4]) aux = GameObject.Instantiate(properties.RWall2C, posRoom, Quaternion.Euler(new Vector3(0, 180, 0)));
            else aux = GameObject.Instantiate(properties.RWallCL, posRoom, Quaternion.Euler(new Vector3(0, 180, 0)));
        }
        else if (corridors[4]) aux = GameObject.Instantiate(properties.RWallCR, posRoom, Quaternion.Euler(new Vector3(0, 180, 0)));
        else aux = GameObject.Instantiate(properties.RLargeWall, posRoom, Quaternion.Euler(new Vector3(0, 180, 0)));
        aux.transform.parent = gameObjectScene.transform;

        if (corridors[5]) aux = GameObject.Instantiate(properties.RWallC, posRoom, Quaternion.Euler(new Vector3(0, 180, 0)));
        else aux = GameObject.Instantiate(properties.RShortWall, posRoom, Quaternion.Euler(new Vector3(0, 180, 0)));
        aux.transform.parent = gameObjectScene.transform;
    }

    public Vector3[] posSubRooms
    {
        get { return _posSubRooms; }
        set { _posSubRooms = value; }
    }
}

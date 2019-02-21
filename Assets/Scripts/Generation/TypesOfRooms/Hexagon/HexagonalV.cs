using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonalV : Room {

    private Vector3[] _posSubRooms = new Vector3[6];
    private Vector3[] posCorridors = new Vector3[10];  

    public HexagonalV(Vector3 pos, bool[] preR, int n) : base(pos, preR, n)
    {
        canCreateSq = new bool[2];
        canCreateRect = new bool[6];
        canCreateSq2 = new bool[4];
        canCreateHexa = new bool[2];
        canCreateOcto = new bool[2];

        corridors = new bool[2];

        for (int i = 0; i < 2; i++)
        {
            canCreateSq[i] = true;
            canCreateHexa[i] = false;
            corridors[i] = false;
            canCreateOcto[i] = true;
        }
        for (int i = 0; i < 6; i++) { canCreateRect[i] = true; }
        for (int i = 0; i < 4; i++) { canCreateSq2[i] = true; }

        PosSubRooms[0] = new Vector3(-size, 0, sDiv2) + pos;
        PosSubRooms[1] = new Vector3(0, 0, sDiv2) + pos;
        PosSubRooms[2] = new Vector3(size, 0, sDiv2) + pos;
        PosSubRooms[3] = new Vector3(size, 0, -sDiv2) + pos;
        PosSubRooms[4] = new Vector3(0, 0, -sDiv2) + pos;
        PosSubRooms[5] = new Vector3(-size, 0, -sDiv2) + pos;

        PosCorridors[0] = posRoom + new Vector3(-size, 0, size) + pos;
        PosCorridors[1] = posRoom + new Vector3(0, 0, size);
        PosCorridors[2] = posRoom + new Vector3(size, 0, size) + pos;
        PosCorridors[3] = posRoom + new Vector3(sBy1dot5, 0, sDiv2);
        PosCorridors[4] = posRoom + new Vector3(sBy1dot5, 0, -sDiv2);
        PosCorridors[5] = posRoom + new Vector3(size, 0, -size);
        PosCorridors[6] = posRoom + new Vector3(0, 0, -size);
        PosCorridors[7] = posRoom + new Vector3(-size, 0, -size);
        PosCorridors[7] = posRoom + new Vector3(-sBy1dot5, 0, -sDiv2);
        PosCorridors[7] = posRoom + new Vector3(-sBy1dot5, 0, sDiv2);

        gameObjectScene = GameObject.Instantiate(properties.Hexagonal, pos,
                                            Quaternion.Euler(0, 90, 0),
                                            properties.Rooms.transform);
        gameObjectScene.name = "Room" + n;
    }

    public override void ActualizeCanCreate(ref Vector3[] posRooms, int posInit)
    {
        for (int i = 0; i < posInit; i++)
        {
            //
            if (posRoom + new Vector3(0, 0, sBy1dot5) == posRooms[i]) { canCreateSq[0] = false; }
            if (posRoom + new Vector3(0, 0, -sBy1dot5) == posRooms[i]) { canCreateSq[1] = false; }
            //
            if (canCreateSq[0])
            {
                if (posRoom + new Vector3(-size, 0, sBy1dot5) == posRooms[i]) { canCreateRect[0] = false; }
                if (posRoom + new Vector3(0, 0, sBy2Dot5) == posRooms[i]) { canCreateRect[1] = false; }
                if (posRoom + new Vector3(size, 0, sBy1dot5) == posRooms[i]) { canCreateRect[2] = false; }
            }
            else { canCreateRect[0] = false; canCreateRect[1] = false; canCreateRect[2] = false; }

            if (canCreateSq[1])
            {
                if (posRoom + new Vector3(size, 0, -sBy1dot5) == posRooms[i]) { canCreateRect[3] = false; }
                if (posRoom + new Vector3(0, 0, -sBy2Dot5) == posRooms[i]) { canCreateRect[4] = false; }
                if (posRoom + new Vector3(-size, 0, -sBy1dot5) == posRooms[i]) { canCreateRect[5] = false; }
            }
            else { canCreateRect[3] = false; canCreateRect[4] = false; canCreateRect[5] = false; }
            //
            if (canCreateRect[1])
            {
                if (canCreateRect[0])
                {
                    if (posRoom + new Vector3(-size, 0, sBy2Dot5) == posRooms[i]) { canCreateSq2[0] = false; }
                }
                else { canCreateSq2[0] = false; }
                if (canCreateRect[2])
                {
                    if (posRoom + new Vector3(size, 0, sBy2Dot5) == posRooms[i]) { canCreateSq2[1] = false; }
                }
                else { canCreateSq2[1] = false; }
            }
            else { canCreateSq2[0] = false; canCreateSq2[1] = false; }

            if (canCreateRect[4])
            {
                if (canCreateRect[3])
                {
                    if (posRoom + new Vector3(size, 0, -sBy2Dot5) == posRooms[i]) { canCreateSq2[2] = false; }
                }
                else { canCreateSq2[2] = false; }
                if (canCreateRect[5])
                {
                    if (posRoom + new Vector3(-size, 0, -sBy2Dot5) == posRooms[i]) { canCreateSq2[3] = false; }
                }
                else { canCreateSq2[3] = false; }
            }
            else { canCreateSq2[2] = false; canCreateSq2[3] = false; }

            //Octo
            if (posRoom + new Vector3(size, 0, sBy3) == posRooms[i] ||
                posRoom + new Vector3(0, 0, sBy3) == posRooms[i] ||
                posRoom + new Vector3(-size, 0, sBy3) == posRooms[i]) { canCreateOcto[0] = false; }
            if (posRoom + new Vector3(size, 0, -sBy3) == posRooms[i] ||
                posRoom + new Vector3(-0, 0, -sBy3) == posRooms[i] ||
                posRoom + new Vector3(-size, 0, -sBy3) == posRooms[i]) { canCreateOcto[1] = false; }        
        }

        if (canCreateOcto[0] && !canCreateSq2[0] || !canCreateSq2[1]) { canCreateOcto[0] = false; }
        if (canCreateOcto[1] && !canCreateSq2[2] || !canCreateSq2[3]) { canCreateOcto[1] = false; }

        //
        for (int i = 0; i < 2; i++) canCreateHexa[i] = false;
        if (canCreateSq2[0] && canCreateSq2[1]) { CanCreateHexa[0] = true; }
        if (canCreateSq2[2] && canCreateSq2[3]) { CanCreateHexa[1] = true; }

        posibilitiesSq = 0;
        posibilitiesRect = 0;
        posibilitiesSq2 = 0;
        posibilitiesHexa = 0;
        posibilitiesOcto = 0;

        for (int i = 0; i <= 1; i++)
        {
            if (canCreateSq[i]) posibilitiesSq++;
            if (canCreateHexa[i]) posibilitiesHexa++;
            if (canCreateOcto[i]) posibilitiesOcto++;
        }
        for (int i = 0; i <= 5; i++) if (canCreateRect[i]) posibilitiesRect++;
        for (int i = 0; i <= 3; i++) if (canCreateSq2[i]) posibilitiesSq2++;
    }
    public override void ActualizeCorridors(ref Vector3[] posCorridors, int posCInit)
    {
        for (int i = 0; i < posCorridors.Length; i++)
        {
            if (posRoom + new Vector3(size, 0, 0) == posCorridors[i]) corridors[0] = true;
            if (posRoom + new Vector3(-size, 0, 0) == posCorridors[i]) corridors[1] = true;
        }
    }

    public override VectArrayBoolInt SqCreator(int cardinal)
    {
        VectArrayBoolInt auxStruct;
        auxStruct.arrayBool = new bool[4];
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
                auxStruct.vector3 = (new Vector3(posRoom.x,
                                                 posRoom.y, posRoom.z - sBy1dot5));
                auxStruct.arrayBool = new bool[] { true, false, false, false };
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
                auxStruct.arrayBool = new bool[] { false, false, false, false, false, true };
                auxStruct.innt = 2;
                break;
            case 3:
                auxStruct.vector3 = (new Vector3(posRoom.x + sDiv2, posRoom.y,
                                                 posRoom.z - sBy1dot5));
                auxStruct.arrayBool = new bool[] { true, false, false, false, false, false };
                auxStruct.innt = 2;
                break;
            case 4:
                auxStruct.vector3 = (new Vector3(posRoom.x, posRoom.y,
                                                 posRoom.z - sBy2));
                auxStruct.arrayBool = new bool[] { true, false, false, false, false, false };
                break;
            case 5:
                auxStruct.vector3 = (new Vector3(posRoom.x - sDiv2, posRoom.y,
                                                 posRoom.z - sBy1dot5));
                auxStruct.arrayBool = new bool[] { false, true, false, false, false, false };
                auxStruct.innt = 2;
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
                auxStruct.vector3 = (new Vector3(posRoom.x + sDiv2, posRoom.y,
                                                 posRoom.z - sBy2));
                auxStruct.arrayBool = new bool[] { true, false, false, false, false, false, false, false };
                break;
            case 3:
                auxStruct.vector3 = (new Vector3(posRoom.x - sDiv2, posRoom.y,
                                                 posRoom.z - sBy2));
                auxStruct.arrayBool = new bool[] { false, true, false, false, false, false, false, false };
                break;
        }
        return auxStruct;
    }
    public override VectArrayBoolInt TriCreator(int cardinal)
    {
        VectArrayBoolInt auxStruct;
        auxStruct.arrayBool = new bool[4];
        auxStruct.vector3 = new Vector3(0, 0, 0);
        auxStruct.innt = 10;

        switch (cardinal)
        {
            case 0:
                auxStruct.vector3 = (new Vector3(posRoom.x,
                                                 posRoom.y, posRoom.z + sBy1dot5));
                auxStruct.arrayBool = new bool[] { false, false, true, false };
                break;
            case 1:
                auxStruct.vector3 = (new Vector3(posRoom.x,
                                                 posRoom.y, posRoom.z - sBy1dot5));
                auxStruct.arrayBool = new bool[] { true, false, false, false };
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
                auxStruct.vector3 = (new Vector3(posRoom.x,
                                                 posRoom.y, posRoom.z + sBy2));
                auxStruct.arrayBool = new bool[] { false, true };
                break;
            case 1:
                auxStruct.vector3 = (new Vector3(posRoom.x,
                                                 posRoom.y, posRoom.z - sBy2));
                auxStruct.arrayBool = new bool[] { true, false };
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
                auxStruct.vector3 = (new Vector3(posRoom.x,
                                                 posRoom.y, posRoom.z + sBy2Dot5));
                auxStruct.arrayBool = new bool[] { false, false, true, false };
                break;
            case 1:
                auxStruct.vector3 = (new Vector3(posRoom.x,
                                                 posRoom.y, posRoom.z - sBy2Dot5));
                auxStruct.arrayBool = new bool[] { true, false, false, false };
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
                aux.quaternion = Quaternion.identity;
                break;
            case 1:
                aux.vector3 = posRoom + new Vector3(0, 0, -size);
                aux.quaternion = Quaternion.identity;
                break;
        }
        return aux;
    }
    public override void WallsCreator()
    {
        throw new System.NotImplementedException();
    }

    public Vector3[] PosSubRooms
    {
        get
        {
            return _posSubRooms;
        }

        set
        {
            _posSubRooms = value;
        }
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

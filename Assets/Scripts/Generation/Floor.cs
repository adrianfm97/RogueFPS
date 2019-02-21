using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor {

    private List<Room> rooms;
    private List<Corridor> corridors;
    private int roomCounter;

    public Vector3[] posRooms, posCorr, posNoCorr;
    public int rInitialized, cInitialized, nCInitialized;

    private Properties properties;    

    public Floor() {
        properties = GameObject.FindObjectOfType<Properties>();

        rooms = new List<Room>(); Corridors = new List<Corridor>();

        posRooms     = new Vector3[properties.NumRoom * 4 + 1];
        posCorr = new Vector3[properties.NumRoom * 8];
        posNoCorr = new Vector3[properties.NumRoom * 8];

        posRooms[0] = new Vector3(0, 0, -25);
        posNoCorr[0] = new Vector3(-12.5f, 0, -25); posNoCorr[1] = new Vector3(12.5f, 0, -25);
        posNoCorr[2] = new Vector3(0, 0, -37.5f); posNoCorr[3] = new Vector3(0, 0, -12.5f);

        posCorr[0] = new Vector3(0, 0, -12.5f);
        roomCounter = 0; rInitialized = 1; cInitialized = 1; nCInitialized = 4;  
    }

    public void AddRoom(Room r) {
        if (r is Square)
        {
            posRooms[rInitialized] = r.PosRoom;
            rInitialized++;
        }
        else if (r is RectangleV)
        {
            RectangleV auxRectangleV = (RectangleV)r;
            for (int i = 0; i <= 1; i++)
            {
                posRooms[rInitialized] = auxRectangleV.posSubRooms[i];
                rInitialized++;
            }
        }
        else if (r is RectangleH)
        {
            RectangleH auxRectangleH = (RectangleH)r;
            for (int i = 0; i <= 1; i++)
            {
                posRooms[rInitialized] = auxRectangleH.posSubRooms[i];
                rInitialized++;
            }
        }
        else if (r is Triangle)
        {
            Triangle auxTriangle = (Triangle)r;
            posRooms[rInitialized] = auxTriangle.PosRoom;
            rInitialized++;

            for (int i = 0; i <= 3; i++)
            {
                if (!auxTriangle.PrevRoom[i])
                {
                    posNoCorr[nCInitialized] = auxTriangle.PosCorridors[i];
                    nCInitialized++;
                }
            }
        }
        else if (r is HexagonalV) {
            HexagonalV auxHexagonalV = (HexagonalV)r;
            for (int i = 0; i <= 5; i++) {
                posRooms[rInitialized] = auxHexagonalV.PosSubRooms[i];
                rInitialized++;
            }
            for (int i = 0; i <= 5; i++) {
                posNoCorr[nCInitialized] = auxHexagonalV.PosCorridors[i];
                nCInitialized++;                
            }
        }
        else if (r is HexagonalH) {
            HexagonalH auxHexagonalH = (HexagonalH)r;
            for (int i = 0; i <= 5; i++)
            {
                posRooms[rInitialized] = auxHexagonalH.PosSubRooms[i];
                rInitialized++;
            }
            for (int i = 0; i <= 5; i++)
            {
                posNoCorr[nCInitialized] = auxHexagonalH.PosCorridors[i];
                nCInitialized++;
            }
        }
        else if (r is Octagon)
        {
            Octagon auxOctagon = (Octagon)r;
            for (int i = 0; i <= 8; i++)
            {
                posRooms[rInitialized] = auxOctagon.PosSubRooms[i];
                rInitialized++;
            }
            for (int i = 0; i <= 7; i++)
            {
                posNoCorr[nCInitialized] = auxOctagon.PosCorridors[i];
                nCInitialized++;
            }
        }
        else
        {
            Square2 auxSquare2 = (Square2)r;
            for (int i = 0; i <= 3; i++)
            {
                posRooms[rInitialized] = auxSquare2.PosSubRooms[i];
                rInitialized++;
            }
        }        
        rooms.Add(r);
        roomCounter++;
    }

    public void AddCorridors() {
        VectQuater aux;
        bool canCreate;
        int cardinal;

        foreach (Room room in rooms)
        {

            //Main Corridor
            cardinal = 0;
            canCreate = true;
            
            if (room is Triangle)
            {
                while (!room.PrevRoom[cardinal]) cardinal++;
                aux = room.CorridorCreator(cardinal);

                for (int j = 0; j < nCInitialized; j++) {
                    if (aux.vector3 == posNoCorr[j]) continue;
                }
                
                posCorr[cInitialized] = aux.vector3;
                posNoCorr[nCInitialized] = aux.vector3;
                cInitialized++;
                nCInitialized++;

                if (aux.quaternion.eulerAngles.y == 90 ||
                    aux.quaternion.eulerAngles.y == 270) {
                    Corridors.Add(new CorrH(aux.vector3, aux.quaternion, cInitialized));
                }
                else {
                    Corridors.Add(new CorrV(aux.vector3, aux.quaternion, cInitialized));
                }

                continue;
            }           

            if (!(room is Octagon)) room.ActualizeCanCreate(ref posRooms, rInitialized); 
            
            while (!room.PrevRoom[cardinal]) cardinal++;
            aux = room.CorridorCreator(cardinal);

            for (int j = 0; j < nCInitialized; j++)
            {
                if (canCreate && aux.vector3 == posNoCorr[j]) canCreate = false;
            }

            if (canCreate) {
                room.Corridors[cardinal] = true;
                posCorr[cInitialized] = aux.vector3;
                posNoCorr[nCInitialized] = aux.vector3;
                cInitialized++;
                nCInitialized++;
                if (aux.quaternion.eulerAngles.y == 90||
                    aux.quaternion.eulerAngles.y == 270) {
                    Corridors.Add(new CorrH(aux.vector3, aux.quaternion, cInitialized));
                }
                else {
                    Corridors.Add(new CorrV(aux.vector3, aux.quaternion, cInitialized));
                }                              
            }
            if (room is Octagon) continue;

            //Other Corridors            
            
            for (int i = 0; i < room.CanCreateSq.Length; i++) {

                if (!room.CanCreateSq[i] && i != cardinal &&
                     Random.Range(0, 10) < properties.ProbCorrid) {                    
                    aux = room.CorridorCreator(i);

                    for (int j = 0; j < nCInitialized; j++) {
                        if (canCreate && aux.vector3 == posNoCorr[j]) canCreate = false;
                    }

                    if (canCreate) {
                        room.Corridors[i] = true;
                        posCorr[cInitialized] = aux.vector3;
                        posNoCorr[nCInitialized] = aux.vector3;
                        cInitialized++;
                        nCInitialized++;

                        if (aux.quaternion.eulerAngles.y == 90 ||
                            aux.quaternion.eulerAngles.y == 270) {
                            Corridors.Add(new CorrH(aux.vector3, aux.quaternion, cInitialized));
                        }
                        else {
                            Corridors.Add(new CorrV(aux.vector3, aux.quaternion, cInitialized));
                        }                        
                    }
                }
            }            
        } //foreach ends
    }

    public void AddWalls() {
        foreach (Room room in rooms) {
            if (room is Octagon) continue;
            if (!(room is Triangle)) room.ActualizeCorridors(ref posCorr, nCInitialized); 
            if(!(room is HexagonalH || room is HexagonalV)) room.WallsCreator();
        }
    }

    public void Colindant() {
        foreach (Corridor c in Corridors) {
            c.ActualizeColindant(ref rooms);
            rooms[c.NumRooms[0]].AddColindantRoom(c.NumRooms[1]);
            rooms[c.NumRooms[1]].AddColindantRoom(c.NumRooms[0]);
            rooms[c.NumRooms[0]].ColindantCorridors.Add(c.Num);
            rooms[c.NumRooms[1]].ColindantCorridors.Add(c.Num);
        }
    }

    public List<Room> Rooms
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
    public List<Corridor> Corridors
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

    public int RInitialized
    {
        get
        {
            return rInitialized;
        }

        set
        {
            rInitialized = value;
        }
    }
    public int CInitialized
    {
        get
        {
            return cInitialized;
        }

        set
        {
            cInitialized = value;
        }
    }
       
}

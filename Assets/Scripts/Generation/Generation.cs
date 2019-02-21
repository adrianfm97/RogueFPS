using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generation : MonoBehaviour {
    private Properties properties;
    public float size;

    private Floor floor;    
    private int pointerRoom = 0;    

    private void Awake() {
        properties = GameObject.FindObjectOfType<Properties>();

        VectArrayBoolInt aux;

        floor = new Floor();

        floor.AddRoom(new Square(new Vector3(0, 0, 0),
                                 new bool[] { false, false, true, false }, 0));
        
        for (int i = 1; i < properties.NumRoom - 1; i++)
        {
            aux = CardinalSelector(RoomSelector(i - 1));

            switch (aux.innt)
            {
                case 0: 
                    floor.AddRoom(new Square(aux.vector3, aux.arrayBool, i));
                    break;
                case 1: 
                    floor.AddRoom(new RectangleV(aux.vector3, aux.arrayBool, i));
                    break;
                case 2:
                    floor.AddRoom(new RectangleH(aux.vector3, aux.arrayBool, i));
                    break;
                case 3: 
                    floor.AddRoom(new Square2(aux.vector3, aux.arrayBool, i));
                    break;
                case 4:
                    floor.AddRoom(new HexagonalV(aux.vector3, aux.arrayBool, i));
                    break;
                case 5:
                    floor.AddRoom(new HexagonalH(aux.vector3, aux.arrayBool, i));
                    break;
                case 10:
                    floor.AddRoom(new Triangle(aux.vector3, aux.arrayBool,
                                        i, Quaternion.identity));
                    break;
                case 11: 
                    floor.AddRoom(new Triangle(aux.vector3, aux.arrayBool,
                                        i, Quaternion.Euler(new Vector3(0, 90, 0))));
                    break;
                case 12:
                    floor.AddRoom(new Triangle(aux.vector3, aux.arrayBool,
                                        i, Quaternion.Euler(new Vector3(0, 180, 0))));
                    break;
                case 13:
                    floor.AddRoom(new Triangle(aux.vector3, aux.arrayBool,
                                        i, Quaternion.Euler(new Vector3(0, 270, 0))));
                    break;
            }
        }
        //Debugger();
        aux = BossRoomSelector();
        floor.AddRoom(new Octagon(aux.vector3, aux.arrayBool, properties.NumRoom));
        floor.AddCorridors();        
        floor.AddWalls();
    }

    private int RoomSelector(int num) {

        Room room = floor.Rooms[num];
        if (room is Triangle) return RoomSelector(num - 1);        

        room.ActualizeCanCreate(ref floor.posRooms, floor.RInitialized);        

        if (num == pointerRoom && room.PosibilitiesSq == 0) {
            pointerRoom++;
            return RoomSelector(num + 1);
        }
        else if (num == pointerRoom) { return num; }

        int rand = Random.Range(0, 100);
        if (room is Square)
        {
            switch (room.PosibilitiesSq)
            {
                case 1:
                    if (rand <= 20) { return num; }
                    break;
                case 2:
                    if (rand <= 40) { return num; }
                    break;
                case 3:
                    if (rand <= 60) { return num; }
                    break;
            }
        }
        else if (room is RectangleV || room is RectangleH)
        {
            switch (room.PosibilitiesSq)
            {
                case 1:
                    if (rand <= 18) { return num; }
                    break;
                case 2:
                    if (rand <= 36) { return num; }
                    break;
                case 3:
                    if (rand <= 54) { return num; }
                    break;
                case 4:
                    if (rand <= 72) { return num; }
                    break;
                case 5:
                    if (rand <= 90) { return num; }
                    break;
            }
        }
        else if(room is HexagonalV || room is HexagonalH)
        {
            switch (room.PosibilitiesSq)
            {
                case 1:
                    if(rand <= 60) { return num; }
                    break;
            }
        }
        else
        {
            switch (room.PosibilitiesSq)
            {
                case 1:
                    if (rand <= 20) { return num; }
                    break;
                case 2:
                    if (rand <= 40) { return num; }
                    break;
                case 3:
                    if (rand <= 50) { return num; }
                    break;
                case 4:
                    if (rand <= 60) { return num; }
                    break;
                case 5:
                    if (rand <= 70) { return num; }
                    break;
                case 6:
                    if (rand <= 80) { return num; }
                    break;
                case 7:
                    if (rand <= 90) { return num; }
                    break;
            }
        }

        return RoomSelector(num - 1);
    }

    private VectArrayBoolInt CardinalSelector(int num) {
        Room room = floor.Rooms[num];
        int rand;
        VectArrayBoolInt aux = new VectArrayBoolInt();

        int sqMax = 4; int rectMax = 12; int sq2Max = 8;
        if (room is RectangleV || room is RectangleH) { sqMax = 6; rectMax = 16; sq2Max = 10; }
        else if (room is HexagonalH || room is HexagonalV) { sqMax = 2; rectMax = 6; sq2Max = 4; }
        else if (room is Square2) { sqMax = 8; rectMax = 20; sq2Max = 12; }

        switch (TypeOfRoom(num)) {
            case 0: //SQ
                do rand = Random.Range(0, sqMax);
                while (!room.CanCreateSq[rand]);
                aux = room.SqCreator(rand);
                break;
            case 1: //RECT
                do rand = Random.Range(0, rectMax);
                while (!room.CanCreateRect[rand]);
                aux = room.RectCreator(rand);                
                break;
            case 2: //SQ2
                do rand = Random.Range(0, sq2Max);
                while (!room.CanCreateSq2[rand]);
                aux = room.Sq2Creator(rand);
                break;
            case 3: //TRI
                do rand = Random.Range(0, sqMax);
                while (!room.CanCreateSq[rand]) ;
                aux = room.TriCreator(rand);
                break;
            case 4: //HEXA
                do rand = Random.Range(0, sqMax);
                while (!room.CanCreateHexa[rand]);
                aux = room.HexaCreator(rand);
                break;
        }
        return aux;
    }
   
    private int TypeOfRoom(int num) {

        Room room = floor.Rooms[num];
        int rand = Random.Range(0, 100);

        if (room.PosibilitiesSq2 != 0
            && rand <= properties.ProbSq2) return 2;

        else if (room.PosibilitiesRect != 0
                 && rand <= properties.ProbSq2 + properties.ProbRect) return 1;

        else if (room.PosibilitiesHexa != 0
                 && rand <= properties.ProbSq2 +
                 properties.ProbRect + properties.ProbHexa) return 4;

        else if (rand <= properties.ProbTri + properties.ProbSq2
                 + properties.ProbRect) return 3;
        else return 0;
    }

    private VectArrayBoolInt BossRoomSelector() {
        int[] furtherRooms = { 0, 0, 0 };
        float[] lengthRooms = { 0, 0, 0 };

        foreach (Room room in floor.Rooms) {
            if (room is Triangle) continue;
            room.ActualizeCanCreate(ref Floor.posRooms, Floor.rInitialized);            
            
            float lengthRoom = room.PosRoom.magnitude;
            Debug.Log(room.Num + "  Octo: " +room.PosibilitiesOcto + "  Hexa: " + room.PosibilitiesHexa);

            if (room.PosibilitiesOcto > 0 && lengthRoom > lengthRooms[0]) {

                if (lengthRooms[1] > lengthRooms[0]) {
                    furtherRooms[1] = furtherRooms[0];
                    lengthRooms[1] = lengthRooms[0];
                }
                else if (lengthRooms[0] > lengthRooms[2])
                {
                    furtherRooms[2] = furtherRooms[0];
                    lengthRooms[2] = lengthRooms[0];
                }

                furtherRooms[0] = room.Num;
                lengthRooms[0] = room.PosRoom.magnitude;                
            }
            else if (room.PosibilitiesOcto > 0 && lengthRoom > lengthRooms[1])
            {
                if (lengthRooms[1] > lengthRooms[2])
                {
                    furtherRooms[2] = furtherRooms[1];
                    lengthRooms[2] = lengthRooms[1];
                }

                furtherRooms[1] = room.Num;
                lengthRooms[1] = room.PosRoom.magnitude;
            }
            else if (room.PosibilitiesOcto > 0 && lengthRoom > lengthRooms[2]) {
                furtherRooms[2] = room.Num;
                lengthRooms[2] = room.PosRoom.magnitude;
            }
        }
        Room preBossRoom;
        do { preBossRoom = floor.Rooms[furtherRooms[(int)Random.Range(0, 3)]]; }
        while (preBossRoom.PosibilitiesOcto == 0 && preBossRoom.Num == 0);

        int octoMax = 4;
        if (preBossRoom is RectangleV || preBossRoom is RectangleH) octoMax = 6;
        else if (preBossRoom is HexagonalH || preBossRoom is HexagonalV) octoMax = 2;
        else if (preBossRoom is Square2) octoMax = 8;

        int rand;
        do { rand = Random.Range(0, octoMax); }
        while (!preBossRoom.CanCreateOcto[rand]);
        Debug.Log("canCreateOcto["+rand+"]");
        Debug.Log(furtherRooms[0] +" "+furtherRooms[1]+" "+furtherRooms[2]);
        return preBossRoom.OctoCreator(rand);
    }

    /*private void Debugger()
    {
        //for (int i = 0; i < floor.rInitialized; i++) Debug.Log(floor.posRooms[i]);

        foreach (Room room in floor.Rooms)
        {
            if ((room is Square))
            {

                room.ActualizeCanCreate(ref floor.posRooms, floor.rInitialized);

                Debug.Log("Room" + room.Num);
                Debug.Log("      Sq:  " + room.PosibilitiesSq);                
                Debug.Log(cadena);
                //Debug.Log("      Rect:" + room.PosibilitiesRect);
                //Debug.Log("      Sq2: " + room.PosibilitiesSq2);
                //Debug.Log("      Octo:" + room.PosibilitiesOcto);
                //Debug.Log("      Hexa:" + room.PosibilitiesHexa);
                Debug.Log("-----------------");
            }
        }
        

    }*/

    public Floor Floor
    {
        get
        {
            return floor;
        }

        set
        {
            floor = value;
        }
    }
}

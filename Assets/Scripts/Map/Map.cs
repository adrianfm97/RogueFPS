using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {
    [SerializeField]
    private Properties properties;

    private Floor floor;
    private GameObject map;

    [SerializeField]
    private GameObject player;
    

    [HideInInspector]
    public List<int> colindantRooms;

    private GameObject[] corridors, rooms;
      
    private GameObject playerMapPosition;
    private Vector3 toFitInMap;

    private void Awake()
    {        
        map = this.gameObject;        
    }

    private void Start()
    {        
        floor = GameObject.FindObjectOfType<Generation>().Floor;   
        

        rooms = new GameObject[floor.Rooms.Count];
        corridors = new GameObject[floor.Corridors.Count];

        
        MapCreation();
        floor.Colindant();
        map.SetActive(false);
    }

    private void Update()
    {
        PlayerPosition();
    }

    private void MapCreation()
    {
        SetMaxes();        

        int i = 0; 

        foreach (Room room in Floor.Rooms)
        {
            if (room is Square)
            {
                rooms[i] = Instantiate(properties.MapSquare, map.transform.position + toFitInMap,
                                       Quaternion.identity, map.transform);
            }
            else if (room is RectangleH)
            {
                rooms[i] = Instantiate(properties.MapRectH, map.transform.position + toFitInMap,
                                       Quaternion.identity, map.transform);
            }
            else if (room is RectangleV)
            {
                rooms[i] = Instantiate(properties.MapRectV, map.transform.position + toFitInMap,
                                       Quaternion.identity, map.transform);
            }
            else if (room is Triangle) {
                rooms[i] = Instantiate(properties.MapTriangle, map.transform.position + toFitInMap,
                                       Quaternion.identity, map.transform);
            }
            else
            {
                rooms[i] = Instantiate(properties.MapSquare2, map.transform.position + toFitInMap,
                                       Quaternion.identity, map.transform);
            }
            rooms[i].transform.position += new Vector3(room.PosRoom.x,
                                                       room.PosRoom.z, 0);
            rooms[i].transform.SetParent(properties.MapRooms.transform);
            rooms[i].name = "Room " + i;
            if (i != 0) { rooms[i].SetActive(false); }
            i++;
        }
        i = 0;

        corridors[0] = Instantiate(properties.MapCorridor, map.transform.position + toFitInMap,
                                   Quaternion.identity, map.transform);
        corridors[0].transform.position += new Vector3(0, -12.5f, 0);
        corridors[0].transform.SetParent(properties.MapCorridors.transform);
        corridors[0].name = "Corridor 0";

        foreach (Corridor corr in Floor.Corridors)
        {
            if (corr is CorrH)
            {
                corridors[i] = Instantiate(properties.MapCorridor, map.transform.position + toFitInMap,
                                           Quaternion.Euler(0, 0, 90), map.transform);
            }
            else
            {
                corridors[i] = Instantiate(properties.MapCorridor, map.transform.position + toFitInMap,
                                           Quaternion.identity, map.transform);
            }

            corridors[i].transform.position += new Vector3(corr.Pos.x, corr.Pos.z, 0);
            corridors[i].transform.parent = properties.MapCorridors.transform;
            corridors[i].name = "Corridor " + i;
            corridors[i].SetActive(false);
            i++;
        }

        playerMapPosition = Instantiate(properties.MapPlayerPosition, map.transform.position + toFitInMap,
                                                   Quaternion.identity, map.transform);
        playerMapPosition.name = "Player";
    }

    private void SetMaxes() {
        int aux = floor.posRooms.Length;
        float tMax = 0, bMax = 0, lMax = 0, rMax = 0;
        for (int i = 0; i < aux; i++) {
            if (tMax < floor.posRooms[i].x) {
                tMax = floor.posRooms[i].x;
            }
            else if (bMax > floor.posRooms[i].x) {
                bMax = floor.posRooms[i].x;
            }

            if (rMax < floor.posRooms[i].z) {
                rMax = floor.posRooms[i].z;
            }
            else if (lMax > floor.posRooms[i].z) {
                lMax = floor.posRooms[i].z;
            }
        }
        toFitInMap = new Vector3(-(tMax + bMax) / 2, -(rMax + lMax) / 2, 0);
    }

    public void ActualizePosibleRooms(int n) {
        foreach (int num in Floor.Rooms[n].ColindantRooms) {            
            rooms[num].SetActive(true);            
        }
        foreach (int num in Floor.Rooms[n].ColindantCorridors) {
            corridors[num - 1].SetActive(true);
        }
        
       
    }      

    private void PlayerPosition() {
        Vector3 aux = new Vector3(player.transform.position.x, player.transform.position.z, 0);
        playerMapPosition.transform.position = aux + map.transform.position + toFitInMap; 
    }

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

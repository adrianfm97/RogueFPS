using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocation : MonoBehaviour {
    private Properties properties;
    private Map map;
    private Floor floor;

    private void Awake()
    {
        properties = GameObject.FindObjectOfType<Properties>();
        map = GameObject.FindObjectOfType<Map>();
        
    }
    private void Start()
    {
        floor = GameObject.FindObjectOfType<Generation>().Floor;
    }

    private void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("Room"))
        {
            map.ActualizePosibleRooms(int.Parse(c.name.Substring(4)));
        }
    }
}

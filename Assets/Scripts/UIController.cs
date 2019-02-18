using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public GameObject map;

	void Update () {
        enableMap();
    }

    private void enableMap() {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            map.SetActive(true);
        }

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            map.SetActive(false);
        }
    } 
}

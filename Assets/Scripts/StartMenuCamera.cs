using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuCamera : MonoBehaviour {
    
    public float radiumTranslation;
    public float heigth;
    public float velocity;
    public float rotationX;

    void Awake () {
        transform.position = new Vector3(radiumTranslation, heigth, 0);
	}
	
	void Update () {
        Translation();
	}

    private void Translation() {
        transform.LookAt(new Vector3(0, rotationX, 0));
        transform.Translate(Vector3.right * velocity * Time.deltaTime);
    }
}

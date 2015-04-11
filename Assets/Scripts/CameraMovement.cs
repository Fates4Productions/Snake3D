using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

    //public Transform camera;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        float x = 6.0f * Mathf.Sin(Time.time / 3.5f);
        float y = 1.6f * Mathf.Cos(Time.time / 3.5f);
        GetComponent<Camera>().transform.position = new Vector3(x, y, -40);
	}
}

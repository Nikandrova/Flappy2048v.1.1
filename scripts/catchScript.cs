using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class catchScript : MonoBehaviour {
    public Transform catchCube;

	void Update () {
        if (catchCube.GetComponent<Transform>().position.x < -75f )
        {
            catchCube.GetComponent<Transform>().position = new Vector3(630f, 20f, 230);
        }
	}
}

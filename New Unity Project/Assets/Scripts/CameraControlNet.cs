using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CameraControlNet : NetworkBehaviour {

    public Camera cam;

	
	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer)
        {
            cam.enabled = false;
            return;
        }
	}
}

using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class CameraController : NetworkBehaviour {

    [SerializeField]
    Camera PlayerCam;
	// Use this for initialization
	void Start ()
    {
        if(!isLocalPlayer)
        {
            GetComponent<PlayerController>().enabled = true;
            PlayerCam.enabled = true;
        }
	
       
	}
	
}

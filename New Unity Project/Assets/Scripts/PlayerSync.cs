using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerSync : NetworkBehaviour {

    [SyncVar]
    private Vector3 syncPos;

    [SerializeField]Transform mytransf;
    [SerializeField]float lerprate = 15;

    // Use this for initialization
   
	// Update is called once per frame
	void FixedUpdate()
    {
        TransmitPosition();
        LerpPostion();
	}
    void LerpPostion()
    {
    if(!isLocalPlayer)
      {
            mytransf.position = Vector3.Lerp(mytransf.position, syncPos, Time.deltaTime * lerprate);
       }
    }
    [Command]
    void CmdProvidePostionToServer(Vector3 pos)
    {
        syncPos = pos;
    }
    [ClientCallback]
    void TransmitPosition()
    {
        CmdProvidePostionToServer(mytransf.position);
    }
}

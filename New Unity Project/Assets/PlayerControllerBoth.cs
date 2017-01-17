using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;

public class PlayerControllerBoth : NetworkBehaviour
{

    public bool isVive = false;
    public bool isCardboard = false;
    public List<GameObject> EnableForVive;
    public List<GameObject> EnableForCardboard;
    private RemoveShitFromCardboard r;
    // Use this for initialization

    void Start()
    {
        r = new RemoveShitFromCardboard();
        if (NetworkManagerMain.curPlayer == 1)
        {
            isVive = true;
            isCardboard = false;
        }
        else
        {
            isVive = false;
            isCardboard = true;
        }

        if (isVive)
        {
            //WORKING
            Destroy(this.GetComponent<Camera>());
            foreach (GameObject item in EnableForCardboard)
            {
                Destroy(item);
            }

            if (isLocalPlayer)
            {
                //WORKING
                this.GetComponent<SteamVR_ControllerManager>().enabled = true;
                this.GetComponent<SteamVR_PlayArea>().enabled = true;
                this.GetComponent<MeshRenderer>().enabled = true;

                foreach (GameObject item in EnableForVive)
                {
                    item.SetActive(true);
                }
            }

        }
        //now in cardboard
        else
        {
            //WORKING
            Destroy(this.GetComponent<SteamVR_ControllerManager>());
            Destroy(this.GetComponent<SteamVR_PlayArea>());
            Destroy(this.GetComponent<MeshRenderer>());
            //r.RemoveMyShit(GetComponents<GameObject>());

            if (!isLocalPlayer)
            {
                foreach (GameObject item in EnableForVive)
                {
                    item.SetActive(true);
                    try
                    {
                        if (item.name == "Camera (head)")
                        {
                            Destroy(item);
                        }
                    }
                    catch (System.Exception)
                    {
                        print("No Camera found for " + item.name);
                    }
                }
                try
                {
                    EnableForCardboard[0].SetActive(true);
                }
                catch (System.Exception)
                {
                    print("Not FOUND");
                }
                
            }
            else if (isLocalPlayer)
            {
                GameObject tempEye = GameObject.Find("Camera (eye)");
                Destroy(tempEye.GetComponent<GvrHead>());
                Destroy(tempEye.GetComponent<SteamVR_Camera>());
                Destroy(tempEye.GetComponent<FlareLayer>());
                Destroy(tempEye.GetComponent<Camera>());
                Destroy(GameObject.Find("Camera (ears)"));

                foreach (GameObject item in EnableForVive)
                {
                    Destroy(item);
                }
               
                this.GetComponent<Camera>().enabled = true;
                foreach (GameObject item in EnableForCardboard)
                {
                    item.SetActive(true);
                }
            }
        }

    }

    public override void OnStartClient()
    {
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (item.activeInHierarchy)
            {
                print("Player is active at position:  " + item.transform.position);
            }
        }
        base.OnStartClient();

    }
    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
    }
}

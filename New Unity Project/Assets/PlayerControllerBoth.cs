using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;

public class PlayerControllerBoth : NetworkBehaviour
{

    public bool isVive = false;
    public bool isCardboard = false;
    public List<GameObject> EnableForVive;
    public List<GameObject> EnableForCardboard;
    // Use this for initialization

    void Start()
    {

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
            Destroy(this.GetComponent<GvrHead>());
            Destroy(this.GetComponent<StereoController>());
            Destroy(this.GetComponent<UnityEngine.EventSystems.PhysicsRaycaster>());
            Destroy(this.GetComponent<GUILayer>());
            Destroy(this.GetComponent<FlareLayer>());
            Destroy(this.GetComponent<Camera>());
            foreach (GameObject item in EnableForCardboard)
            {
                Destroy(item);
            }

            if (isLocalPlayer)
            {
                //NOT WORKING
                this.GetComponent<SteamVR_ControllerManager>().enabled = true;
                //this.GetComponent<MeshRenderer>().enabled = true;
                this.GetComponent<SteamVR_PlayArea>().enabled = true;
                foreach (GameObject item in EnableForVive)
                {
                    item.SetActive(true);
                }
            }

        }
        else
        {
            //WORKING
            Destroy(this.GetComponent<SteamVR_ControllerManager>());
            Destroy(this.GetComponent<MeshRenderer>());
            Destroy(this.GetComponent<SteamVR_PlayArea>());
            foreach (GameObject item in EnableForVive)
            {
                Destroy(item);
            }

            if (isLocalPlayer)
            {
                //NOT WORKING
                this.GetComponent<GvrHead>().enabled = true;
                this.GetComponent<StereoController>().enabled = true;
                this.GetComponent<UnityEngine.EventSystems.PhysicsRaycaster>().enabled = true;
                this.GetComponent<GUILayer>().enabled = true;
                this.GetComponent<FlareLayer>().enabled = true;
                this.GetComponent<Camera>().enabled = true;
                GameObject.Find("Camera (eye)").SetActive(false);
                GameObject.Find("ViveModel").SetActive(true);
                foreach (GameObject item in EnableForCardboard)
                {
                    item.SetActive(true);
                }
            }
            else
            {

                foreach (GameObject item in this.GetComponents<GameObject>())
                {
                    if (item.name == "CardboardModel")
                    {
                        item.SetActive(true);
                    }
                }
                    //.Find("CardboardModel").SetActive(true);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        //if (isLocalPlayer && isVive)
        //{
        //    foreach (GameObject item in EnableForCardboard)
        //    {
        //        item.SetActive(false);
        //    }
        //    //GameObject.Find("Main Camera Left").SetActive(true);
        //    //GameObject.Find("Main Camera Right").SetActive(true);
        //}
    }
}

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
            Destroy(this.GetComponent<Camera>());
            foreach (GameObject item in EnableForCardboard)
            {
                Destroy(item);
            }

            if (isLocalPlayer)
            {
                //NOT WORKING
                this.GetComponent<SteamVR_ControllerManager>().enabled = true;                
                this.GetComponent<SteamVR_PlayArea>().enabled = true;
                this.GetComponent<MeshRenderer>().enabled = true;

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
            Destroy(this.GetComponent<SteamVR_PlayArea>());
            Destroy(this.GetComponent<MeshRenderer>());

            foreach (GameObject item in EnableForVive)
            {
                Destroy(item);
            }

            if (isLocalPlayer)
            {
                //NOT WORKING
                this.GetComponent<Camera>().enabled = true;
                //GameObject.Find("Camera (eye)").SetActive(false);
                //GameObject.Find("ViveModel").SetActive(true);
                foreach (GameObject item in EnableForCardboard)
                {
                    item.SetActive(true);
                }
                EnableForVive[3].SetActive(true);
            }
            else
            {
                try
                {
                    EnableForCardboard[0].SetActive(true);
                    
                }
                catch (System.Exception)
                {

                    print("Not FOUND");
                }
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

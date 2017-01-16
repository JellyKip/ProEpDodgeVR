using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections.Generic;

public class NetworkManagerMain : NetworkManager
{
    string ipAddress = "192.168.178.81";
    private bool isVive = false;
    private bool isCardboard = false;
    public List<Transform> spawnPositions = new List<Transform>();
    public static int curPlayer = 0;


    //Called on the server when a new client connects.
    public override void OnServerConnect(NetworkConnection conn)
    {

    }

    // Called on Server when a new player is added for a client
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        
        //GameObject player = Instantiate(playerPrefab);
        //DeterminePlayerStatus(player);
        var player = (GameObject)GameObject.Instantiate(playerPrefab, spawnPositions[curPlayer].position, Quaternion.identity);
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        curPlayer++;
        
        print("---OnServerAddPlayer---" + curPlayer);
        //Destroy(player);
    }

    private void Start()
    {
        spawnPositions.Add(GameObject.FindGameObjectsWithTag("ViveSpawn")[0].transform);
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Spawn"))
        {
            spawnPositions.Add(obj.transform);
        }

    }

    public void StartupHost()
    {
        SetPort();
        print("---StartupHost---");
        print(ipAddress);
        NetworkManager.singleton.StartHost();

    }
    public void LanServer()
    {
        //SetPort();
        print("---StartupLan---");
        //print(ipAddress);
        NetworkManager.singleton.StartServer();
    }
    public void StoppingHost()
    {
        NetworkManager.singleton.StopHost();
    }

    public void JoinGame()
    {
        SetIPAddress();
        SetPort();
        NetworkManager.singleton.StartClient();
        //NetworkManager.singleton.ServerChangeScene("MainScene");
    }

    void SetIPAddress()
    {
        NetworkManager.singleton.networkAddress = ipAddress;
    }

    void SetPort()
    {
        NetworkManager.singleton.networkPort = 7777;

    }

    //private void OnLevelWasLoaded(int level)
    //{
    //    if (level == 0)
    //    {
    //        SetupMenuSceneButtons();
    //    }
    //    else
    //    {
    //        SetupSecondscreen();
    //    }
    //}



    void SetupSecondscreen()
    {
        GameObject.Find("Disconnect").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Disconnect").GetComponent<Button>().onClick.AddListener(StoppingHost);
    }
    void DeterminePlayerStatus(GameObject player)
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
            Destroy(player.GetComponent<GvrHead>());
            Destroy(player.GetComponent<StereoController>());
            Destroy(player.GetComponent<UnityEngine.EventSystems.PhysicsRaycaster>());
            Destroy(player.GetComponent<GUILayer>());
            Destroy(player.GetComponent<FlareLayer>());
            Destroy(player.GetComponent<Camera>());
            foreach (GameObject item in player.GetComponentsInChildren<GameObject>())
            {
                switch (item.name)
                {
                    case "Main Camera Left":
                        Destroy(item);
                        break;
                    case "Main Camera Right":
                        Destroy(item);
                        break;
                    case "CardboardModel":
                        Destroy(item);
                        break;
                }
            }

            //if (isLocalPlayer)
            //{
            //    //NOT WORKING
            //    this.GetComponent<SteamVR_ControllerManager>().enabled = true;
            //    //this.GetComponent<MeshRenderer>().enabled = true;
            //    this.GetComponent<SteamVR_PlayArea>().enabled = true;
            //    //foreach (GameObject item in EnableForVive)
            //    //{
            //    //    item.SetActive(true);
            //    //}
            //}

        }
        else
        {
            //WORKING
            Destroy(player.GetComponent<SteamVR_ControllerManager>());
            Destroy(player.GetComponent<MeshRenderer>());
            Destroy(player.GetComponent<SteamVR_PlayArea>());
            foreach (GameObject item in player.GetComponentsInChildren<GameObject>())
            {
                switch (item.name)
                {
                    case "Controller (left)":
                        Destroy(item);
                        break;
                    case "Controller (right)":
                        Destroy(item);
                        break;
                    case "Camera (head)":
                        Destroy(item);
                        break;
                }
            }

            //if (isLocalPlayer)
            //{
            //    //NOT WORKING
            //    this.GetComponent<GvrHead>().enabled = true;
            //    this.GetComponent<StereoController>().enabled = true;
            //    this.GetComponent<UnityEngine.EventSystems.PhysicsRaycaster>().enabled = true;
            //    this.GetComponent<GUILayer>().enabled = true;
            //    this.GetComponent<FlareLayer>().enabled = true;
            //    this.GetComponent<Camera>().enabled = true;
            //    GameObject.Find("Camera (eye)").SetActive(false);
            //    GameObject.Find("ViveModel").SetActive(true);
            //    foreach (GameObject item in EnableForCardboard)
            //    {
            //        item.SetActive(true);
            //    }
            //}
            //else
            //{

            //    foreach (GameObject item in this.GetComponents<GameObject>())
            //    {
            //        if (item.name == "CardboardModel")
            //        {
            //            item.SetActive(true);
            //        }
            //    }
            //    //.Find("CardboardModel").SetActive(true);
            //}

        }
    }
}




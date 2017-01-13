using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections.Generic;

public class NetworkManager_Custom : NetworkManager
{
    string ipAddress = "192.168.178.81";

    public List<Transform> spawnPositions = new List<Transform>();
    public int curPlayer = 0;

    public override void OnServerConnect(NetworkConnection conn)
    {
        
        int tempPrefab = curPlayer;

        Debug.Log("Player connected: " + curPlayer.ToString());


        if (curPlayer >= 1)
        {
            tempPrefab = 1;
            var player = Instantiate(playerPrefab, spawnPositions[curPlayer].position, Quaternion.identity) as GameObject;

            

            NetworkServer.Spawn(player);
            //NetworkServer.AddPlayerForConnection(conn, player, 0);
            

            try
            {
                RemoveViewer();
            }
            catch (System.Exception)
            {

            }
           // RemoveHeadScript();
        }
        else
        {
            //var player = GameObject.Find("Vive");
            //player = Instantiate(playerPrefab, spawnPositions[curPlayer].position, Quaternion.identity) as GameObject;
            //NetworkServer.Spawn(player);
        }

        //NetworkConnection stuff = new NetworkConnection();


        //Select the prefab from the spawnable objects list
        // var playerPrefab = spawnPrefabs[tempPrefab];
        curPlayer++;

        //var player = new GameObject();
        //player = Instantiate(playerPrefab, spawnPosition.position + new Vector3(2, 2, 2), Quaternion.identity) as GameObject;
        //NetworkServer.Spawn(player);
    }


    //// Server
    //public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
    //{
    //    Debug.Log("Does this work?");
    //    if (curPlayer > 1)
    //    {
    //        curPlayer = 1;
    //    }

    //    //Select the prefab from the spawnable objects list
    //    var playerPrefab = spawnPrefabs[curPlayer];
    //    curPlayer++;

    //    // Create player object with prefab
    //    var player = Instantiate(playerPrefab, spawnPosition.position, Quaternion.identity) as GameObject;

    //    // Add player object for connection
    //    NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    //}

    private void Start()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Spawn"))
        {
            spawnPositions.Add(obj.transform);
        }
        
    }
    //private void Update()
    //{
    //    //GameObject.Find("Vive").SetActive(true);
    //}


    public void StartupHost()
    {
        SetPort();
        //int tempPrefab = curPlayer;
        print("Connectinggggg");
        print(ipAddress);
        NetworkManager.singleton.StartHost();

    }
    public void LanServer()
    {
        NetworkManager.singleton.StartServer();
    }
    public void RemoveViewer()
    {
        GameObject.Find("GvrViewer").SetActive(false);
        
    }
    public void RemoveHeadScript()
    {
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Player"))
        {
            print("Head found");
            try
            {
                item.GetComponent<GvrHead>().enabled = false;

            }
            catch (System.Exception)
            {
            }
        }
    }
    public void StopingHost()
    {
        NetworkManager.singleton.StopHost();
    }

    public void JoinGame()
    {
        SetIPAddress();
        SetPort();
        NetworkManager.singleton.StartClient();
        NetworkManager.singleton.ServerChangeScene("MainScene");
    }

    void SetIPAddress()
    {
        NetworkManager.singleton.networkAddress = ipAddress;
    }

    void SetPort()
    {
        NetworkManager.singleton.networkPort = 7777;

    }

    private void OnLevelWasLoaded(int level)
    {
        if (level == 0)
        {
            SetupMenuSceneButtons();
        }
        else
        {
            SetupSecondscreen();
        }
    }

    void SetupMenuSceneButtons()
    {
        GameObject.Find("Play").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Play").GetComponent<Button>().onClick.AddListener(StartupHost);

        GameObject.Find("Connect").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Connect").GetComponent<Button>().onClick.AddListener(JoinGame);
    }


    void SetupSecondscreen()
    {
        GameObject.Find("Disconnect").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Disconnect").GetComponent<Button>().onClick.AddListener(NetworkManager.singleton.StopHost);
    }



}

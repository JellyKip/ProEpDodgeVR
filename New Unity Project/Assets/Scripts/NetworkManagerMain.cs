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


    void SetupSecondscreen()
    {
        GameObject.Find("Disconnect").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Disconnect").GetComponent<Button>().onClick.AddListener(StoppingHost);
    }
}




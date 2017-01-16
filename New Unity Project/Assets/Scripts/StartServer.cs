using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartServer : NetworkBehaviour {

    string HostName = "127.0.0.1";

    // Use this for initialization
    private void Start()
    {
    }
    public void StartHost () {
      //  NetworkManager.singleton.serverBindToIP = true;
      //  NetworkManager.singleton.serverBindAddress = Network.player.ipAddress;
        NetworkManager.singleton.networkPort = 7777;
        NetworkManager.singleton.maxConnections = 5;
        NetworkManager.singleton.StartHost();
    }
	
	public void JoinWorld () {
        NetworkManager.singleton.networkAddress = HostName;
        NetworkManager.singleton.networkPort = 7777;
        NetworkManager.singleton.StartClient();
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class StartServer : MonoBehaviour {

    string HostName = "198.168.178.1";
    

    // Use this for initialization
    public void StartHost () {
        NetworkManager.singleton.networkPort = 7777;
        NetworkManager.singleton.maxConnections = 4;
        NetworkManager.singleton.StartHost();
    }
	
	// Update is called once per frame
	public void JoinWorld () {
        NetworkManager.singleton.networkAddress = HostName;
        NetworkManager.singleton.networkPort = 7777;
        NetworkManager.singleton.StartClient();
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartServer : NetworkBehaviour {

    string HostName = "127.0.0.1";
    public Button b1;
    public Button b2;

    // Use this for initialization
    private void Start()
    {
        b1.GetComponent<Button>();
        b2.GetComponent<Button>();
    }
    public void StartHost () {
      //  NetworkManager.singleton.serverBindToIP = true;
      //  NetworkManager.singleton.serverBindAddress = Network.player.ipAddress;
        NetworkManager.singleton.networkPort = 7777;
        NetworkManager.singleton.maxConnections = 4;
        NetworkManager.singleton.StartHost();
        b1.gameObject.SetActive(false);
            
      
    }
	
	public void JoinWorld () {
        NetworkManager.singleton.networkAddress = HostName;
        NetworkManager.singleton.networkPort = 7777;
        NetworkManager.singleton.StartClient();
        b2.gameObject.SetActive(false);

    }
}

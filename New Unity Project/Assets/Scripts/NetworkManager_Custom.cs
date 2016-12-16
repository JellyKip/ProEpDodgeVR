using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkManager_Custom : NetworkManager {
    string ipAddress = "127.0.0.1";
    public void StartupHost()
    {
        SetPort();
        
        NetworkManager.singleton.StartHost();
    }

    public void JoinGame()
    {
        SetIPAddress();
        SetPort();
        NetworkManager.singleton.StartClient();
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
        if(level == 0)
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

        GameObject.Find("Join").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Join").GetComponent<Button>().onClick.AddListener(JoinGame);
    }


    void SetupSecondscreen()
    {
        GameObject.Find("Disconnect").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Disconnect").GetComponent<Button>().onClick.AddListener(NetworkManager.singleton.StopHost);
    }
  
  

}

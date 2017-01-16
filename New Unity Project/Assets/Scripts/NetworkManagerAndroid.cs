using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections.Generic;

public class NetworkManagerAndroid : NetworkManager
{
    string ipAddress = "192.168.178.81";

    public List<Transform> spawnPositions = new List<Transform>();
    public int curPlayer = 0;

    private void Start()
    {

    }

    public void RemoveViewer()
    {
        GameObject.Find("GvrViewer").SetActive(false);

    }
    public void RemoveHeadScript()
    {
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Player"))
        {

#if UNITY_EDITOR
            try
            {
                print("---RemoveHeadScript---");
                item.GetComponent<GvrHead>().trackRotation = false;

            }
            catch (System.Exception)
            {
                print("---RemoveHeadScript $$ caught Exception---");
            }
#endif
        }
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

}

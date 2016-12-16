using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class NetworkController : NetworkManager
{

    public Transform spawnPosition;
    public int curPlayer;

    // Server
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
    {

        //Select the prefab from the spawnable objects list
        var playerPrefab = spawnPrefabs[curPlayer];

        // Create player object with prefab
        var player = Instantiate(playerPrefab, spawnPosition.position, Quaternion.identity) as GameObject;

        // Add player object for connection
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }
}
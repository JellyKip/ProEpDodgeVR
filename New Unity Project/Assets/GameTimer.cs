using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameTimer : NetworkBehaviour {

    [SyncVar]
    private float gameTime = 130;

    [SyncVar]
    public bool masterTimer = false; //Is this the master timer?
                                     //public ServerTimer timerObj;

    GameTimer serverTimer;
    public Text Ttime;

    private float minutes;
    private float seconds;
   // ColorExplosion xs = new ColorExplosion();
    [Command]
    public void CmdEndgame(int level)
    {
       // NetworkManager.singleton.ServerChangeScene(level);
    }

    void Start()
    {
        if (isServer)
        { // For the host to do: use the timer and control the time.
            if (isLocalPlayer)
            {
                serverTimer = this;
                masterTimer = true;
            }
        }
        else if (isLocalPlayer)
        { //For all the boring old clients to do: get the host's timer.
            GameTimer[] timers = FindObjectsOfType<GameTimer>();
            for (int i = 0; i < timers.Length; i++)
            {
                if (timers[i].masterTimer)
                {
                    serverTimer = timers[i];
                    
                }
            }
        }
    }
    void Update()
    {
        minutes = Mathf.Floor(gameTime / 60);
        seconds = Mathf.Floor(gameTime % 60);

        if (masterTimer)
        { //Only the MASTER timer controls the time
            //if(gameTime==0)
           if (minutes==0&&seconds==0)
            {
                NetworkManager.singleton.StopHost();
                Debug.Log("done");

            }
            else
            {
                gameTime -= Time.deltaTime;
             
            }
        }

        if (isLocalPlayer)
        { //EVERYBODY updates their own time accordingly.
            if (serverTimer)
            {
                gameTime = serverTimer.gameTime;
            }
            else
            { //Maybe we don't have it yet?
                GameTimer[] timers = FindObjectsOfType<GameTimer>();
                for (int i = 0; i < timers.Length; i++)
                {
                    if (timers[i].masterTimer)
                    {
                        serverTimer = timers[i];
                    }
                }
            }
        }
        Ttime.text = string.Format("{0:0}:{1:00}", minutes, seconds);
        
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;
using UnityEngine.SceneManagement;
public class Player : NetworkBehaviour
{
    public static Player localPlayer;
    [SyncVar] public string matchID;
    private NetworkMatch networkMatch;

    private void Start() 
    {
        networkMatch = GetComponent<NetworkMatch>();

        if (isLocalPlayer)
        {
            localPlayer = this;
        }
        else
        {
            MatchMaking.instance.SpawnPlayerUIPPrefub(this);
        }
    }
   
    #region HOST
    public void HostGame()
    {
        string ID = MatchMaking.GetRandomID();
        CmdHostGame(ID);
    }
    [Command]
    public void CmdHostGame(string ID)
    {
        matchID = ID;
        if ( MatchMaking.instance.HostGame(ID, gameObject))
        {
            Debug.Log("Lobbi ID: "+ matchID + " - The lobby has been successfully created" );
            networkMatch.matchId = ID.ToGuid();
            TargetHostGame(true, ID);
        }
        else
        {
            Debug.Log("Lobbi ID: "+ matchID + " - The lobby has NOT been created" );
            TargetHostGame(false, ID);
        }
    }
    [TargetRpc]
    private void TargetHostGame(bool success, string ID)
    {
        matchID = ID;
        MatchMaking.instance.HostSuccsess(success, ID);

    }

    #endregion _________________________________________________________

    #region JOIN

    public void JoinGame(string inputID)
    {
        CmdJoinGame(inputID);
    }

    [Command]
    public void CmdJoinGame(string ID)
    {
        matchID = ID;

        if ( MatchMaking.instance.JoinGame(ID, gameObject))
        {
            Debug.Log("Lobbi ID: "+ matchID + " - The lobby has been successfully created" );
            networkMatch.matchId = ID.ToGuid();
            TargetJoinGame(true, ID);
        }

        else
        {
            Debug.Log("Lobbi ID: "+ matchID + " - The lobby has NOT been created" );
            TargetJoinGame(false, ID);
        }
    }
    [TargetRpc]
    private void TargetJoinGame(bool success, string ID)
    {
        matchID = ID;
        MatchMaking.instance.JoinSuccsess(success, ID);

    }

    #endregion _________________________________________________________

    // local scale player !!
    // SceneManager.LoadScene("Game", LoadSceneMode.Additive); !!
    #region BEGIN

    public void BeginGame()
    {
        CmdBeginGame();
    }

    [Command]
    public void CmdBeginGame()
    {
        MatchMaking.instance.BeginGame(matchID);
        Debug.Log("GAME HES BEGUN !!!");

    }
    [TargetRpc]
    private void TargetBeginGame()
    {
        Debug.Log($"ID {matchID} | BEGUN");
        DontDestroyOnLoad(gameObject);
        MatchMaking.instance.inGame = true;
        transform.localScale = new Vector3(1, 1, 1);
        SceneManager.LoadScene("Game", LoadSceneMode.Additive);
    }
    public void StartGame()
    {
        TargetBeginGame();  
    }
    #endregion _________________________________________________________

    

}

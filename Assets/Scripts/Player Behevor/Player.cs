using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;
public class Player : NetworkBehaviour
{
    public static Player localPlayer;
    [SyncVar] public string matchID;
    private NetworkMatch networkMatch;

    public override void OnStartClient()
    {
        base.OnStartClient();
        
        // This ensures the local player is set as soon as the client starts
        if (isLocalPlayer)
        {
            localPlayer = this;
            Debug.Log("Local player initialized!");
        }
    }

    public override void OnStartAuthority()
    {
        base.OnStartAuthority();
        localPlayer = this;
        Debug.Log("Player authority assigned - player fully initialized!");
    }

    private void Start() 
    {
        networkMatch = GetComponent<NetworkMatch>();

        if (!isLocalPlayer)
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
        SceneManager.LoadScene(2, LoadSceneMode.Additive);
    }
    public void StartGame()
    {
        TargetBeginGame();  
    }
    #endregion _________________________________________________________

    public bool IsNetworkReady()
    {
        return isLocalPlayer && NetworkClient.isConnected;
    }

}

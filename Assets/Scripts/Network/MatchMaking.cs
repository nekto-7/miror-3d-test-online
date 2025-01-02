using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using System;
using System.Security.Cryptography;
using System.Text; 
using TMPro;

[System.Serializable] // save data match
public class Match : NetworkBehaviour 
{
    public string ID;
    public readonly List<GameObject> players = new List<GameObject>();

    public Match(string ID, GameObject player)
    {
        this.ID = ID;
        players.Add(player);
    }
}


public static class MatchException
{
    public static Guid ToGuid(this string id)
    {
        using (MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider())
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(id);
            byte[] hashBytes = provider.ComputeHash(inputBytes);
            
            return new Guid(hashBytes);
        }
    }
}

public class MatchMaking : NetworkBehaviour
{
    // :(
    // Before entering the lobby
    public static MatchMaking instance;
    public readonly SyncList<Match> matches= new SyncList<Match>();
    public readonly SyncList<string> matchIDs = new SyncList<string>();
    public InputField joinInput;
    public Button hostButton;
    public Button joinButton;
    public Canvas LobbiCanvas;
    // UIP
    // After
    public Transform UIPLayerParnet;    
    public GameObject UIPlayerPrefub;
    public TextMeshProUGUI IDText;
    public Button BeginGameButton;
    public GameObject TurnManager;
    public bool inGame;

    private void Start() 
    {
        instance = this;
    }
 
    private void Update()
    {
        if (!inGame)
        {
            Player[] players = FindObjectsOfType<Player>();
            for (int i = 0; i< players.Length; i++ )
            {
                players[i].gameObject.transform.localScale = Vector3.zero;
            }   
        }
    }

    #region  HOST
        
    public void Host()
    {
        if (Player.localPlayer == null || !Player.localPlayer.IsNetworkReady())
        {
            Debug.LogError("Игрок не готов к сетевому взаимодействию! Убедитесь, что вы подключены к серверу и имеете необходимые права.");
            return;
        }
        
        joinInput.interactable = false;
        hostButton.interactable = false;        
        joinButton.interactable = false;

        Player.localPlayer.HostGame();
    }

    public void HostSuccsess(bool success, string mutchID)
    {
        if(success)
        {
            LobbiCanvas.enabled = true;

            SpawnPlayerUIPPrefub(Player.localPlayer);
            IDText.text = mutchID;
            BeginGameButton.interactable = true;
        }
        else
        {
            joinInput.interactable = true;
            hostButton.interactable = true;        
            joinButton.interactable = true;
        }
    }
    public bool HostGame(string matchID, GameObject player)
    {
        if(!matchIDs.Contains(matchID))
        {
            matchIDs.Add(matchID);
            matches.Add(new Match(matchID, player));
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion _________________________________________________________
    
    #region  JOIN
    public void Join()
    {
        if (Player.localPlayer == null)
        {
            Debug.LogError("Local player not initialized!");
            return;
        }
        
        joinInput.interactable = false;
        hostButton.interactable = false;        
        joinButton.interactable = false;

        Player.localPlayer.JoinGame(joinInput.text.ToUpper());
    }
    public void JoinSuccsess(bool success, string mutchID)
    {
        if(success)
        {
            LobbiCanvas.enabled = true;

            SpawnPlayerUIPPrefub(Player.localPlayer);
            IDText.text = mutchID;
            BeginGameButton.interactable = false;
        }
        else
        {
            joinInput.interactable = true;
            hostButton.interactable = true;        
            joinButton.interactable = true;
        }
    }

    public bool JoinGame(string matchID, GameObject player)
    {
        if(!matchIDs.Contains(matchID))
        {
            for(int i = 0 ; i < matches.Count; i++)
            {
                if(matches[i].ID == matchID)
                {
                    matches[i].players.Add(player);
                    break;
                }
            }
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion _________________________________________________________
    
    // Generate ID

    public static string GetRandomID()
    {
        string ID = string.Empty;
        for(int i = 0; i < 5; i++)
        {
            int randInt = UnityEngine.Random.Range(0,99);
            if (randInt < 45)
            {
                ID += (char)(randInt + 1);
            }
            else
            {
                ID += (randInt - 22).ToString();
            }
        }
        return ID;
    }

    public void SpawnPlayerUIPPrefub(Player player)
    {
        GameObject newPlayer = Instantiate(UIPlayerPrefub, UIPLayerParnet);
        newPlayer.GetComponent<PlayerID>().SetPlayer(player);
    }

    public void StartGame()
    {
        Player.localPlayer.BeginGame();        
    }

    public void BeginGame(string matchID)
    {
        GameObject newTurnManager = Instantiate(TurnManager);
        NetworkServer.Spawn(newTurnManager);
        newTurnManager.GetComponent<NetworkMatch>().matchId = matchID.ToGuid();
        TurnManager turnManager = newTurnManager.GetComponent<TurnManager>();
        for (int i = 0; i < matches.Count; i++)
        {
            if(matches[i].ID == matchID)
            {
                foreach (var player in matches[i].players)
                {
                    Player player1 = player.GetComponent<Player>();
                    turnManager.AddPlayer(player1);
                    player1.StartGame();
                }
                break;
            }
        }
    }
    public void CopyText()
    { 
        string textToCopy = IDText.text;  
        GUIUtility.systemCopyBuffer = textToCopy; 
    }
}

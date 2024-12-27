using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerID : MonoBehaviour
{
     public TextMeshProUGUI nameText;
    private Player player;

    public void SetPlayer(Player player)
    {
        this.player = player;    
        nameText.text = "name"; // plaer.name;
    }
}

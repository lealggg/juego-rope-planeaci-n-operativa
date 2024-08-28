using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class PlayerName : MonoBehaviourPunCallbacks 
{
    public TMP_Text playerName;

    [PunRPC]
    public void SetNameText(string name)
    {
        playerName.text = name; 
    }
}

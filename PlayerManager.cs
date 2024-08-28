using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;

    void Start()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            if (playerPrefab != null)
            {
                PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);
            }
            else
            {
                Debug.LogError("Player prefab no asignado en PlayerManager.");
            }
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public PhotonView playerPrefab;
    //public Transform spawnPoint;
    public Transform[] spawnPoints;

    void Start()
    {
        PhotonNetwork.JoinOrCreateRoom("Room1", new RoomOptions { MaxPlayers = 10 }, TypedLobby.Default);
       
    }

   /* public override void OnJoinedLobby()
    {
        PhotonNetwork.JoinOrCreateRoom("Room1", new RoomOptions { MaxPlayers = 5 }, TypedLobby.Default);
    }
   */
    public override void OnJoinedRoom()
    {
        int spawnIndex = PhotonNetwork.LocalPlayer.ActorNumber - 1;

        if (spawnIndex < spawnPoints.Length)
        {
            GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, spawnPoints[spawnIndex].position, Quaternion.identity);
            
            player.GetComponent<PhotonView>().RPC("SetNameText", RpcTarget.AllBuffered, PlayerPrefs.GetString("PlayerName"));
        }
        //GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.position, spawnPoint.rotation);
        //GameObject player = PhotonNetwork.Instantiate("PlayerPrefabName", spawnPoints[spawnIndex].position, Quaternion.identity);
    }

    
}

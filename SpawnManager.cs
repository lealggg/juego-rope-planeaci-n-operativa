using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class SpawnManager : MonoBehaviourPunCallbacks
{
    public Transform[] spawnPoints; // Asigna tus puntos de spawn en el inspector

    void Start()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            SpawnPlayer();
        }
    }

    void SpawnPlayer()
    {
        // Determina el índice de spawn basado en el ActorNumber del jugador
        int spawnIndex = PhotonNetwork.LocalPlayer.ActorNumber - 1;

        if (spawnIndex < spawnPoints.Length)
        {
            PhotonNetwork.Instantiate("PlayerPrefabName", spawnPoints[spawnIndex].position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("No hay suficientes puntos de spawn configurados.");
        }
    }
}
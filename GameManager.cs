using LitJson;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    public Transform[] spawnPoints;
    public Button distributeButton;
    public Button restartButton; // Botón de reinicio
    public CardLibrary library;
    public Deck deck;

    void Start()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            int spawnIndex = PhotonNetwork.LocalPlayer.ActorNumber - 1;
            PhotonNetwork.Instantiate(playerPrefab.name, spawnPoints[spawnIndex].position, Quaternion.identity);
        }

        distributeButton.onClick.AddListener(OnDistributeCardsButtonClicked);

        if (PhotonNetwork.IsMasterClient)
        {
            restartButton.gameObject.SetActive(true); // Mostrar el botón solo al Master
            restartButton.onClick.AddListener(OnRestartButtonClicked);
        }
        //else
        //{
        //    restartButton.gameObject.SetActive(false); // Ocultar el botón para los demás jugadores
        //}
    }

    [PunRPC]
    public void DistributeCards()
    {
        int numberOfPlayers = PhotonNetwork.CurrentRoom.PlayerCount;
        if (numberOfPlayers < 2)
        {
            Debug.LogError("No hay suficientes jugadores para repartir las cartas.");
            return;
        }

        deck.ShuffleDeck(library.cards);
        List<CardData>[] playerHands = deck.DealCards(numberOfPlayers, library.cards);

        for (int i = 0; i < numberOfPlayers; i++)
        {
            Player player = PhotonNetwork.PlayerList[i].TagObject as Player;
            if (player != null)
            {
                CardData[] playerCards = playerHands[i].ToArray();
                string[] cardNames = Array.ConvertAll(playerCards, (e) => e.cardName);
                string json = JsonMapper.ToJson(cardNames);
                Debug.LogFormat("Sending playerCards {0}: {1}", i, json);
                player.photonView.RPC("ReceiveCards", player.photonView.Owner, json);
            }
            else
            {
                Debug.LogError($"No se encontró PhotonView para el jugador {i + 1}");
            }
        }
    }

    public void OnDistributeCardsButtonClicked()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("DistributeCards", RpcTarget.All);
        }
    }

    public void OnRestartButtonClicked()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("RestartGame", RpcTarget.All); // Llamada RPC para reiniciar el juego
        }
    }

    [PunRPC]
    public void RestartGame()
    {
        // Reiniciar el estado del juego sin recargar la escena
        ResetGameState();

        // Volver a distribuir las cartas
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("DistributeCards", RpcTarget.All);
        }

        Debug.Log("El juego ha sido reiniciado por el Master.");
    }

    void ResetGameState()
    {
        // Reinicia las variables necesarias, como las puntuaciones, manos de los jugadores, etc.
        // Puedes añadir aquí cualquier lógica específica que necesites para reiniciar el juego.

        // Ejemplo: Resetear la baraja y las manos de los jugadores
        //deck.ResetDeck();
        foreach (Player player in FindObjectsOfType<Player>())
        {
            player.ResetHand(); // Suponiendo que tienes un método para resetear la mano del jugador
        }
    }
}







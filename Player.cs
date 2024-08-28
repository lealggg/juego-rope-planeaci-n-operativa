using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using LitJson;
using System;
using UnityEngine.XR;

public class Player : MonoBehaviourPun
{ 
    public List<CardData> mano;
    public List<CardGO> instances;
    public PlayerHandUI handUI;
    public List<GameObject> listCards;
    public CardLibrary library;

    private void Awake()
    {
        library.Initialize();
    }

    void Start()
    {
        handUI = FindObjectOfType<PlayerHandUI>();

        if (photonView.IsMine)
        {
            PhotonNetwork.LocalPlayer.TagObject = this;
            

        }
        // Mensaje de depuración para verificar que handUI está asignado
        if (handUI == null)
        {
            Debug.LogError("handUI no está asignado en el prefab del jugador.");
        }
        else
        {
            Debug.Log("handUI está asignado correctamente.");
        }
    }

    [PunRPC]
    public void ReceiveCards(string cardsJson)
    {
        Debug.Log("ReceiveCards called");
        Debug.Log(cardsJson);

        string[] cardNames = JsonMapper.ToObject<string[]>(cardsJson);
        CardData[] cards = Array.ConvertAll(cardNames, library.GetCard);

        // Añadir las cartas a la mano del jugador
        //mano.AddRange(cards);
        handUI.InstantiateCards(cards);
    
        //UpdateHandUI();
    }

    public void ResetHand()
    {
        mano.Clear();
    }



}



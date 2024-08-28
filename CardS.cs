using UnityEngine;
using Photon.Pun;

public class CardS : MonoBehaviourPun, IPunObservable
{
    private Vector3 targetPosition;
    private bool isDragging = false;

    void Start()
    {
        // Iniciar la posición de destino como la posición actual de la carta
        targetPosition = transform.position;
    }

   // void Awake()
   // {
    //    DontDestroyOnLoad(gameObject);
   // }

    void Update()
    {
        if (photonView.IsMine)
        {
            if (isDragging)
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                targetPosition = new Vector3(mousePosition.x, mousePosition.y, 0);
                photonView.RPC("UpdateCardPosition", RpcTarget.Others, targetPosition);
            }

            transform.position = targetPosition;
        }
    }

    void OnMouseDown()
    {
        if (photonView.IsMine)
        {
            isDragging = true;
        }
    }

    void OnMouseUp()
    {
        if (photonView.IsMine)
        {
            isDragging = false;
        }
    }

    [PunRPC]
    public void UpdateCardPosition(Vector3 newPosition)
    {
        targetPosition = newPosition;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // Enviar la posición de la carta a otros jugadores
            stream.SendNext(transform.position);
        }
        else
        {
            // Recibir la posición de la carta de otros jugadores
            targetPosition = (Vector3)stream.ReceiveNext();
        }
    }
}



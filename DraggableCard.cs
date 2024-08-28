using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;

public class DraggableCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Vector2 originalPosition;
    private PhotonView photonView;


    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        photonView = GetComponent<PhotonView>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Guardar la posición original
        originalPosition = rectTransform.anchoredPosition;

        // Hacer la carta transparente y permitir que se mueva
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (photonView)
        {
            rectTransform.anchoredPosition += eventData.delta;
            photonView.RPC("UpdateCardPosition", RpcTarget.All, rectTransform.anchoredPosition);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (photonView)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;

            // Puedes agregar lógica adicional aquí si es necesario.
        }

    }

    [PunRPC]
    public void UpdateCardPosition(Vector2 newPosition)
    {
        rectTransform.anchoredPosition = newPosition;
    }
}





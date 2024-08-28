using UnityEngine;
using UnityEngine.UI;

public class CardGO : MonoBehaviour
{
    public Image cardImage;
    public Text cardNameText;
    public float cardSize = 10f;

    private CardData data;

    public void UpdateView(CardData data)
    {
        this.data = data;
        cardImage.sprite = data.image;

        RectTransform rectTransform = GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            rectTransform.localScale = new Vector3(cardSize / 45f, cardSize / 25f, 1f);
        }

        // Asegúrate de que la carta tenga el componente DraggableCard
        if (GetComponent<DraggableCard>() == null)
        {
            gameObject.AddComponent<DraggableCard>();
        }

        // Asegúrate de que la carta tenga el componente CanvasGroup
        if (GetComponent<CanvasGroup>() == null)
        {
            gameObject.AddComponent<CanvasGroup>();
        }
    }
}




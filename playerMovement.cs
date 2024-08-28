using UnityEngine;
using System.Collections.Generic;

public class playerMovement : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 startPosition;
    private Vector3 offset;

    void OnMouseDown()
    {
        isDragging = true;
        startPosition = transform.position;
        offset = startPosition - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);
            Vector3 newPos = Camera.main.ScreenToWorldPoint(mousePosition) + offset;
            newPos.z = 0; // Asegúrate de que la carta permanezca en el plano 2D
            transform.position = newPos;
        }
    }

    void OnMouseUp()
    {
        isDragging = false;
        // Puedes agregar lógica adicional aquí para comprobar si la carta se soltó en una zona válida
    }
}

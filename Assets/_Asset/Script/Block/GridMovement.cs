using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridMovement : MonoBehaviour
{
    public float moveDuration = 0.2f;
    public LayerMask obstacleLayer; // Layer chứa quân cờ/vật cản
    public Vector2 gridSize = Vector2.one; // Kích thước 1 ô
    private bool isMoving = false;

    void Start()
    {
        EventDispatcher<GameObject>.AddListener(Event.MoveBlock.ToString(), TryMoveUp);
    }
    private void OnDisable()
    {
        EventDispatcher<GameObject>.RemoveListener(Event.MoveBlock.ToString(), TryMoveUp);
    }
    
    public void TryMoveUp(GameObject obj)
    {
        if ((obj != this.gameObject)) return;
        if (isMoving) return;

        Vector2 targetPosition = (Vector2)transform.position + new Vector2(0, gridSize.y);

        // Kiểm tra xem có object nào ở ô đó không (ví dụ quân cờ)
        Collider2D hit = Physics2D.OverlapCircle(targetPosition, 0.1f, obstacleLayer);
        if (hit == null)
        {
            StartCoroutine(MoveTo(targetPosition));
        }
        else
        {
            Debug.Log("Không thể di chuyển! Có vật cản ở ô trên.");
        }
    }

    private IEnumerator MoveTo(Vector2 targetPosition)
    {
        isMoving = true;

        Vector2 startPosition = transform.position;
        float elapsed = 0;

        while (elapsed < moveDuration)
        {
            transform.position = Vector2.Lerp(startPosition, targetPosition, elapsed / moveDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        isMoving = false;
    }
}

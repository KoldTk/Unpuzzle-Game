using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { Up, Down, Left, Right}
public class BlockControl : MonoBehaviour
{
    public Direction direction;
    [SerializeField] private float moveSpeed;
    public Rigidbody2D rigidBody;
    private Vector2 moveDirection;
    // Start is called before the first frame update
    void Start()
    {
        EventDispatcher<GameObject>.AddListener(Event.MoveBlock.ToString(), MoveBlock);
        moveDirection = GetMoveDirection(direction);
    }
    private void OnDisable()
    {
        EventDispatcher<GameObject>.RemoveListener(Event.MoveBlock.ToString(), MoveBlock);
    }
    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        rigidBody.bodyType = RigidbodyType2D.Kinematic;
        StopAllCoroutines();
    }
    private void MoveBlock(GameObject receiver)
    {
        if ((receiver != this.gameObject)) return;
        StartCoroutine(BlockMove(moveDirection));
    } 
    private Vector2 GetMoveDirection(Direction dir)
    {
        switch (dir)
        {
            case Direction.Up:
                return Vector2.up;
            case Direction.Down:
                return Vector2.down;
            case Direction.Left:
                return Vector2.left;
            case Direction.Right:
                return Vector2.right;
            default:
                return Vector2.zero;
        }
    }
    private IEnumerator BlockMove(Vector2 moveDirection)
    {
        rigidBody.bodyType = RigidbodyType2D.Dynamic;
        rigidBody.gravityScale = 0;
        while (gameObject.activeInHierarchy)
        {
            rigidBody.MovePosition(rigidBody.position + moveDirection * moveSpeed * Time.deltaTime);
            yield return null;
        }    
    }    
}

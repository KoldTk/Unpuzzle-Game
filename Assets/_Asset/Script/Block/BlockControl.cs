using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Buffers;

public enum Direction { Up, Down, Left, Right}
public class BlockControl : MonoBehaviour
{
    public Direction direction;
    public ObstacleChecker checker;
    [SerializeField] private float moveSpeed;
    private Vector2 moveDirection;

    public static bool blockIsMoving;
    void Start()
    {
        EventDispatcher<GameObject>.AddListener(Event.MoveBlock.ToString(), MoveBlock);
        moveDirection = GetMoveDirection(direction);
    }
    private void OnDisable()
    {
        EventDispatcher<GameObject>.RemoveListener(Event.MoveBlock.ToString(), MoveBlock);
        blockIsMoving = false;
    }
    private void MoveBlock(GameObject receiver)
    {
        if ((receiver != this.gameObject)) return;

        if (!checker.haveObstacle)
        {
            StartCoroutine(BlockMove(moveDirection));
            
        }
        else if (checker.haveObstacle)
        {
            checker.BumpObstacle(moveDirection);
        }
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
        blockIsMoving = true;
        while (!checker.haveObstacle)
        {
            transform.Translate(moveDirection.normalized * moveSpeed * Time.deltaTime);
            yield return null;
        }
        checker.BumpObstacle(moveDirection);
        checker.gameObject.SetActive(false);
        blockIsMoving = false;
    }

}

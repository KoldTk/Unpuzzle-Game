using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleChecker : MonoBehaviour
{
    private float checkRadius = 0.1f;
    public LayerMask nearbyObjectLayer;
    private Transform obstacle;
    public bool haveObstacle;
    [SerializeField] private float bumpDistance;
    [SerializeField] private float bumpDuration;
    private Vector2 obstacleOriginalPos;
    private void Update()
    {
        CheckObstacle();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
    public void CheckObstacle()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, checkRadius, nearbyObjectLayer);
        foreach (var hit in hits)
        {
            if (hit.gameObject != this.gameObject && hit.gameObject != this.transform.parent.gameObject)
            {
                haveObstacle = true;
                obstacle = hit.transform;
                return;
            }
        }
        haveObstacle = false;
    } 
    public void BumpObstacle(Vector2 moveDirection)
    {
        obstacleOriginalPos = obstacle.position;
        Vector2 bumpOffset = moveDirection.normalized * bumpDistance;
        Sequence bumpSequence = DOTween.Sequence();
        bumpSequence.Append(obstacle.DOMove(obstacleOriginalPos + bumpOffset, bumpDuration))
                    .Append(obstacle.DOMove(obstacleOriginalPos, bumpDuration));
    }    
}

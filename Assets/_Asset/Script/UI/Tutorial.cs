using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public RectTransform tutorialPos;
    private Vector2 originalPos;
    private float moveDuration = 0.5f;
    private float moveDistance = 20f;
    // Start is called before the first frame update
    void Start()
    {
        originalPos = tutorialPos.transform.position;
        ImageMove();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gameObject.SetActive(false);
        }
    }
    private void ImageMove()
    {
        Vector2 originalPos = tutorialPos.anchoredPosition;
        tutorialPos.DOAnchorPosX(originalPos.x + moveDistance, moveDuration)
                     .SetLoops(-1, LoopType.Yoyo)
                     .SetEase(Ease.InOutSine);
    }      
}

using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIGameplay : MonoBehaviour
{
    public TextMeshProUGUI scoreNum;
    public Transform moneyImage;
    private int score;
    private float rotationAngle = 30f;
    private float duration = 0.2f;
    private int loopCount = 4;
    
    void Start()
    {
        scoreNum.text = score.ToString();
        EventDispatcher<int>.AddListener(Event.GainScore.ToString(), GainScore);
    }
    private void OnDisable()
    {
        EventDispatcher<int>.RemoveListener(Event.GainScore.ToString(), GainScore);
    }
    void Update()
    {
        if(!BlockControl.blockIsMoving)
        {
            BlockClick();
        }    
    }
    private void BlockClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            int waypointLayer = LayerMask.GetMask("Block");
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Find target
            RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero, Mathf.Infinity, waypointLayer);
            if (hit.collider != null)
            {
                Transform checker = hit.transform.Find("Checker");
                checker.gameObject.SetActive(true);
                EventDispatcher<GameObject>.Dispatch(Event.MoveBlock.ToString(), hit.transform.gameObject);
            }
        }
    }
    private void GainScore(int value)
    {
        score += value;
        scoreNum.text = score.ToString();
        ShakeImage();
    }
    private void ShakeImage()
    {
        moneyImage.DOLocalRotate(new Vector3(0, 0, rotationAngle), duration)
                 .SetLoops(loopCount, LoopType.Yoyo)
                 .SetEase(Ease.InOutSine);
    }    
}

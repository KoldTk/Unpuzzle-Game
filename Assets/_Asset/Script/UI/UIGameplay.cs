using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms.Impl;

public class UIGameplay : MonoBehaviour
{
    public TextMeshProUGUI scoreNum;
    public TextMeshProUGUI turnNum;
    public TextMeshProUGUI levelNum;
    public Transform moneyImage;
    private float rotationAngle = 30f;
    private float duration = 0.2f;
    private int loopCount = 4;
    public Transform blockGroup;
    public GameObject levelClearMenu;
    public GameObject gameOverMenu;
    [SerializeField] private int turnCount;
    
    void Start()
    {
        AudioManager.Instance.PlayMusic("Game Music");
        levelNum.text = $"Level: {GameManager.Instance.levelCount}";
        turnNum.text = $"Moves: {turnCount}";
        scoreNum.text = GameManager.Instance.score.ToString();
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
                AudioManager.Instance.PlaySFX("Click Block");
                ReduceTurn();
            }
        }
    }
    private void GainScore(int value)
    {
        GameManager.Instance.score += value;
        scoreNum.text = GameManager.Instance.score.ToString();
        AudioManager.Instance.PlaySFX("Gain Score");
        ShakeImage();
        StartCoroutine(GameClearCheck());
    }
    private void ShakeImage()
    {
        moneyImage.DOLocalRotate(new Vector3(0, 0, rotationAngle), duration)
                 .SetLoops(loopCount, LoopType.Yoyo)
                 .SetEase(Ease.InOutSine);
    }
    private void ReduceTurn()
    {
        turnCount -= 1;
        turnNum.text = $"Moves: {turnCount}";
    }
    private IEnumerator GameClearCheck()
    {
        yield return new WaitForSeconds(0.1f);
        Debug.Log("Check Result");
        if (blockGroup.childCount <= 0)
        {
            levelClearMenu.SetActive(true);
            GameManager.Instance.levelCount += 1;
            AudioManager.Instance.PlayMusic("Level Clear Music");
        }
        else if (turnCount <= 0 && blockGroup.childCount > 0)
        {
            gameOverMenu.SetActive(true);
            AudioManager.Instance.PlayMusic("Game Over Music");
        }
    }
}

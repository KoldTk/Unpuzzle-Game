using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int levelCount;
    public int score;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        SetupGameData();
    }

    void Update()
    {
        
    }
    private void SetupGameData()
    {
        levelCount = 1;
        score = 0;
    }
    private void GameOver()
    {
        EventDispatcher<bool>.Dispatch(Event.GameOver.ToString(), true);
        AudioManager.Instance.PlaySFX("Game Over");
    }    
}

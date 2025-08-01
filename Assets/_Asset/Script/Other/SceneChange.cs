using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public string nextScene;
    
    public void ChangeScene()
    {
        SceneManager.LoadScene(nextScene);
        AudioManager.Instance.PlaySFX("Change Stage");
    }
    
    public void NextLevel()
    {
        SceneManager.LoadScene($"Stage {GameManager.Instance.levelCount}");
    }    
}

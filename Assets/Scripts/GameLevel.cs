using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameLevel : MonoBehaviour
{
    public GameObject level1;
    public GameObject level2;
    public GameObject level3;
    public static int count;
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
       
    }


    // Update is called once per frame
   public void LevelMessage()
    {
        if(ScoreManager.score>=100 && count==0)
        {
            level1.SetActive(true);
            count = count + 1;
            Time.timeScale = 0f;
        }
        if (ScoreManager.score >=200 && count==1)
        {
            level2.SetActive(true);
            count = count + 1;
            Time.timeScale = 0f;
        }
        if (ScoreManager.score >=4000 && count == 2)
        {
            level3.SetActive(true);
            count = count + 1;
            Time.timeScale = 0f;
        }
       
        
    }

    public void RemovePanel()
    {
        level1.SetActive(false);
        level2.SetActive(false);
        level3.SetActive(false);
        gameManager.Menu();
    }
}

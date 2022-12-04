﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverMenu;
	public GameObject pauseMenu;
   
    public AudioSource clickSound;
    public AudioSource music;

    public GameObject loadingScreen;
	public Slider slider;
    // Start is called before the first frame update
     private void Start()
    {
        //Cursor.visible = true;
        
    }

    private void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * 0.8f);
    }
    public void Play()
	{
        clickSound.Play();       
	StartCoroutine(PlayAsynchronously());
	}
    IEnumerator PlayAsynchronously()
    {
        AsyncOperation operation= SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex+1);
        
        loadingScreen.SetActive(true);
        while(!operation.isDone)
        {
            float progress=Mathf.Clamp01(operation.progress / 0.9f);
            slider.value=progress;            
            yield return null;
        }
    }
    public void PauseMenu()
	{
        music.enabled=false;
        clickSound.Play();
        pauseMenu.SetActive(true);
		Time.timeScale=0f;
    }
    public void Resume()
	{
        Time.timeScale=1f;
        music.enabled=true;;
        clickSound.Play();		
		pauseMenu.SetActive(false);	
	}
public void Restart()
	{
        Time.timeScale=1f;
        clickSound.Play();        
		StartCoroutine(RestartAsynchronously());
      
	}
    IEnumerator RestartAsynchronously()
    {
        AsyncOperation operation= SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        
        loadingScreen.SetActive(true);
        while(!operation.isDone)
        {
            float progress=Mathf.Clamp01(operation.progress / 0.9f);
            slider.value=progress;
            yield return null;
        }
    }
    public void GameOver()
	{ 
       if(PlayerPrefs.GetInt("HighScore")<ScoreManager.score)
		 PlayerPrefs.SetInt("HighScore",ScoreManager.score);       
        music.enabled=false;
        gameOverMenu.SetActive(true); 
	}
	public void Menu()
	{	
        clickSound.Play();	
		Time.timeScale=1f;	
        StartCoroutine(MenuAsynchronously());
	}
    IEnumerator MenuAsynchronously()
    {
        AsyncOperation operation= SceneManager.LoadSceneAsync(0);
        
        loadingScreen.SetActive(true);
        while(!operation.isDone)
        {
            float progress=Mathf.Clamp01(operation.progress / 0.9f);
            slider.value=progress;
            yield return null;
        }
    }
    
    
    public void Quit()
	{
        clickSound.Play();
		Time.timeScale=1f;
		Application.Quit();
	}
    
}

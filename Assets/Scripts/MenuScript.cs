using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuScript : MonoBehaviour {
    
    public static bool gameIsPaused = false;
    [SerializeField] GameObject MenuUi;
    [SerializeField] GameObject nextLevelText;
    private AudioSource backgroundAudio;
    [SerializeField] TMP_Text resultText; 
    private int maxScore;
    private bool calcScore = false;
    private int currentScore;

    void Start() {
        MenuUi.SetActive(false);
        backgroundAudio = GameObject.Find("Main Camera").GetComponent<AudioSource>();
    }

    void Update() {

        if (calcScore) {
            if (currentScore < maxScore) {
                currentScore +=2 ;
                resultText.text = "Score: " + currentScore;
            }
            else {
                calcScore = false;
            }
        }
    }

    public void resume() {

        MenuUi.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        backgroundAudio.mute = false;
    }

   public void pause() {

        if (gameIsPaused) {
            resume();
        }
        else {
            backgroundAudio.mute = true;
            MenuUi.SetActive(true);
            Time.timeScale = 0f;
            gameIsPaused = true;
        }
    }

    public void quit() {
        gameIsPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void restart() {  
        gameIsPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(GameManager.sceneNum);
    }

    public void gameOver(bool isWin=false) {

        backgroundAudio.mute = true;
        gameIsPaused = false;
        MenuUi.SetActive(true);
        Time.timeScale = 0f;
        
        if(GameManager.levelNum == 4 && isWin) {
            nextLevelText.GetComponent<TMP_Text>().text = "Change level";
        }
        
        if (!isWin) {
            nextLevelText.transform.parent.gameObject.SetActive(false); // disable the next level button
        }
        else { // player win - calculate the grade
         
            currentScore = 0;
            if (TimerUpdate.timeForResult.Hours == 0)
                maxScore = (60 - TimerUpdate.timeForResult.Minutes) * 10;
            else
                maxScore=10;
            calcScore = true;
        }
    }

    public void finishLevel() {
        if(GameManager.character == 0) {
            GameObject.Find("Player").GetComponent<PlayerMovement>().defaultProp(); // reset player properties
        }
        else if(GameManager.character == 1) {
            GameObject.Find("Player2").GetComponent<PlayerMovement>().defaultProp();
        }
        
        if (GameManager.levelNum < 4) {
            Time.timeScale = 1f;
            gameIsPaused = false;
            GameManager.levelNum++;
            SceneManager.LoadScene(GameManager.sceneNum);
        }
        else {
            Time.timeScale = 1f;
            SceneManager.LoadScene(0); // win the game
        }
    }
}
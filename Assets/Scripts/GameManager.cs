using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static int sceneNum;
    public static int levelNum;
    public static int character;

    public void setStage(int stage) {
        sceneNum = stage;
    }

    public void setLevel(int level) {
        levelNum = level;
    }
    
    public void PlayGame() {
        SceneManager.LoadScene(sceneNum);
    }

    public void setCharacter(int charNum) {
        character = charNum;
    }
}
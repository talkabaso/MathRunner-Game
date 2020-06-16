using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    
    [SerializeField] GameObject[] prefabs;
    [SerializeField] GameObject TargetScore;
    [SerializeField] int minTargetScore = 0;
    System.Random rnd;
    public static int targetNum;
    private Text targetScoreText;    

    void Start() {
        
        targetScoreText = TargetScore.GetComponent<Text>();
        rnd= new System.Random();
        
        switch (GameManager.levelNum) {
            case 1: // score target will be in range [0, 100), without Mul, Div and ThreeDigits numbers
                prefabs[0].SetActive(false); // Mul
                prefabs[1].SetActive(false); // Div
                prefabs[3].SetActive(false); // three digits
                genTarget(100);
                break;

            case 2: // score target will be in range [0, 1000), without Mul and Div
                prefabs[0].SetActive(false);
                prefabs[1].SetActive(false);
                genTarget(1000);
                break;

            case 3: // score target will be in range [0, 100), without ThreeDigits numbers
                prefabs[3].SetActive(false);
                genTarget(100);
                break;

            case 4: // score target will be in range [0, 1000) include all
                genTarget(1000);
                break;
        }
    }

    void Update() {}

    private void genTarget(int maxNumber) {
        targetNum = Random.Range(minTargetScore, maxNumber);
        targetScoreText.text = targetNum.ToString();
    }
}
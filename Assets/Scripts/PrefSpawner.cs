using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PrefSpawner : MonoBehaviour {

    [SerializeField] GameObject[] prefabs;
    [SerializeField] float leftLimitForSpawn = -6.5f;
    [SerializeField] float rightLimitForSpawn = 6.5f;
    [SerializeField] int numberLayer = 10;
    [SerializeField] int signLayer = 11;
    [SerializeField] int enemyLayer = 13;
    [SerializeField] int boostLayer = 14;
    [Tooltip("Minimum time between spawns in seconds")] [SerializeField] float TimeBetweenSpawns;
    private GameObject PlayerObj;
    private int randomNumber;
    private int randomPosY;
    private int randomPosZ;
    private float randomPosX;

    void Start() {

        if(GameManager.character == 0)
            PlayerObj = GameObject.Find("Player");

        else if(GameManager.character == 1)
            PlayerObj = GameObject.Find("Player2");

        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine() {

        while (true) {
            
            yield return new WaitForSeconds(TimeBetweenSpawns);
            randomPosX = Random.Range(leftLimitForSpawn, rightLimitForSpawn);
            randomPosY = Random.Range(1, 5);
            randomPosZ = Random.Range(1, 16);

            if (prefabs[0].layer == numberLayer) { // use switch case on digits
                switch (prefabs[0].tag) {
                    case "OneDigit" :
                        randomNumber = Random.Range(0, 10);
                        break;

                    case "TwoDigits":
                        randomNumber = Random.Range(10, 100);
                        break;

                    case "ThreeDigits":
                        randomNumber = Random.Range(100, 1000);
                        break;
                }

                Instantiate(prefabs[0], new Vector3 (randomPosX, randomPosY, PlayerObj.transform.position.z + 10 * randomPosZ),
                                                    Quaternion.identity)
                        .transform.GetComponentInChildren<TextMeshPro>().text = randomNumber.ToString();
            }
            else if (prefabs[0].layer == signLayer) {
                Instantiate(prefabs[0], new Vector3 (randomPosX,randomPosY,PlayerObj.transform.position.z + 10 * randomPosZ),Quaternion.identity)
                .transform.GetComponentInChildren<TextMeshPro>().text = prefabs[0].transform.GetComponentInChildren<TextMeshPro>().text;                
            }
            else if (prefabs[0].layer == enemyLayer) {
                int enemyNum = genExtra();
                int distanceFromPlayer;
                if (prefabs[0].tag == "MeteorBallEnemy")
                    distanceFromPlayer = 200;
                else
                   distanceFromPlayer = 125;
                   
                Instantiate(prefabs[enemyNum], new Vector3 (randomPosX,prefabs[enemyNum].transform.position.y,
                                PlayerObj.transform.position.z + distanceFromPlayer), Quaternion.identity);
            }
            else if (prefabs[0].layer == boostLayer) {
                int boostNum = genExtra();
                while((GameManager.levelNum == 1 || GameManager.levelNum == 2) && boostNum == 1){ // prevent mulTwo
                    boostNum = genExtra();
                }
                Instantiate(prefabs[boostNum], new Vector3 (randomPosX, prefabs[boostNum].transform.position.y, 
                            PlayerObj.transform.position.z + 5 * randomPosZ), Quaternion.identity);
            }
        }
    }

    private int genExtra() {
        int maxOptions = prefabs.Length;
        int prefNum = Random.Range(0, maxOptions); 
        return prefNum;   
    }
}
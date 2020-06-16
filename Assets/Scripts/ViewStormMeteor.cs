using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewStormMeteor : MonoBehaviour {
    [SerializeField] GameObject[] Prefabs;
    [SerializeField] float posX = 40f; // distance from ground
    [Tooltip("Minimum time between spawns in seconds")] [SerializeField] float TimeBetweenSpawns;
    private float randomSide;
    void Start() {
        StartCoroutine(SpawnRoutine());
    }

    void Update() {}

    private IEnumerator SpawnRoutine() {

        while (true) {
            yield return new WaitForSeconds(TimeBetweenSpawns);
            randomSide = Random.Range(0, 2);
            if (randomSide == 0) // random between two sides (Symmetric X position)
                posX *= -1;
            GameObject.Instantiate(Prefabs[0],
            new Vector3 (posX,transform.position.y + 5, transform.position.z + 40), Quaternion.identity);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    [SerializeField] GameObject[] targets;
    [SerializeField] float height;
    [SerializeField] float distance;
    private Vector3 cameraPos;
    private int index;

	void Start () {
       cameraPos = this.transform.position;
       visiblePlayers();
	}

    void Update() {
        index = GameManager.character;
	}    
	
    void visiblePlayers() {

        for (int i=0; i<targets.Length; i++) {
            if (i == GameManager.character) {
                targets[i].SetActive(true);
            }
            else {
                targets[i].SetActive(false);
            }
        }
    }
	
    void LateUpdate() {
        
        cameraPos.x = targets[index].transform.position.x;
        cameraPos.y = Mathf.Lerp(cameraPos.y, targets[index].transform.position.y + height, Time.deltaTime);
        cameraPos.z = targets[index].transform.position.z - distance;
        transform.position = cameraPos;
    }
}
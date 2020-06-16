using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInMeteor : MonoBehaviour {
    
    [SerializeField] Vector3 reLocationVector3 = new Vector3(0, 0 , -680f); // position to move before ground ending
    [SerializeField] GameObject[] Prefabs;
    private GameObject currentPrefabObject;
    private bool alreadyFired = false;

    void Start() {}

    // Update is called once per frame
    void Update() {

        if (transform.position.z >= 670 && alreadyFired == false) {
            BeginEffect(0);
            alreadyFired = true;
        }
        else {
            if (transform.position.z < 660)
                alreadyFired = false;
        }
        if (transform.position.z > 680) {
            reLocationVector3.x=transform.position.x;
            transform.position = reLocationVector3;
            PlayerMovement.landing = true;
            PlayerMovement.landingSpeed = PlayerMovement.fastLanding;
		}
    }

    private void BeginEffect(int prefIndex) {
        currentPrefabObject = GameObject.Instantiate(Prefabs[prefIndex],
        new Vector3 (transform.position.x,transform.position.y + 5,transform.position.z+20), Prefabs[prefIndex].transform.rotation);
    }
}
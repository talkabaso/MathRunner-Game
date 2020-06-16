using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSetter : MonoBehaviour {

    [SerializeField] GameObject floorOnRunning; // current floor
    [SerializeField] GameObject floorForward; // next floor
    [SerializeField] int distForSwap = 400; // when to create the next floor
    [SerializeField] float distToNextFloor = 1500f; // where to create the next floor

	void Update () {
        
        // after we pass distForSwap of the current floor, replace the prev floor to be the next floor
        if(transform.position.z > floorOnRunning.transform.position.z + distForSwap) {
            floorOnRunning.transform.position = new Vector3(floorOnRunning.transform.position.x,
                        floorOnRunning.transform.position.y, floorOnRunning.transform.position.z + distToNextFloor);

            GameObject temp = floorOnRunning;
            floorOnRunning = floorForward;
            floorForward = temp; 
        }
	}
}

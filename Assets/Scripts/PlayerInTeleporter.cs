using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInTeleporter : MonoBehaviour {

    [SerializeField] Vector3 reLocationVector3 = new Vector3(0, 0 , -680f); // position to move before ground ending
    [SerializeField] GameObject teleporter;
    [SerializeField] float offsetMetersInside = 1; // meters to be in the teleporter before relocation
    private float portPosZ;

    void Start() {
        portPosZ = teleporter.transform.position.z;
    }

    void Update() {

        if (transform.position.z > portPosZ + offsetMetersInside) {
            transform.position = reLocationVector3;
            PlayerMovement.landing = true;
            PlayerMovement.landingSpeed = PlayerMovement.fastLanding;
		}
    }
}

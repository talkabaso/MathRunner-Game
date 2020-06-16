using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInVortex : MonoBehaviour {

    [SerializeField] Vector3 reLocationVector = new Vector3(0, 32f , -680f); // position to move before ground ending
    [SerializeField] Vector3 rotationPoint = new Vector3(0,-19.5f,665f); // rotation point around this point
    [SerializeField] float rotationSpeed = 50f; // degrees per second
    [SerializeField] float maxRotation = 70f;
    [SerializeField] GameObject vortex;
    [SerializeField] float metersInVortex = 13.5f; // how much meters to enter inside the vortex
    private float vortexPosZ;

    void Start() {
        vortexPosZ = vortex.transform.position.z;
    }

    void Update() {

        if (transform.position.z > vortexPosZ - metersInVortex) { // rotate player for feeling in vortex        
            transform.RotateAround(rotationPoint, Vector3.up, (rotationSpeed * Time.deltaTime));
        }

        if (transform.rotation.eulerAngles.y >= maxRotation) {
            transform.position = reLocationVector;
			transform.rotation = Quaternion.Euler(Vector3.zero);
			PlayerMovement.landing = true;
			PlayerMovement.landingSpeed = PlayerMovement.fastLanding;
        }
    }
}
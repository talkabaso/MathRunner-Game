using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInTornado : MonoBehaviour {
    [SerializeField] Vector3 reLocationVector = new Vector3(0, 32f , -680f); // position to move before ground ending
	[SerializeField] GameObject tornado;
	[SerializeField] float metersInTornado = 12f; //meters to be in the vortex

	private float tornadoPosZ;

	void Start () {
		tornadoPosZ = tornado.transform.position.z;
	}

	void Update() {
		
		if (transform.position.z > tornadoPosZ + metersInTornado) {
			transform.position = reLocationVector;
			PlayerMovement.landing = true;
			PlayerMovement.landingSpeed = PlayerMovement.fastLanding;
		}
	}
}

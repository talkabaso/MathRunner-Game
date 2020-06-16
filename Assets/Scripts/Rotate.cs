using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

	[SerializeField] int rotationSpeed = 5; // tornado rotation degrees per second

	void Start () {}

	void Update() {
		transform.Rotate( new Vector3(0, rotationSpeed, 0), Space.Self );
	}
}
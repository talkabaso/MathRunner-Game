using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuffInTornado : MonoBehaviour {

	[SerializeField] float rotationSpeed = 75f; // degrees per second
	[SerializeField] int direction = 1;
	void Start () {}

	void Update() {
		transform.RotateAround(transform.parent.position, Vector3.up, (rotationSpeed * Time.deltaTime) * direction);
	}
}
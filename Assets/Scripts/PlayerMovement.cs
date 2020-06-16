using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	[Tooltip("Power of movement in X axis")] [SerializeField] float force = 200f;
	[Tooltip("Speed of the player")] [SerializeField] float speed = 7f;
	[Tooltip("Gravity for jumping height ")] [SerializeField] float gravity = 20f;
    [Tooltip("The jumping height")] [SerializeField] float jumpHeight = 30f;
	[Tooltip("The rotation of the player while sliding")] [SerializeField] float slideRotation = -85f;	
    [SerializeField] GameObject[] boostsImages;
	[SerializeField] Animator anim;
	[SerializeField] AudioSource[] playerSounds; // 0- jump, 1- slide
	[SerializeField] Rigidbody player;	
	[SerializeField] Vector3 reLocationVector = new Vector3(0, 32f , -680f); // position to move before ground ending
	[SerializeField] float tornadoPos = 670f;
	[SerializeField] float vortexPos = 664f;
	[SerializeField] float maxJumpHeight = 3.6f;
	[SerializeField] float groundHeight	= 0.51f;
	[SerializeField] float slideDuration = 1.5f;
	[SerializeField] float boostDuration = 10f;
	private bool grounded = true; // when player is running on the floor
	private bool needToJump = false; // indicates if player pressed "W" or UpArrow
	public static string boost = ""; // which boost in use
	public static float fastLanding = 7.5f; // landing speed after relocation
	public static float landingSpeed = 3.25f;
	public static bool landing = true; // when player is landing after jump
	
	void Awake() { // when restart game apply correct properties - happens before start
    	foreach (GameObject boost in boostsImages)
			boost.SetActive(false);
		defaultProp();
 	}

	void Update() {

		transform.position += (new Vector3(0,0,speed)) * Time.deltaTime; // move all time in z axis (run)
		if (transform.position.y <= groundHeight) {
			grounded = true;
			landing = false;
			if (landingSpeed == fastLanding)
				landingSpeed = 2.5f;
		}
		else // while jumping
			grounded = false;
		
		// check if clicked on W or UpArrow and player is grounded for prvent jump while jump
		if (Input.GetKeyDown(KeyCode.W) && grounded || Input.GetKeyDown(KeyCode.UpArrow) && grounded)
			needToJump = true;


		// check if clicked on S or DownArrow and player is grouned for prvent slide while jump
		if (Input.GetKeyDown(KeyCode.S) && grounded || Input.GetKeyDown(KeyCode.DownArrow) && grounded)
			StartCoroutine(slide());

		if (!grounded && player.transform.position.y >= maxJumpHeight) // player is at maximum height and need to land
			landing = true;

		StartCoroutine(useBoost(boost)); // check if took boost
	}

	private void FixedUpdate() {

		if (grounded)
			anim.Play("Run");

		if (needToJump) {
			anim.Play("Jump");
			playerSounds[0].Play();
         	player.velocity = new Vector3(player.velocity.x, CalculateJumpVerticalSpeed(), player.velocity.z);
			needToJump = false;
        }

		// Move player on the X axis (sideways)
		if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
			if (grounded)
				player.AddForce(force * Time.deltaTime, 0f, 0f, ForceMode.VelocityChange);
		}

		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
			if (grounded)
				player.AddForce(-force * Time.deltaTime, 0f, 0f, ForceMode.VelocityChange);
		}

		if (landing)
			player.velocity = new Vector3(player.velocity.x, -landingSpeed, player.velocity.z);
	}

	private IEnumerator slide() {

		anim.enabled = false;
		playerSounds[1].Play();
 		transform.rotation = Quaternion.Euler(slideRotation, transform.rotation.y, transform.rotation.z);		
		yield return new WaitForSeconds(slideDuration); // wait in floor 1.5 second
		transform.rotation = Quaternion.Euler(Vector3.zero);
		anim.enabled = true;
		anim.Play("Run");
	}

	private IEnumerator useBoost(string boostType) {

		if(boostType == "HeavyWeight") {
			boostsImages[0].SetActive(true);
			speed = 3f;
			jumpHeight = 3f;
			yield return new WaitForSeconds(boostDuration); // use boost for 10 seconds
			defaultProp();
		}

		else if(boostType == "PowerUp") {
			boostsImages[1].SetActive(true);		
			speed = 15f;
			yield return new WaitForSeconds(boostDuration); // use boost for 10 seconds
			defaultProp();
		}
		else if(boostType == "PogoStick"){
			jumpHeight = 50f;
			maxJumpHeight= 4.72f;
			boostsImages[2].SetActive(true);		
			yield return new WaitForSeconds(boostDuration); // use boost for 10 seconds
			defaultProp();
		}
	}

	public void defaultProp() {
		foreach(GameObject boost in boostsImages)
			boost.SetActive(false);
		speed = 7f;
		jumpHeight = 30f;
		maxJumpHeight= 3.6f;
		boost = "";
	}

	private float CalculateJumpVerticalSpeed() {
        return Mathf.Sqrt(2 * jumpHeight * gravity); // using Uniform acceleration
    }
}

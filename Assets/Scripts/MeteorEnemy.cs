using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorEnemy : MonoBehaviour {
    
    [Tooltip("Speed of the meteor")] [SerializeField] float speed = 5f;
    [SerializeField] int numberLayer = 10;
    [SerializeField] int signLayer = 11;
    [SerializeField] int enemyLayer = 13;
    [SerializeField] int boostLayer = 14;
    
    void Start() {}

    void Update() {
        transform.position += (new Vector3(0, 0, -speed)) * Time.deltaTime;
        transform.Rotate(new Vector3(-2f, 0, 0), Space.Self);	
    }

    public void OnCollisionEnter(Collision collision) {

        if(collision.gameObject.layer == numberLayer || collision.gameObject.layer == signLayer
         || collision.gameObject.layer == enemyLayer || collision.gameObject.layer == boostLayer) {
            Destroy(collision.gameObject);
        }
    }
}
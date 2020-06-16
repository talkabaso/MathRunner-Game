using UnityEngine;

public class Destroy: MonoBehaviour {

    [SerializeField] float distToDestroy = 7f;        
    [SerializeField] float maxDistanceInGround = 658f;
    private GameObject PlayerObj;

    void start() {}

    void Update() {
        
        PlayerObj = GameObject.FindGameObjectWithTag("Player");
        // if the player pass the object or the object created after the portal
        if (transform.position.z + distToDestroy < PlayerObj.transform.position.z ||
            transform.position.z >= maxDistanceInGround) {
                Destroy(gameObject); 
        }
    }
}
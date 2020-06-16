using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class CollectOnTrigger : MonoBehaviour {
    
    [SerializeField] Rigidbody rb;
    [SerializeField] GameObject EndMenu;
    [SerializeField] GameObject sounds;
    private AudioSource[] audioSounds; // 0- coin, 1- alreadyUseSign, 2- notEqual, 3- scoreEqual, 4- hit
    [SerializeField] GameObject equalPanel;
    [SerializeField] Text sumUI;    
    [SerializeField] Text PlusText;
    [SerializeField] Text SubText;
    [SerializeField] Text MulText;
    [SerializeField] Text DivText;
    [SerializeField] TMP_Text exercise;
    [SerializeField] TMP_Text errorMsg;
    [SerializeField] int numberLayer = 10;
    [SerializeField] int signLayer = 11;
    [SerializeField] int wallLayer = 12;
    [SerializeField] int enemyLayer = 13;
    [SerializeField] int boostLayer = 14;
    [SerializeField] int tornadoLayer = 15;
    [SerializeField] int waterLayer = 4;    
    [SerializeField] int playerLife = 3;
    [SerializeField] float sum = 0;
    [SerializeField] Vector3 offsetSumtext = new Vector3(0,14f,0);
    [SerializeField] Vector3 offsetSigntext = new Vector3(-12f,13.5f,0);
    [SerializeField] Vector3 offsetBoostCanvas = new Vector3(13f,10f,0);
    private Vector3 sumTextPos, signTextPos, boostCanvasPos;
    private int lastCollected; // need to collect number at the beginning
    private string currentSign = "Plus";

    void Start() {
        lastCollected = signLayer;
        audioSounds = sounds.GetComponents<AudioSource>();
    }

    void Update() {}

    public void OnCollisionEnter(Collision collision) {

        initializeRelativePos(collision); // get all panels positions

        if (collision.gameObject.tag == "Equal") {
            if (sum == LevelManager.targetNum) {
                audioSounds[3].Play(); // sum equal - win
                EndMenu.GetComponent<MenuScript>().gameOver(true); //GameOver menu
            }
            else { // collect equal sign and sum not equal to target
                StartCoroutine(changeErrMsg("Equal"));
                audioSounds[2].Play(); // not Equal
                Destroy(collision.gameObject);
            }
        }           
        else if (collision.gameObject.layer == numberLayer) {
            
            if (lastCollected != numberLayer) { // need to collect number
                TextMeshPro collisionText = collision.gameObject.transform.GetComponentInChildren<TextMeshPro>();
                
                if (currentSign =="Div" && sum % float.Parse(collisionText.text) != 0) { // sum is undivided
                    StartCoroutine(changeErrMsg(collisionText.text,true)); // because sum is undivided by collision number
                    audioSounds[1].Play(); // already use sign
                    Destroy(collision.gameObject);
                }
                else { // if not collected div and not divisible
                    lastCollected = numberLayer;
                    audioSounds[0].Play(); // collect
                    collision.gameObject.transform.position = Vector3.Lerp(collision.gameObject.transform.position, 
                                                                            sumTextPos, 0.5f);
                    StartCoroutine(doExercise(collisionText.text)); // write and execute the exercise
                    sumUI.text = "Sum: " + sum;
                    StartCoroutine(searchEqualMsg());
                }
                
                Destroy(collision.gameObject, 1f); // wait 1 sec in order to see the number moving up
            }
            else { // if you collected number and the last element was number
                StartCoroutine(changeErrMsg("Number"));
                audioSounds[1].Play(); // already use sign
                Destroy(collision.gameObject); // destroy the number because we shouldn't collect it
            }         
        }
        else if (collision.gameObject.layer == signLayer) {
            if (lastCollected != signLayer) {
                currentSign = collision.gameObject.tag;

                if (currentSign != "Equal") {
                    changeSignText(currentSign); // change the sign text on the left corner
                    collision.gameObject.transform.position = Vector3.Lerp(collision.gameObject.transform.position,
                                                                            signTextPos, 0.45f);
                    lastCollected = signLayer;
                    audioSounds[0].Play(); // coin sound
                    Destroy(collision.gameObject, 10f); // wait 10 seconds in order to see the sign moving up
                }
            }
            else { // cannot collect sign after sign
                StartCoroutine(changeErrMsg("Sign"));
                audioSounds[1].Play(); // already use sign
                Destroy(collision.gameObject); // destroy the sign because we shouldn't collect it
            }
        }
        else if (collision.gameObject.layer == wallLayer) {

            if (collision.gameObject.tag == "WallL") {
                rb.velocity = new Vector3(25f, 10f, rb.velocity.z); // if touch left wall get hit and move back to center
                rb.AddForce(0f, -5f, 0f, ForceMode.VelocityChange); // while hit, force fast landing
                Debug.Log("Touched with left");
            }

            if (collision.gameObject.tag == "WallR") {
                rb.velocity = new Vector3(-25f, 10f, rb.velocity.z); // if touch right wall get hit and move back to center
                rb.AddForce(0f, -5f, 0f, ForceMode.VelocityChange); // while hit, force fast landing
                Debug.Log("Touched with right");
            }
            audioSounds[4].Play();
        }

        else if (collision.gameObject.layer == enemyLayer) {
            audioSounds[4].Play(); // hit with enemy
            Destroy(collision.gameObject);
            decreaseLife();
        }

        else if (collision.gameObject.layer == boostLayer) {
            string collisionTag = collision.gameObject.tag;
            if (collisionTag == "MulTwo") {
                sum *= 2;
                StartCoroutine(searchEqualMsg());
                sumUI.text = "Sum: " + sum;
                Destroy(collision.gameObject);
            }
            else { // other boost 
                collision.gameObject.transform.position = Vector3.Lerp(collision.gameObject.transform.position,
                                                                     boostCanvasPos, 0.7f);
                PlayerMovement.boost = collisionTag; // change the static var of playerMovement
                Debug.Log(PlayerMovement.boost + " collectOnTrigger");
                Destroy(collision.gameObject,0.4f);
            }
        }
        else if (collision.gameObject.layer == tornadoLayer) {
            transform.position += (new Vector3(0, 20f, 0));
        }
        else if (collision.gameObject.layer == waterLayer) {
            Debug.Log("touch water");
            EndMenu.GetComponent<MenuScript>().gameOver(false); // player lost the game
        }
    }

    private void changeSignText(string signCollected) {

        if (signCollected == "Plus"){
            PlusText.text = "+";
        }
        if (signCollected == "Sub"){
            SubText.text = "-";
        }                    
        if (signCollected == "Mul"){
            MulText.text = "X";
        }
        if (signCollected == "Div"){ // sum is divisible
            DivText.text = "/";
        }
    }

    private void decreaseLife() {

        GameObject currentLife;
        switch (playerLife) {
            case 3:
                playerLife--;
                currentLife = GameObject.Find("Life 3");
                Destroy(currentLife);
                break;
            
            case 2:
                playerLife--;
                currentLife = GameObject.Find("Life 2");
                Destroy(currentLife);
                break;

            case 1:
                playerLife--;
                currentLife = GameObject.Find("Life 1");
                Destroy(currentLife);
                EndMenu.GetComponent<MenuScript>().gameOver(false); // player lost the game
                break;
        }
    }
    
    private IEnumerator doExercise(string secondParam) {
        
        string initialEx = sum.ToString();
        if (currentSign == "Plus") {
            sum +=  float.Parse(secondParam);
            PlusText.text = "";
            initialEx += "+";    
        }
        if (currentSign == "Sub") {
            sum -=  float.Parse(secondParam);
            SubText.text = "";
            initialEx += "-";
        }                    
        if (currentSign == "Mul") {
            sum *=  float.Parse(secondParam);
            MulText.text = "";
            initialEx += "*";
        }
        if (currentSign == "Div") { // sum is divided
            sum /=  float.Parse(secondParam);
            DivText.text = "";
            initialEx += "/";
        }
        exercise.text = initialEx + secondParam + "=" + sum;
        yield return new WaitForSeconds(1.5f); // wait in floor 1.5 seconds
        exercise.text = "";
    }

    private IEnumerator changeErrMsg(string secondParam, bool notDivisible=false) {
        
        if (notDivisible) {
            errorMsg.text = sum + " not divisible by " + secondParam;
        }
        else {
            if (secondParam.Equals("Equal"))
                errorMsg.text = "collect Equal just after you reach the target";
            else
                errorMsg.text = "oops! cannot collect " + secondParam;
        }
        yield return new WaitForSeconds(2f); // show msg for 2 seconds
        errorMsg.text= "";
    }

    private IEnumerator searchEqualMsg() {

        if (sum == LevelManager.targetNum) {
            equalPanel.SetActive(true);
            yield return new WaitForSeconds(2f);
            equalPanel.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            equalPanel.SetActive(true);
            yield return new WaitForSeconds(2f);
            equalPanel.SetActive(false);
        }
    }

    private void initializeRelativePos(Collision collision) {

        sumTextPos = signTextPos = boostCanvasPos = new Vector3(transform.position.x, 0,
                                                                        collision.gameObject.transform.position.z);
        sumTextPos += offsetSumtext;
        signTextPos += offsetSigntext;
        boostCanvasPos += offsetBoostCanvas;
    }    
}
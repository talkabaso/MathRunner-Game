using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour {
    
    [SerializeField] GameObject[] targets;
    
    void Start() {
        
        for (int i=0; i<targets.Length; i++) {
            if (i == GameManager.character){
                targets[i].SetActive(true);
            }
            else {
                targets[i].SetActive(false);
            }
        }
    }

    void Update() {}
}
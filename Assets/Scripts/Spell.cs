using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : MonoBehaviour {

    //--------------------------------------------------------------------------------

    float maxTimeToLive;
    float timeToLive;

    int damage;

    //--------------------------------------------------------------------------------

    void Start() {

    	timeToLive = maxTimeToLive;
    	
    }

    //--------------------------------------------------------------------------------

    void Update() {

    	//
        
    }

    //--------------------------------------------------------------------------------
}
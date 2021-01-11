using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Item {

	//--------------------------------------------------------------------------------
    
    void Start() {

    	float randomOffsetHorizontal = Random.Range(-0.1f, 0.1f);
    	float randomOffsetVertical = Random.Range(-0.1f, 0.1f);
    	transform.position += new Vector3(randomOffsetHorizontal, randomOffsetVertical, 0f);
        
    }

    //--------------------------------------------------------------------------------

    public override string GetItemType() { return "Coin"; }

    //--------------------------------------------------------------------------------

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : Spell {

	//--------------------------------------------------------------------------------

	public float growthSpeed;

    //--------------------------------------------------------------------------------

    public override void SpecificUpdate() {

    	growthSpeed = 15f;

    	direction = Vector2.zero;

    	transform.localScale += new Vector3(growthSpeed * Time.deltaTime, growthSpeed * Time.deltaTime, 0);
        
    }

    public override string GetName() { return "ForceField"; }

    //--------------------------------------------------------------------------------

    void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Item") {
            return;
        } else if (collision.gameObject.tag == "Enemy") { 
        	Attack(collision.gameObject); 
        } else {
        	Despawn();
        }

    }

    //--------------------------------------------------------------------------------

}

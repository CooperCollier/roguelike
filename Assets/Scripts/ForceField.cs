using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : Spell {

    //--------------------------------------------------------------------------------

    public override void SpecificUpdate() {

    	direction = Vector2.zero;

    	// expand outward by a small amount each frame
        
    }

    //--------------------------------------------------------------------------------

    void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Item") {
            return;
        } else if (collision.gameObject.tag == "Enemy") { 
        	Attack(collision.gameObject); 
        }

    }

    //--------------------------------------------------------------------------------

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour {

	[SerializeField]
	int hitPoints;

	//--------------------------------------------------------------------------------

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Spell" && hitPoints <= 1) {
        	Destroy(gameObject);
        } else if (collision.gameObject.tag == "Spell") {
        	hitPoints -= 1;
        }
    }

    //--------------------------------------------------------------------------------

}

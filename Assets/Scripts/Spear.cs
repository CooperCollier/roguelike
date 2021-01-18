using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : Spell {

	//--------------------------------------------------------------------------------

	int collisions = 2;

	public override void SpecificUpdate() {

    }

    public override string GetName() { return "Spear"; }

    //--------------------------------------------------------------------------------

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Item"
         || collision.gameObject.tag == "Spell" || collision.gameObject.tag == "EnemyProjectile") {
            return;
        } else if (collision.gameObject.tag == "Enemy") {
            Attack(collision.gameObject);
            if (collisions == 0) {
            	Despawn();
            } else {
            	collisions -= 1;
            }
        } else if (collision.gameObject.tag == "Environment") {
            if (collisions == 0) {
                Despawn();
            } else {
                collisions -= 1;
            }
        } else {
        	Despawn();
        }
        
    }

    //--------------------------------------------------------------------------------

}

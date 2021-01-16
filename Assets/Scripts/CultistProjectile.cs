using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CultistProjectile : EnemyProjectile {

	//--------------------------------------------------------------------------------

    public override void SpecificUpdate() {

    }

    //--------------------------------------------------------------------------------

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Item"
         || collision.gameObject.tag == "Spell") {
            return;
        } else if (collision.gameObject.tag == "Player") {
            Attack(collision.gameObject);
        }
        Despawn();
    }

    //--------------------------------------------------------------------------------

}


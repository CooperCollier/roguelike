using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpikes : Spell {

	//--------------------------------------------------------------------------------

	public override void SpecificUpdate() {

    }

    public override string GetName() { return "IceSpikes"; }

    //--------------------------------------------------------------------------------

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Item"
         || collision.gameObject.tag == "Spell" || collision.gameObject.tag == "EnemyProjectile") {
            return;
        } else if (collision.gameObject.tag == "Enemy") {
            Attack(collision.gameObject);
        }
        Despawn();
    }

    //--------------------------------------------------------------------------------

}

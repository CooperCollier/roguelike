using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Spell {

    //--------------------------------------------------------------------------------

    [SerializeField]
    public float blastRadius;

    bool exploded = false;

    float explosionTime = 0.5f;

    float explosionRadius = 4f;

	//--------------------------------------------------------------------------------

	public override void SpecificUpdate() {
        if (explosionTime <= 0) { 
            Despawn(); 
        } else if (exploded) {
            explosionTime -= Time.deltaTime;
        }
    }

    public override string GetName() { return "Fireball"; }

    //--------------------------------------------------------------------------------

    void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Item"
         || collision.gameObject.tag == "Spell" || collision.gameObject.tag == "EnemyProjectile") {
            return;
        }

        if (!exploded) {
            //Change animation to blast
            transform.localScale *= explosionRadius;
            direction = Vector2.zero;
            exploded = true;
        }

        if (collision.gameObject.tag == "Enemy") { Attack(collision.gameObject); }

    }

    //--------------------------------------------------------------------------------

}


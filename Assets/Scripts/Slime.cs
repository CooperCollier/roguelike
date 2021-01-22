using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy {

	//--------------------------------------------------------------------------------

	public float attackTime = 0.3f;

    public Vector3 destination = Vector3.zero;

    //--------------------------------------------------------------------------------

    public override void SpecificUpdate() {

    	Move();

        UpdateAnimation();

    }

    //--------------------------------------------------------------------------------

    public override void Move() {

        destination = playerLocation + new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0);
    	Vector3 direction = destination - transform.position;
    	direction = direction.normalized;
    	rigidbody2D.velocity = direction * speed;

    }

    //--------------------------------------------------------------------------------

    public override void Attack() {
    	player.SendMessage("TakeDamage", attack); // Change This
        //pauseTime = maxPauseTime;
    }

    //--------------------------------------------------------------------------------

    public override void UpdateAnimation() {

        if (rigidbody2D.velocity.x <= 0) {
            currentAnimation = "SlimeLeft";
        } else {
            currentAnimation = "SlimeRight";
        }

        if(!animator.GetCurrentAnimatorStateInfo(0).IsName(currentAnimation)) {
            animator.Play(currentAnimation, 0);
        }

    }

    //--------------------------------------------------------------------------------

    void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
        	if (attackTime <= 0) {
        		Attack();
        		attackTime = 0.3f;
        	} else {
        		attackTime -= Time.deltaTime;
        	}
        }
    }

    //--------------------------------------------------------------------------------
}

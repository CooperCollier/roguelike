using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy {

    //--------------------------------------------------------------------------------

    public override void SpecificUpdate() {

    	Move();

        UpdateAnimation();

    }

    //--------------------------------------------------------------------------------

    public override void Move() {

    	Vector3 direction = playerLocation - transform.position;
    	direction = direction.normalized;
    	rigidbody2D.velocity = direction * speed;

    }

    //--------------------------------------------------------------------------------

    public override void Attack() {
    	player.SendMessage("TakeDamage", attack); // Change This
        pauseTime = maxPauseTime;
    }

    //--------------------------------------------------------------------------------

    public override void UpdateAnimation() {

        if (rigidbody2D.velocity.x <= 0) {
            currentState = "Left";
        } else {
            currentState = "Right";
        }

        string stateName = "Slime" + currentState;

        if(!animator.GetCurrentAnimatorStateInfo(0).IsName(stateName)) {
            animator.Play(stateName, 0);
        }

    }

    //--------------------------------------------------------------------------------

    void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
        	Attack();
        }
    }

    //--------------------------------------------------------------------------------
}

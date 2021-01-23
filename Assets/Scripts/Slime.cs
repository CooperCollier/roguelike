using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy {

	//--------------------------------------------------------------------------------

	public float attackTime = 0f;

    public Vector3 destination = Vector3.zero;

    public float attackCooldownTime = 0f;

    //--------------------------------------------------------------------------------

    public override void SpecificUpdate() {

        if (attackCooldownTime <= 0f) {
    	   Move();
        } else {
            rigidbody2D.velocity = Vector2.zero;
            attackCooldownTime -= Time.deltaTime;
        }

        UpdateAnimation();

    }

    //--------------------------------------------------------------------------------

    public override void Move() {

        destination = playerLocation + new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0);
    	Vector3 direction = destination - transform.position;
        float distance = direction.magnitude;
    	direction = direction.normalized;
        if (distance > 1f) {
            rigidbody2D.velocity = direction * speed * 0.9f;
        } else if (distance < 0.5f) {
    	   rigidbody2D.velocity = direction * speed * 1.5f;
        } else {
            rigidbody2D.velocity = direction * speed;
        }

    }

    //--------------------------------------------------------------------------------

    public override void Attack() {
    	player.SendMessage("TakeDamage", attack); // Change This
        attackCooldownTime = 1f;
    }

    //--------------------------------------------------------------------------------

    public override void UpdateAnimation() {

        if (attackCooldownTime > 0 && playerLocation.x <= transform.position.x) {
            currentAnimation = "SlimeAttkLeft";
        } else if (attackCooldownTime > 0 && playerLocation.x > transform.position.x) {
            currentAnimation = "SlimeAttkRight";
        } else if (rigidbody2D.velocity.x <= 0) {
            currentAnimation = "SlimeLeft";
        } else if (rigidbody2D.velocity.x > 0) {
            currentAnimation = "SlimeRight";
        }

        if(!animator.GetCurrentAnimatorStateInfo(0).IsName(currentAnimation)) {
            animator.Play(currentAnimation, 0);
        }

    }

    //--------------------------------------------------------------------------------

    void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
        	if (attackTime > 0.15f) {
        		Attack();
        		attackTime = 0f;
        	} else {
        		attackTime += Time.deltaTime;
        	}
        }
    }

    //--------------------------------------------------------------------------------
}

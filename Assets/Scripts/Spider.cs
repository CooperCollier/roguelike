using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy {

	//--------------------------------------------------------------------------------

	public float attackTime = 0.15f;

	public bool jumping = false;

	public Vector3 walkDestination = Vector3.zero;

	public Vector3 jumpDestination = Vector3.zero;

    //--------------------------------------------------------------------------------

    public override void SpecificUpdate() {

    	if (walkDestination == Vector3.zero) { SetWalkDestination(); }

    	Move();

        UpdateAnimation();

    }

    //--------------------------------------------------------------------------------

    public override void Move() {
    	if (!jumping) {
    		Walk();
    	} else {
    		Jump();
    	}
    }

    //--------------------------------------------------------------------------------

    public void SetWalkDestination() {
    	float rand = Random.value;
    	if (rand < 0.25f) {
    		walkDestination = playerLocation + new Vector3(0, -0.5f, 0);
    	} else if (rand < 0.5f) {
    		walkDestination = playerLocation + new Vector3(0, 0.5f, 0);
    	} else if (rand < 0.75f) {
    		walkDestination = playerLocation + new Vector3(-0.5f, 0, 0);
    	} else {
    		walkDestination = playerLocation + new Vector3(0.5f, 0, 0);
    	}
    }

    public void SetJumpDestination() {
    	jumpDestination = playerLocation;
    }

    //--------------------------------------------------------------------------------

    public void Walk() {
    	if ((walkDestination - transform.position).magnitude < 0.2f) {
    		jumping = true;
    		speed *= 1.5f;
    		SetJumpDestination();
    	} else {
    		Vector3 direction = walkDestination - transform.position;
    		direction = direction.normalized;
    		rigidbody2D.velocity = direction * speed;
    	}
    }

    public void Jump() {
		if ((jumpDestination - transform.position).magnitude < 0.05f) {
    		jumping = false;
    		speed /= 1.5f;
    		SetWalkDestination();
    	} else {
    		Vector3 direction = jumpDestination - transform.position;
    		direction = direction.normalized;
    		rigidbody2D.velocity = direction * speed;
    	}
    }

    //--------------------------------------------------------------------------------

    public override void Attack() {
    	player.SendMessage("TakeDamage", attack); // Change This
        //pauseTime = maxPauseTime;
    }

    //--------------------------------------------------------------------------------

    public override void UpdateAnimation() {

    	if (jumping) { 
    		currentAnimation = "SpiderJump";
    	} else {
    		currentAnimation = "SpiderWalk";
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
        		attackTime = 0.15f;
        	} else {
        		attackTime -= Time.deltaTime;
        	}
        } else if (!jumping) {
        	SetWalkDestination();
        } else if (jumping) {
        	jumping = false;
    		speed /= 1.5f;
    		SetWalkDestination();
        }
    }

    //--------------------------------------------------------------------------------
}
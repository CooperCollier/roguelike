using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy {

    [SerializeField]
    public Bone bone;

	//--------------------------------------------------------------------------------

	float attackTimer = 2f;

	Vector3 destination;

    //--------------------------------------------------------------------------------

	public override void SpecificUpdate() {

    	if (rigidbody2D.velocity.magnitude <= 0.1) {
    		Move();
    	}

    	UpdateAnimation();

    	if (attackTimer <= 0) {
    		Attack();
    		attackTimer = 2f;
    	} else {
    		attackTimer -= Time.deltaTime;
    	}

	}

	//--------------------------------------------------------------------------------

	public override void Move() {

		if (transform.position.x - playerLocation.x < 0.1f) {

			destination = new Vector3(playerLocation.x, playerLocation.y, 0);

		} else if (transform.position.y - playerLocation.y < 0.1f) {

			destination = new Vector3(playerLocation.x, playerLocation.y, 0);

		} else {

			bool randomBool = (Random.value > 0.5f);

    		if (randomBool) {
    			destination = new Vector3(transform.position.x, playerLocation.y, 0);
    		} else {
    			destination = new Vector3(playerLocation.x, transform.position.y, 0);
    		}
    		
    	}

    	Vector3 direction = (destination - transform.position).normalized;

    	rigidbody2D.velocity = direction * speed;

    }

    //--------------------------------------------------------------------------------

    public override void Attack() {
    	Bone thisBone = Instantiate(bone);
        thisBone.transform.position = transform.position;
        thisBone.direction = rigidbody2D.velocity.normalized;
    }

    //--------------------------------------------------------------------------------

    public override void UpdateAnimation() {

        if (rigidbody2D.velocity.x <= 0) {
            currentState = "Left";
        } else {
            currentState = "Right";
        }

        string stateName = "Skeleton" + currentState;

        if(!animator.GetCurrentAnimatorStateInfo(0).IsName(stateName)) {
            animator.Play(stateName, 0);
        }

    }

	//--------------------------------------------------------------------------------

}

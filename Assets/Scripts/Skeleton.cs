using System.Collections;
using static System.Math;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy {

	//--------------------------------------------------------------------------------

    [SerializeField]
    public Bone bone;

	float attackTimer = 2f;

	float moveTimer = 0.5f;

	Vector2 direction;

    //--------------------------------------------------------------------------------

	public override void SpecificUpdate() {

		if (moveTimer <= 0) {
			UpdateMovementDirection();
			moveTimer = 0.5f;
		} else {
			moveTimer -= Time.deltaTime;
		}

		if (attackTimer <= 0) {
    		Attack();
    		attackTimer = 2f;
    	} else {
    		attackTimer -= Time.deltaTime;
    	}

    	Move();

    	UpdateAnimation();

	}

	//--------------------------------------------------------------------------------

	public void UpdateMovementDirection() {

		if (Abs(transform.position.y - playerLocation.y) < 0.1f) {

			if (transform.position.x < playerLocation.x) {
				direction = Vector2.right;
			} else {
				direction = Vector2.left;
			}

		} else if (Abs(transform.position.x - playerLocation.x) < 0.1f) {

			if (transform.position.y < playerLocation.y) {
				direction = Vector2.up;
			} else {
				direction = Vector2.down;
			}

		} else {

			bool randomBool = (Random.value > 0.5f);

    		if (randomBool) {
    			
    			if (transform.position.x < playerLocation.x) {
					direction = Vector2.right;
				} else {
					direction = Vector2.left;
				}

    		} else {
    			
    			if (transform.position.y < playerLocation.y) {
					direction = Vector2.up;
				} else {
					direction = Vector2.down;
				}

    		}
    		
    	}

	}

	//--------------------------------------------------------------------------------

	public override void Move() {

    	rigidbody2D.velocity = (Vector3) direction * speed;

    }

    //--------------------------------------------------------------------------------

    public override void Attack() {
    	Bone thisBone = Instantiate(bone);
        thisBone.SetPositionAndDirection(transform.position, rigidbody2D.velocity.normalized);
    }

    //--------------------------------------------------------------------------------

    public override void UpdateAnimation() {

        if (rigidbody2D.velocity.x < 0) {
            currentAnimation = "SkeletonLeft";
        } else if (rigidbody2D.velocity.x > 0) {
            currentAnimation = "SkeletonRight";
        } else if (rigidbody2D.velocity.y < 0) {
            currentAnimation = "SkeletonFront";
        } else if (rigidbody2D.velocity.y > 0) {
            currentAnimation = "SkeletonBack";
        }

        if(!animator.GetCurrentAnimatorStateInfo(0).IsName(currentAnimation)) {
            animator.Play(currentAnimation, 0);
        }

    }

	//--------------------------------------------------------------------------------

}

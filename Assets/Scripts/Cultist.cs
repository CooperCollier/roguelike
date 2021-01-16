using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cultist : Enemy {

	//--------------------------------------------------------------------------------

	[SerializeField]
    public CultistProjectile projectile;

    [SerializeField]
    public LayerMask detectWall;

	float moveTimer = 3f;

	string attackType = "Attack1";

	//--------------------------------------------------------------------------------

	public override void SpecificUpdate() {

		if (moveTimer <= 0) {
			moveTimer = 3f;
			Move();
			if (attackType == "Attack1") { 
				attackType = "Attack2"; 
			} else {
				attackType = "Attack1";
			}
			Attack();
		} else {
			moveTimer -= Time.deltaTime;
		}

		UpdateAnimation();

	}

    //--------------------------------------------------------------------------------

    public override void Move() {

    	int attempts = 10;

    	while (attempts > 0) {
    		float randomX = Random.Range(-1f, 1f);
    		float randomY = Random.Range(-1f, 1f);
    		Vector2 newLocation = new Vector2(transform.position.x + randomX, transform.position.y + randomY);
    		Vector2 directionVector = newLocation - new Vector2(transform.position.x, transform.position.y);

    		RaycastHit2D CheckWall = Physics2D.Raycast(transform.position,
                                                  directionVector,
                                                  directionVector.magnitude,
                                                  detectWall);
        	if (CheckWall.collider == null) {
        		attempts = 0;
            	transform.position = newLocation;
        	} else {
                attempts -= 1;
            }

    	}

    }

	//--------------------------------------------------------------------------------

	public override void Attack() {
		if (attackType == "Attack1") {
			StartCoroutine(Attack1());
        } else if (attackType == "Attack2") {
        	StartCoroutine(Attack2());
        }
    }

    IEnumerator Attack1() {
    	yield return new WaitForSeconds(2f);

    	CultistProjectile top = Instantiate(projectile);
		CultistProjectile middle = Instantiate(projectile);
		CultistProjectile bottom = Instantiate(projectile);

		Vector2 directionVector = playerLocation - transform.position;

		float angle;
		if (playerLocation.y >= transform.position.y) {
			angle = Vector2.Angle(Vector2.right, directionVector);
		} else {
			angle = -Vector2.Angle(Vector2.right, directionVector);
		}

        middle.GetComponent<Transform>().position = transform.position;
        middle.direction = directionVector;

        top.GetComponent<Transform>().position = transform.position;
        top.direction = new Vector2(Mathf.Cos((angle + 30) * Mathf.Deg2Rad), Mathf.Sin((angle + 30) * Mathf.Deg2Rad));

        bottom.GetComponent<Transform>().position = transform.position;
        bottom.direction = new Vector2(Mathf.Cos((angle - 30) * Mathf.Deg2Rad), Mathf.Sin((angle - 30) * Mathf.Deg2Rad));

    }

    IEnumerator Attack2() {
    	yield return new WaitForSeconds(2f);
    	for (int i = 0; i < 5; i += 1) {
			CultistProjectile thisProjectile = Instantiate(projectile);
        	thisProjectile.transform.position = transform.position;
        	thisProjectile.direction = (playerLocation - transform.position).normalized;
        	yield return new WaitForSeconds(0.1f);
        }
    }

    //--------------------------------------------------------------------------------

    public override void UpdateAnimation() {

        if (playerLocation.x < transform.position.x) {
            currentState = "Left";
        } else {
            currentState = "Right";
        }

        string stateName = "Cultist" + currentState;

        if(!animator.GetCurrentAnimatorStateInfo(0).IsName(stateName)) {
            animator.Play(stateName, 0);
        }

    }

    //--------------------------------------------------------------------------------

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy {

    //--------------------------------------------------------------------------------

    public override void SpecificUpdate() {

    	Move();

    }

    public override void Move() {

    	Vector3 direction = playerLocation - transform.position;
    	direction = direction.normalized;
    	rigidbody2D.velocity = direction * speed;

    }

    public override void Attack() {

    }

    //--------------------------------------------------------------------------------
}

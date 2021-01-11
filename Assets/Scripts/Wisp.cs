using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wisp : Enemy {

    //--------------------------------------------------------------------------------

    public override void SpecificUpdate() {

    	Move();

    }

    public override void Move() {

    	Vector3 direction = playerLocation - transform.position;
    	direction = direction.normalized;
    	transform.Translate(direction * speed * Time.deltaTime);

    }

    public override void Attack() {

    }

    //--------------------------------------------------------------------------------
}
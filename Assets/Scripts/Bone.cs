using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bone : EnemyProjectile {

	//--------------------------------------------------------------------------------

    public override void SpecificUpdate() {

    	transform.Rotate(new Vector3(0, 0, 1f), Time.deltaTime * 200f, Space.Self);

    }

    //--------------------------------------------------------------------------------

}

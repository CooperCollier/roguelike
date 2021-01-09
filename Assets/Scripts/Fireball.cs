using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Spell {

	//--------------------------------------------------------------------------------

	public override void SpecificUpdate() {

    }

    //--------------------------------------------------------------------------------

    public override void Despawn() {

    	Destroy(gameObject);

    }

    //--------------------------------------------------------------------------------

}


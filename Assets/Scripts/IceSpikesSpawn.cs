using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpikesSpawn : Spell {

	//--------------------------------------------------------------------------------

	[SerializeField]
    public Spell iceSpikes;

	//--------------------------------------------------------------------------------

	public override void SpecificUpdate() {

		Spell top = Instantiate(iceSpikes);
		Spell middle = Instantiate(iceSpikes);
		Spell bottom = Instantiate(iceSpikes);

		float angle = 0f;

		if (direction == Vector2.up) {
			angle = 60f;
		} else if (direction == Vector2.down) {
			angle = 240f;
		} else if (direction == Vector2.left) {
			angle = 150f;
		} else if (direction == Vector2.right) {
			angle = 330f;
		}

		middle.GetComponent<Transform>().position = transform.position;
        middle.direction = direction;

        top.GetComponent<Transform>().position = transform.position;
        top.direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));

        bottom.GetComponent<Transform>().position = transform.position;
        bottom.direction = new Vector2(Mathf.Cos((angle + 60) * Mathf.Deg2Rad), Mathf.Sin((angle + 60) * Mathf.Deg2Rad));

		Despawn();

    }

    //--------------------------------------------------------------------------------

}


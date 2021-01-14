using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scroll : Item {

	//--------------------------------------------------------------------------------

	[SerializeField]
	public Spell spell;

	//--------------------------------------------------------------------------------

	public override string GetItemType() { return "Scroll"; }

	//--------------------------------------------------------------------------------

	void Start() {

		Image source = spell.GetComponent<Image>();
    	SpriteRenderer target = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
    	target.sprite = source.sprite;

	}

	//--------------------------------------------------------------------------------

	public Spell GetSpell() { return spell; }

	//--------------------------------------------------------------------------------

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestBlue : Chest {

    //--------------------------------------------------------------------------------

    [SerializeField]
    public Spell fireball;
    [SerializeField]
    public Spell forceField;
    [SerializeField]
    public Spell spear;
    [SerializeField]
    public Spell iceSpikesSpawn;
    // More prefabs for spells here...

    //--------------------------------------------------------------------------------

    public override void UpdateAnimation() {

        string animationName;

        if (opened) {
            animationName = "ChestBlueOpen";
        } else {
            animationName = "ChestBlueClosed";
        }

        if(!animator.GetCurrentAnimatorStateInfo(0).IsName(animationName)) {
            animator.Play(animationName, 0);
        }

    }

    //--------------------------------------------------------------------------------

    public override int GenerateCoins() {
    	return Random.Range(15, 25);
    }

    public override int GenerateApples() {
    	float rand = Random.value;
    	if (rand > 0.5f) {
    		return 1;
    	} else if (rand > 0.25f) {
    		return 2;
    	} else {
    		return 0;
    	}
    }

    public override bool GenerateCoffee() {
    	if (Random.value > 0.6f) {
    		return true;
    	} else {
    		return false;
    	}
    }

    public override bool GenerateGoldApple() {
    	return false;
    }

    public override bool GenerateBook() {
    	if (Random.value > 0.75f) {
    		return true;
    	} else {
    		return false;
    	}
    }

    public override Spell GenerateScroll() {

        Spell[] spells = { null, null, null, null };

        spells[0] = fireball;
        spells[1] = forceField;
        spells[2] = spear;
        spells[3] = iceSpikesSpawn;

    	int numberOfSpells = 4;
    	int index = Random.Range(0, numberOfSpells);
    	return spells[index];
    }

    //--------------------------------------------------------------------------------

}
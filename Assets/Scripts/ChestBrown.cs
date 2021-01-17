using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestBrown : Chest {

    //--------------------------------------------------------------------------------

    public override void UpdateAnimation() {

        string animationName;

        if (opened) {
            animationName = "ChestBrownOpen";
        } else {
            animationName = "ChestBrownClosed";
        }

        if(!animator.GetCurrentAnimatorStateInfo(0).IsName(animationName)) {
            animator.Play(animationName, 0);
        }

    }

    //--------------------------------------------------------------------------------

    public override int GenerateCoins() {
    	return Random.Range(5, 15);
    }

    public override int GenerateApples() {
    	if (Random.value > 0.6f) {
    		return 1;
    	} else {
    		return 0;
    	}
    }

    public override bool GenerateCoffee() {
    	if (Random.value > 0.9f) {
    		return true;
    	} else {
    		return false;
    	}
    }

    public override bool GenerateGoldApple() {
    	return false;
    }

    public override bool GenerateBook() {
    	return false;
    }

    public override Spell GenerateScroll() {
    	return null;
    }

    //--------------------------------------------------------------------------------

}

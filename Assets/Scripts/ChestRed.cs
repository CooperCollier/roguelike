using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestRed : Chest {

    //--------------------------------------------------------------------------------

    public override void UpdateAnimation() {

        string animationName;

        if (opened) {
            animationName = "ChestRedOpen";
        } else {
            animationName = "ChestRedClosed";
        }

        if(!animator.GetCurrentAnimatorStateInfo(0).IsName(animationName)) {
            animator.Play(animationName, 0);
        }

    }

    //--------------------------------------------------------------------------------

    public override int GenerateCoins() {
    	return Random.Range(10, 20);
    }

    public override int GenerateApples() {
    	float rand = Random.value;
    	if (rand > 0.5f) {
    		return 1;
    	} else if (rand < 0.15f) {
    		return 2;
    	} else {
    		return 0;
    	}
    }

    public override bool GenerateCoffee() {
    	if (Random.value > 0.7f) {
    		return true;
    	} else {
    		return false;
    	}
    }

    public override bool GenerateGoldApple() {
    	return false;
    }

    public override bool GenerateBook() {
    	if (Random.value > 0.4f) {
    		return true;
    	} else {
    		return false;
    	}
    }

    public override Spell GenerateScroll() {
    	return null;
    }

    //--------------------------------------------------------------------------------

}
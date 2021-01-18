using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestGold : Chest {

    //--------------------------------------------------------------------------------

    public override void UpdateAnimation() {

        string animationName;

        if (opened) {
            animationName = "ChestGoldOpen";
        } else {
            animationName = "ChestGoldClosed";
        }

        if(!animator.GetCurrentAnimatorStateInfo(0).IsName(animationName)) {
            animator.Play(animationName, 0);
        }

    }

    //--------------------------------------------------------------------------------

    public override int GenerateCoins() {
    	return Random.Range(30, 40);
    }

    public override int GenerateApples() {
    	return 0;
    }

    public override bool GenerateCoffee() {
    	return false;
    }

    public override bool GenerateGoldApple() {
    	return true;
    }

    public override bool GenerateBook() {
    	return false;
    }

    public override Spell GenerateScroll() {
    	return null;
    }

    //--------------------------------------------------------------------------------

}
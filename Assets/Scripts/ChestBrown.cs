using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestBrown : Chest {

    //--------------------------------------------------------------------------------

    void Start() {
        
    }

    //--------------------------------------------------------------------------------

    void Update() {
        
    }

    //--------------------------------------------------------------------------------

    public override int GenerateCoins() {
    	return Random.Range(10, 15);
    }

    public override int GenerateApples() {
    	if (Random.value > 0.5f) {
    		return 1;
    	} else {
    		return 0;
    	}
    }

    public override bool GenerateCoffee() {
    	if (Random.value > 0.75f) {
    		return true;
    	} else {
    		return false;
    	}
    }

    public override bool GenerateGoldenApple() {
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

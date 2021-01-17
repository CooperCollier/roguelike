using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Chest : MonoBehaviour {

	//--------------------------------------------------------------------------------

	bool opened;

    //--------------------------------------------------------------------------------

    void Start() {
    	opened = false;
    }

    //--------------------------------------------------------------------------------

    void Update() {
        
    }

    //--------------------------------------------------------------------------------

    public abstract int GenerateCoins();

    public abstract int GenerateApples();

    public abstract bool GenerateCoffee();

    public abstract bool GenerateGoldenApple();

    public abstract bool GenerateBook();

    public abstract Spell GenerateScroll();

    //--------------------------------------------------------------------------------

}

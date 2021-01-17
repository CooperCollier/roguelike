using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Chest : MonoBehaviour {

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

    [SerializeField]
    public Coin coin;
    [SerializeField]
    public Apple apple;
    [SerializeField]
    public GoldApple goldApple;
    [SerializeField]
    public Book book;
    [SerializeField]
    public Coffee coffee;
    // More items here...

    [SerializeField]
    public Scroll scroll;

    public Animator animator;

	public bool opened;

	public static Spell[] spells = { null, null, null, null };

    //--------------------------------------------------------------------------------

    void Start() { 

    	opened = false;

    	animator = GetComponent<Animator>();

    	spells[0] = fireball;
        spells[1] = forceField;
        spells[2] = spear;
        spells[3] = iceSpikesSpawn;

    }

    //--------------------------------------------------------------------------------

    void Update() { UpdateAnimation(); }

    //--------------------------------------------------------------------------------

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Spell" && !opened) {
        	opened = true;
        	SpawnLoot();
        }
    }

    void SpawnLoot() {

    	int numberOfCoins = GenerateCoins();
    	int numberOfApples = GenerateApples();

    	for (int i = 0; i < numberOfCoins; i += 1) { 
    		Coin newCoin = Instantiate(coin); 
    		newCoin.transform.position = transform.position + 
    		new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0);
    	}

    	for (int i = 0; i < numberOfApples; i += 1) {
    		Apple newApple = Instantiate(apple); 
    		newApple.transform.position = transform.position + 
    		new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0);
    	}

    	if (GenerateCoffee()) {
    		Coffee newCoffee = Instantiate(coffee); 
    		newCoffee.transform.position = transform.position + 
    		new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0);
    	}

    	if (GenerateGoldApple()) {
    		GoldApple newGoldApple = Instantiate(goldApple); 
    		newGoldApple.transform.position = transform.position + 
    		new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0);
    	}

    	if (GenerateBook()) {
    		Book newBook = Instantiate(book); 
    		newBook.transform.position = transform.position + 
    		new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0);
    	}

    	if (GenerateScroll()) {
    		Spell newSpell = spells[Random.Range(0, 3)];
    		Scroll newScroll = Instantiate(scroll);
    		newScroll.spell = newSpell;
    		newScroll.transform.position = transform.position + 
    		new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0);
    	}

    }

    //--------------------------------------------------------------------------------

    public abstract void UpdateAnimation();

    public abstract int GenerateCoins();

    public abstract int GenerateApples();

    public abstract bool GenerateCoffee();

    public abstract bool GenerateGoldApple();

    public abstract bool GenerateBook();

    public abstract Spell GenerateScroll();

    //--------------------------------------------------------------------------------

}

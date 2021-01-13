using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	//--------------------------------------------------------------------------------

    [SerializeField]
    public Spell magicMissile;
    [SerializeField]
    public Spell fireball;
    [SerializeField]
    public Spell forceField;
    [SerializeField]
    public Spell spear;
    // More prefabs for spells here...

    Rigidbody2D rigidbody2D;
    Animator animator;
    SpriteRenderer spriteRenderer;
    // More componenets here...

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
	public float speed;
    [SerializeField]
	public float dashSpeed;

    [SerializeField]
	public float maxDashTime;
	public float dashTime;

    [SerializeField]
	public float maxAttkTime;
	public float attkTime;

    [SerializeField]
    public int maxHealth;
    public int health;

    [SerializeField]
    public float maxMana;
    public float mana;

    public static int coins;
    public static bool alive;

	public enum State {IdleLeft, IdleRight, IdleUp, IdleDown, MoveLeft, MoveRight, MoveUp, MoveDown};
	public static State currentState;

    public static Spell[] spells = { null, null, null, null };
    public static int selectedSpell = 0;

    [SerializeField]
    public float maxInvincibilityTime;
    public float invincibilityTime;

    [SerializeField]
    public float manaRecoveryRate;

    [SerializeField]
    public float dashManaCost;

    public bool coffeeActive;

    public static Vector2 attkDirection = Vector2.zero;

    //--------------------------------------------------------------------------------

    void Start() {

        health = maxHealth;
        mana = maxMana;
        coins = 0;
        alive = true;

    	currentState = State.IdleRight;

        spells[0] = magicMissile;
        spells[1] = fireball;
        spells[2] = forceField;
        spells[3] = spear;

        coffeeActive = false;

    	rigidbody2D = GetComponent<Rigidbody2D>();
    	animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    //--------------------------------------------------------------------------------

    void Update() {

        if (health <= 0) { alive = false; }
    	if (!alive || (Time.timeScale == 0f) ) {return;}

    	Move();

    	Dash();

    	Attack();

        ChangeSpell();

    	UpdateAnimation();

        RecoverMana();

        DecrementInvincibilityTime();
        
    }

    //--------------------------------------------------------------------------------

    void Move() {

    	if (Input.GetKey(KeyCode.W)) {
    		transform.Translate(Vector2.up * speed * Time.deltaTime);
    		currentState = State.MoveUp;
    	} 
    	if (Input.GetKey(KeyCode.S)) {
    		transform.Translate(Vector2.down * speed * Time.deltaTime);
    		currentState = State.MoveDown;
    	} 
    	if (Input.GetKey(KeyCode.A)) {
    		transform.Translate(Vector2.left * speed * Time.deltaTime);
    		currentState = State.MoveLeft;
    	} 
    	if (Input.GetKey(KeyCode.D)) {
    		transform.Translate(Vector2.right * speed * Time.deltaTime);
    		currentState = State.MoveRight;
    	}

    	if (Input.GetKeyUp(KeyCode.W)) {
    		currentState = State.IdleUp;
    	} 
    	if (Input.GetKeyUp(KeyCode.S)) {
    		currentState = State.IdleDown;
    	} 
    	if (Input.GetKeyUp(KeyCode.A)) {
    		currentState = State.IdleLeft;
    	} 
    	if (Input.GetKeyUp(KeyCode.D)) {
    		currentState = State.IdleRight;
    	}

    }

    //--------------------------------------------------------------------------------

    void Dash() {

    	if (Input.GetKeyDown(KeyCode.M) && dashTime == 0 && mana > dashManaCost) {

    		if (currentState == State.MoveUp || currentState == State.IdleUp) {
    			currentState = State.MoveUp;
    			rigidbody2D.velocity = Vector2.up * dashSpeed;
    		} 
    		if (currentState == State.MoveDown || currentState == State.IdleDown) {
    			currentState = State.MoveDown;
    			rigidbody2D.velocity = Vector2.down * dashSpeed;
    		} 
    		if (currentState == State.MoveLeft || currentState == State.IdleLeft) {
    			currentState = State.MoveLeft;
    			rigidbody2D.velocity = Vector2.left * dashSpeed;
    		} 
    		if (currentState == State.MoveRight || currentState == State.IdleRight) {
    			currentState = State.MoveRight;
    			rigidbody2D.velocity = Vector2.right * dashSpeed;
    		}

    		dashTime = maxDashTime;

            mana -= dashManaCost;

    	} else if (dashTime > 0) {
    		dashTime -= Time.deltaTime;
    	} else if (dashTime <= 0) {
    		rigidbody2D.velocity = Vector2.zero;
    		dashTime = 0;
    	}

    }

    //--------------------------------------------------------------------------------

    void Attack() {

    	if (Input.GetKeyDown(KeyCode.Space) && attkTime == 0) {

            if (spells[selectedSpell] == null) { return; }

            int manaCost = spells[selectedSpell].manaCost;
            if (mana < manaCost) {
                return;
            } else {
                mana -= manaCost;
            }

    		if (currentState == State.MoveUp || currentState == State.IdleUp) {
    			attkDirection = Vector2.up;
    		} 
    		if (currentState == State.MoveDown || currentState == State.IdleDown) {
    			attkDirection = Vector2.down;
    		} 
    		if (currentState == State.MoveLeft || currentState == State.IdleLeft) {
    			attkDirection = Vector2.left;
    		} 
    		if (currentState == State.MoveRight || currentState == State.IdleRight) {
    			attkDirection = Vector2.right;
    		}

            attkTime = maxAttkTime;

            Spell newSpell = (Spell) Instantiate(spells[selectedSpell]);

            Vector3 offset = attkDirection * 0.1f;

            newSpell.GetComponent<Transform>().position = transform.position + offset;
            newSpell.direction = attkDirection;

    	} else if (attkTime > 0) {
    		attkTime -= Time.deltaTime;
    	} else if (attkTime <= 0) {
    		attkDirection = Vector2.zero;
    		attkTime = 0;
    	}

    }

    //--------------------------------------------------------------------------------

    void ChangeSpell() {

        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            if (selectedSpell == 0) {
                selectedSpell = 3;
            } else {
                selectedSpell -= 1;
            }
        } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            if (selectedSpell == 3) {
                selectedSpell = 0;
            } else {
                selectedSpell += 1;
            }
        }
    }

    //--------------------------------------------------------------------------------

    void UpdateAnimation() {

    	string stateName;

    	if (attkDirection == Vector2.up) {
    		stateName = "AttkUp";
    	} else if (attkDirection == Vector2.down) {
    		stateName = "AttkDown";
    	} else if (attkDirection == Vector2.left) {
    		stateName = "AttkLeft";
    	} else if (attkDirection == Vector2.right) {
    		stateName = "AttkRight";
    	} else {
    		stateName = currentState.ToString();
    	}

    	if(!animator.GetCurrentAnimatorStateInfo(0).IsName(stateName)) {
    		animator.Play(stateName, 0);
    	}

    }

    //--------------------------------------------------------------------------------

    void RecoverMana() {
        if (mana >= maxMana) {
            mana = maxMana;
        } else {
            mana += Time.deltaTime * manaRecoveryRate;
        }
    }

    void DecrementInvincibilityTime() {
        if (invincibilityTime <= 0) {
            invincibilityTime = 0;
            spriteRenderer.color = Color.white;
        } else {
            invincibilityTime -= Time.deltaTime;
        }
    }

    //--------------------------------------------------------------------------------

    public void TakeDamage(int damage) {

        if (invincibilityTime > 0 || dashTime > 0) { return; }
        spriteRenderer.color = Color.red;
        if (coffeeActive) { EndCoffee(); }
        health -= damage;
        if (health < 0) { 
            health = 0; 
        } else {
            invincibilityTime = maxInvincibilityTime;
        }

    }

    //--------------------------------------------------------------------------------

    void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject.tag == "Item") {

            Item item = (Item) collision.gameObject.GetComponent<Item>();

            if (item.GetItemType() == "Coin") {
                GetCoin();
            }
            if (item.GetItemType() == "Apple") {
                GetApple();
            }
            if (item.GetItemType() == "GoldApple") {
                GetGoldApple();
            }
            if (item.GetItemType() == "Coffee") {
                if (!coffee) { StartCoffee(); }
            }
            if (item.GetItemType() == "Book") {
                GetBook();
            }

            Destroy(collision.gameObject);

        }

    }

    //--------------------------------------------------------------------------------

    public void GetCoin() {
        coins += 1;
    }

    public void GetApple() {
        health += 20;
        if (health > maxHealth) { health = maxHealth; }
    }

    public void GetGoldApple() {
        health = maxHealth;
    }

    public void GetBook() {
        Debug.Log("Got a book!");
    }

    public void StartCoffee() {
        coffeeActive = true;
        speed *= 1.5f;
        dashSpeed *= 1.5f;
        manaRecoveryRate *= 2f;
    }

    public void EndCoffee() {

        coffeeActive = false;
        speed /= 1.5f;
        dashSpeed /= 1.5f;
        manaRecoveryRate /= 2f;
        
    }

    //--------------------------------------------------------------------------------

    public int ReportHealth() { return health; }

    public float ReportMana() { return mana; }

    public int ReportCoins() { return coins; }

    public bool ReportAlive() { return alive; }

    public int ReportSelectedSpell() { return selectedSpell; }

    public Spell[] ReportSpells() { return spells; }

    public Vector3 ReportLocation() { return transform.position; }

    //--------------------------------------------------------------------------------
    
}

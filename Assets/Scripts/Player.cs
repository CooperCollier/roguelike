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
    [SerializeField]
    public Spell iceSpikesSpawn;
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

	public enum State {IdleLeft, IdleRight, IdleUp, IdleDown, 
                       MoveLeft, MoveRight, MoveUp, MoveDown};
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

    public static Vector2 direction = Vector2.zero;

    public Vector2 deathPosition = Vector2.zero;

    //--------------------------------------------------------------------------------

    void Start() {

        health = maxHealth;
        mana = maxMana;
        coins = 0;
        alive = true;

    	currentState = State.IdleRight;

        spells[0] = magicMissile;
        spells[1] = null;
        spells[2] = null;
        spells[3] = null;

        coffeeActive = false;

    	rigidbody2D = GetComponent<Rigidbody2D>();
    	animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    //--------------------------------------------------------------------------------

    void Update() {

    	if (Time.timeScale == 0f) {
    		return;
    	} else if (!alive) {
    		transform.position = deathPosition;
    		return;
    	} else if (health <= 0) { 
    		Die();
    		return;
    	}

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

    	if (dashTime > 0) { return; }

    	if (Input.GetKey(KeyCode.W)) {
    		rigidbody2D.velocity = new Vector2(0, speed);
    		currentState = State.MoveUp;
    	} else if (Input.GetKey(KeyCode.S)) {
    		rigidbody2D.velocity = new Vector2(0, -speed);
    		currentState = State.MoveDown;
    	} else if (Input.GetKey(KeyCode.A)) {
    		rigidbody2D.velocity = new Vector2(-speed, 0);
    		currentState = State.MoveLeft;
    	} else if (Input.GetKey(KeyCode.D)) {
    		rigidbody2D.velocity = new Vector2(speed, 0);
    		currentState = State.MoveRight;
    	} else {
    		rigidbody2D.velocity = Vector2.zero;
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
    			direction = Vector2.up;
    		} 
    		if (currentState == State.MoveDown || currentState == State.IdleDown) {
    			direction = Vector2.down;
    		} 
    		if (currentState == State.MoveLeft || currentState == State.IdleLeft) {
    			direction = Vector2.left;
    		} 
    		if (currentState == State.MoveRight || currentState == State.IdleRight) {
    			direction = Vector2.right;
    		}

            attkTime = maxAttkTime;

            Spell newSpell = (Spell) Instantiate(spells[selectedSpell]);

            newSpell.SetPositionAndDirection(transform.position, direction);

    	} else if (attkTime > 0) {
    		attkTime -= Time.deltaTime;
    	} else if (attkTime <= 0) {
    		direction = Vector2.zero;
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

    	string animationName;

    	if (!alive && direction == Vector2.left) {
    		animationName = "DeathLeft";
    	} else if (!alive) {
    		animationName = "DeathRight";
    	} else if (direction == Vector2.up) {
    		animationName = "AttkUp";
    	} else if (direction == Vector2.down) {
    		animationName = "AttkDown";
    	} else if (direction == Vector2.left) {
    		animationName = "AttkLeft";
    	} else if (direction == Vector2.right) {
    		animationName = "AttkRight";
    	} else {
    		animationName = currentState.ToString();
    	}

    	if(!animator.GetCurrentAnimatorStateInfo(0).IsName(animationName)) {
    		animator.Play(animationName, 0);
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

    public void Die() {
    	alive = false;
    	rigidbody2D.velocity = Vector2.zero;
    	deathPosition = transform.position;
    	UpdateAnimation();
    }

    //--------------------------------------------------------------------------------

    void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject.tag == "Item") {

            Item item = (Item) collision.gameObject.GetComponent<Item>();

            bool pickedUpItem = false;

            if (item.GetItemType() == "Coin") {
                pickedUpItem = GetCoin();
            } else if (item.GetItemType() == "Apple") {
                pickedUpItem = GetApple();
            } else if (item.GetItemType() == "GoldApple") {
                pickedUpItem = GetGoldApple();
            } else if (item.GetItemType() == "Coffee") {
                pickedUpItem = StartCoffee();
            } else if (item.GetItemType() == "Book") {
                pickedUpItem = GetBook();
            } else if (item.GetItemType() == "Scroll") {
                pickedUpItem = GetScroll(((Scroll) item).GetSpell());
            }

            if (pickedUpItem) { Destroy(collision.gameObject); }

        }

    }

    //--------------------------------------------------------------------------------

    public bool GetCoin() {
        coins += 1;
        return true;
    }

    public bool GetApple() {
        health += 20;
        if (health > maxHealth) { health = maxHealth; }
        return true;
    }

    public bool GetGoldApple() {
        health = maxHealth;
        return true;
    }

    public bool GetBook() {
        return true;
    }

    public bool StartCoffee() {
        if (coffeeActive) { return true; }
        coffeeActive = true;
        speed *= 1.3f;
        dashSpeed *= 1.3f;
        manaRecoveryRate *= 2f;
        return true;
    }

    public void EndCoffee() {
        coffeeActive = false;
        speed /= 1.3f;
        dashSpeed /= 1.3f;
        manaRecoveryRate /= 2f;
    }

    public bool GetScroll(Spell spell) {

        for (int i = 0; i < spells.Length; i += 1) {
            if (spells[i] == null) {
                continue;
            } else if (spells[i].GetName() == spell.GetName()) {
                return false;
            }
        }

        for (int i = 0; i < spells.Length; i += 1) {
            if (spells[i] == null) {
                spells[i] = spell;
                return true;
            }
        }

        return false;

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {

    //--------------------------------------------------------------------------------

    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rigidbody2D;
    public Animator animator;

    public float maxDamageTime = 0.1f;
    public float damageTime = 0f;

    public float maxPauseTime = 0.25f;
    public float pauseTime = 0f;

    public float maxWaitTime = 1f;
    public float waitTime = 0f;

	public static Player player;

    public Coin coin;

    [SerializeField]
    public float speed;

    [SerializeField]
    public int attack;

    [SerializeField]
    public int reward;

    [SerializeField]
    public int aggroRadius;

    [SerializeField]
    public int maxHealth;
    public int health;

    public string currentAnimation;

    public Vector3 playerLocation;

    //--------------------------------------------------------------------------------

    void Start() {

        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    	player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        maxDamageTime = 0.1f;
        maxPauseTime = 0.25f;
        maxWaitTime = 0.1f;

    	health = maxHealth;
        
    }

    //--------------------------------------------------------------------------------

    void Update() {

    	if (health <= 0) { Despawn(); }

        if (damageTime < 0f) {
            spriteRenderer.color = Color.white;
            rigidbody2D.velocity = Vector2.zero;
            damageTime = 0f;
        } else if (damageTime > 0f) {
            damageTime -= Time.deltaTime;
        }

        if (pauseTime < 0f) {
            pauseTime = 0f;
        } else if (pauseTime > 0f) {
            pauseTime -= Time.deltaTime;
            return;
        }

        if (waitTime <= 0f) {
            waitTime = maxWaitTime;
            rigidbody2D.velocity /= 2f;
        } else {
            waitTime -= Time.deltaTime;
        }

        playerLocation = player.ReportLocation();

        if ((playerLocation - transform.position).magnitude < aggroRadius) {
            SpecificUpdate();
        }
        
    }

    //--------------------------------------------------------------------------------

    public abstract void SpecificUpdate();

    public abstract void Move();

    public abstract void Attack();

    public abstract void UpdateAnimation();

    //--------------------------------------------------------------------------------

    public void TakeDamage(int damage) {

    	spriteRenderer.color = Color.red;
        damageTime = maxDamageTime;
    	health -= damage;
    	if (health < 0) { health = 0; }

    }

    //--------------------------------------------------------------------------------

    public void Despawn() {
    	// Play death animation
    	for (int i = 0; i < reward; i += 1) { 
    		Coin newCoin = Instantiate(coin);
    		newCoin.transform.position = transform.position;
    	}
    	Destroy(gameObject);
    }

    //--------------------------------------------------------------------------------

    public void Push(Vector2 direction) {
        rigidbody2D.AddForce(direction, ForceMode2D.Impulse);
    }

    //--------------------------------------------------------------------------------

    void OnTriggerStay2D(Collider2D other) {
    	if (other.gameObject.tag == "Enemy") {
    		Vector2 direction = transform.position - other.gameObject.transform.position;
    		transform.Translate(direction.normalized * 0.01f);
    	}
    }

    //--------------------------------------------------------------------------------

}

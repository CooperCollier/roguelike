using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {

    //--------------------------------------------------------------------------------

    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rigidbody2D;
    public Animator animator;

    public float maxFlashRedTime = 0.1f;
    public float flashRedTime = 0f;

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

        maxFlashRedTime = 0.1f;

    	health = maxHealth;

        float speedOffset = Random.Range(0.9f, 1.1f);
        speed *= speedOffset;
        
    }

    //--------------------------------------------------------------------------------

    void Update() {

    	if (health <= 0) { Despawn(); }

        if (flashRedTime < 0f) {
            spriteRenderer.color = Color.white;
            rigidbody2D.velocity = Vector2.zero;
            flashRedTime = 0f;
        } else if (flashRedTime > 0f) {
            flashRedTime -= Time.deltaTime;
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
        flashRedTime = maxFlashRedTime;
    	health -= damage;
    	if (health < 0) { health = 0; }

    }

    //--------------------------------------------------------------------------------

    public void KnockBack(Vector2 direction) {
        rigidbody2D.AddForce(direction, ForceMode2D.Impulse);
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

}

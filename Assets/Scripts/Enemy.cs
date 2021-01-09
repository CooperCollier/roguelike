using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {

    //--------------------------------------------------------------------------------

	public static Player player;

    [SerializeField]
    public float speed;

    [SerializeField]
    public int attack;

    [SerializeField]
    public int maxHealth;
    public int health;

    public Vector3 playerLocation;

    //--------------------------------------------------------------------------------

    void Start() {

    	player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    	health = maxHealth;
        
    }

    //--------------------------------------------------------------------------------

    void Update() {

    	if (health <= 0) { Despawn(); }

    	playerLocation = player.ReportLocation();

    	SpecificUpdate();
        
    }

    public abstract void SpecificUpdate();

    public abstract void Move();

    public abstract void Attack();

    //--------------------------------------------------------------------------------

    public void TakeDamage(int damage) {

    	// flash the sprite red
    	health -= damage;
    	if (health < 0) { health = 0; }

    }

    public void Despawn() {

    	// Play death animation
    	Destroy(gameObject);
    	
    }

    //--------------------------------------------------------------------------------

    void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            collision.gameObject.SendMessage("TakeDamage", attack); // Change This
        }
    }

    //--------------------------------------------------------------------------------

}

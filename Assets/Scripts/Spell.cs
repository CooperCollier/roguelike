using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : MonoBehaviour {

    //--------------------------------------------------------------------------------

    [SerializeField]
    public int damage;

    [SerializeField]
    public float speed;

    [SerializeField]
    public int manaCost;

    [SerializeField]
    public float knockback;

    [SerializeField]
    float maxTimeToLive;
    float timeToLive;

    public Vector2 direction = Vector2.zero;

    //--------------------------------------------------------------------------------

    void Start() {

    	timeToLive = maxTimeToLive;
    	
    }

    //--------------------------------------------------------------------------------

    void Update() {

    	timeToLive -= Time.deltaTime;
        if (timeToLive <= 0) { Despawn(); }

        transform.Translate(direction * speed * Time.deltaTime);

        SpecificUpdate();
        
    }

    public abstract void SpecificUpdate();

    //--------------------------------------------------------------------------------

    public void Attack (GameObject enemy) {
        enemy.SendMessage("TakeDamage", damage); // change this
        Vector2 directionToEnemy = (enemy.transform.position - transform.position).normalized;
        enemy.SendMessage("Push", directionToEnemy * knockback);
    }

    public void Despawn() {
        // Play particle effect
        Destroy(gameObject);
    }

    //--------------------------------------------------------------------------------

}
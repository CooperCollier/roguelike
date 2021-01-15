using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyProjectile : MonoBehaviour {

    //--------------------------------------------------------------------------------

    [SerializeField]
    public int damage;

    [SerializeField]
    public float speed;

    [SerializeField]
    float maxTimeToLive;
    float timeToLive;

    public Vector2 direction = Vector2.zero;

    //--------------------------------------------------------------------------------

    void Start() {

    	timeToLive = maxTimeToLive;
    	
    }

    //--------------------------------------------------------------------------------

    /* https://forum.unity.com/threads/rotate-a-2d-object.520750/ */
    public static Quaternion LookAt2D(Vector2 forward) {
        return Quaternion.Euler(0, 0, Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg);
    }

    void Update() {

    	transform.rotation = LookAt2D(direction);

    	timeToLive -= Time.deltaTime;
        if (timeToLive <= 0) { Despawn(); }

        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        SpecificUpdate();
        
    }

    public abstract void SpecificUpdate();

    //--------------------------------------------------------------------------------

    public void Attack (GameObject player) {
        player.SendMessage("TakeDamage", damage); // change this
    }

    public void Despawn() {
        // Play particle effect
        Destroy(gameObject);
    }

    //--------------------------------------------------------------------------------

}
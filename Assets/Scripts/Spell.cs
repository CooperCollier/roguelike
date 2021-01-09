using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : MonoBehaviour {

    //--------------------------------------------------------------------------------

    [SerializeField]
    int damage;

    [SerializeField]
    float speed;

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

    //--------------------------------------------------------------------------------

    public abstract void SpecificUpdate();

    public abstract void Despawn();

    //--------------------------------------------------------------------------------


}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	//--------------------------------------------------------------------------------

	public static int health = 100;
	public static int mana = 100;
	public static int coins = 0;
	public static bool alive = true;

	private float speed = 1f;
	private float dashSpeed = 3f;

	private float maxDashTime = 0.3f;
	private float dashTime;

	private float maxAttkTime = 0.2f;
	private float attkTime;

	public static Vector2 attkDirection = Vector2.zero;

	public enum State {IdleLeft, IdleRight, IdleUp, IdleDown, 
					   MoveLeft, MoveRight, MoveUp, MoveDown}
	public static State currentState;

	Rigidbody2D rigidbody2D;
	Animator animator;

    //--------------------------------------------------------------------------------

    void Start() {

    	currentState = State.IdleRight;

    	rigidbody2D = GetComponent<Rigidbody2D>();
    	animator = GetComponent<Animator>();
        
    }

    //--------------------------------------------------------------------------------

    void Update() {

    	if (!alive) {return;}

    	Move();

    	Dash();

    	Attack();

    	UpdateAnimation();
        
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

    	if (Input.GetKeyDown(KeyCode.M) && dashTime == 0) {

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

    	} else if (attkTime > 0) {
    		attkTime -= Time.deltaTime;
    	} else if (attkTime <= 0) {
    		attkDirection = Vector2.zero;
    		attkTime = 0;
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
    
}

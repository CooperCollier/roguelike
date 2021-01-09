using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

	[SerializeField]
	public Player player;

	public int health;
	public float mana;
	public int coins;
	public bool alive;
	public int selectedSpell;

	public Slider healthBarSlider;
	public Slider manaBarSlider;

	public GameObject spellSlot0;
	public GameObject spellSlot1;
	public GameObject spellSlot2;
	public GameObject spellSlot3;
	public GameObject[] spellSlots;

    //--------------------------------------------------------------------------------

    void Start() {

    	alive = true;

    	healthBarSlider.maxValue = player.maxHealth;
    	manaBarSlider.maxValue = player.maxMana;

    	spellSlots = new GameObject[] {spellSlot0, spellSlot1, spellSlot2, spellSlot3};
        
    }

    //--------------------------------------------------------------------------------

    void Update() {

    	if (!alive) {return;}

    	health = player.ReportHealth();
    	mana = player.ReportMana();
    	coins = player.ReportCoins();
    	alive = player.ReportAlive();
    	selectedSpell = player.ReportSelectedSpell();

    	SetHealthBar();
    	SetManaBar();

    	UpdateSpellSlots();
        
    }

    //--------------------------------------------------------------------------------

    void UpdateSpellSlots() {
    	for (int i = 0; i < spellSlots.Length; i += 1) {
    		if (i == selectedSpell) {
    			spellSlots[selectedSpell].transform.GetChild(0).gameObject.SetActive(true);
    		} else {
    			spellSlots[selectedSpell].transform.GetChild(0).gameObject.SetActive(false);
    		}
    	}
    }

    //--------------------------------------------------------------------------------

    void SetHealthBar() { healthBarSlider.value = health; }

    void SetManaBar() { manaBarSlider.value = mana; }

    //--------------------------------------------------------------------------------

}

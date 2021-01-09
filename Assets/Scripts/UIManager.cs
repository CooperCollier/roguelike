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
	public Spell[] spells;

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
    	spells = player.ReportSpells();

    	SetHealthBar();
    	SetManaBar();
    	UpdateSpellSlots();
        
    }

    //--------------------------------------------------------------------------------

    void UpdateSpellSlots() {

    	for (int index = 0; index < spellSlots.Length; index += 1) {

    		if (selectedSpell == index) {
    			spellSlots[index].transform.GetChild(1).gameObject.SetActive(true);
    		} else {
    			spellSlots[index].transform.GetChild(1).gameObject.SetActive(false);
    		}

    		if (spells[index] == null) {
    			spellSlots[index].transform.GetChild(2).gameObject.SetActive(false);
    		} else {
    			spellSlots[index].transform.GetChild(2).gameObject.SetActive(true);
    			Image source = spells[index].GetComponent<Image>();
    			Image target = spellSlots[index].transform.GetChild(2).gameObject.GetComponent<Image>();
    			target.sprite = source.sprite;
    			target.SetNativeSize();
    		}

    	}

    }

    //--------------------------------------------------------------------------------

    void SetHealthBar() { healthBarSlider.value = health; }

    void SetManaBar() { manaBarSlider.value = mana; }

    //--------------------------------------------------------------------------------

}

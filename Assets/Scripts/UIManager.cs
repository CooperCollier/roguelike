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

    [SerializeField]
    public GameObject pauseButton;
    [SerializeField]
    public GameObject pauseScreen;
    [SerializeField]
    public GameObject deathScreen;
    [SerializeField]
    public GameObject coinsText;

    //--------------------------------------------------------------------------------

    void Awake() {

    	alive = true;
        Time.timeScale = 1f;

    	healthBarSlider.maxValue = player.maxHealth;
    	manaBarSlider.maxValue = player.maxMana;

    	spellSlots = new GameObject[] {spellSlot0, spellSlot1, spellSlot2, spellSlot3};

        pauseButton.SetActive(true);
        pauseScreen.SetActive(false);
        deathScreen.SetActive(false);
        
    }

    //--------------------------------------------------------------------------------

    void Update() {

    	if (!alive) { Die(); }
        
        if (Input.GetKey(KeyCode.Escape)) { Pause(); }

    	health = player.ReportHealth();
    	mana = player.ReportMana();
    	coins = player.ReportCoins();
    	alive = player.ReportAlive();
    	selectedSpell = player.ReportSelectedSpell();
    	spells = player.ReportSpells();

    	SetHealthBar();
    	SetManaBar();
    	UpdateSpellSlots();
        UpdateCoinsText();
        
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

    void UpdateCoinsText() {
        coinsText.GetComponent<Text>().text = ("Coins: " + coins.ToString());
    }

    //--------------------------------------------------------------------------------

    void Pause() {
        pauseButton.SetActive(false);
        pauseScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    void UnPause() {
        pauseButton.SetActive(true);
        pauseScreen.SetActive(false);
        Time.timeScale = 1f;
    }

    void Die() {
        pauseButton.SetActive(false);
        deathScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    void Menu() {
        SceneManager.LoadScene(0);
    }

    //--------------------------------------------------------------------------------

    void SetHealthBar() { healthBarSlider.value = health; }

    void SetManaBar() { manaBarSlider.value = mana; }

    //--------------------------------------------------------------------------------

}

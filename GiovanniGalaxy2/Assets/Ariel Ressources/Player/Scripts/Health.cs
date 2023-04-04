using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public bool isAlive = true;

    public float maxHealth = 100;
    public float health;

    public float maxArmor = 60;
    public float armor;

    private GameManager gm;
    private FlashScreenPostProcessing flashScreen;
    //--------------------s

    public GameObject DeathPanel;
    public float delayTime = 5f;
    public string sceneName = "Main Menu";


    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
        flashScreen = FindObjectOfType<FlashScreenPostProcessing>();
        health = maxHealth;
        armor = maxArmor;
    }
    public void Update()
    {

    }

    public void IsHit(float damage)
    {

        if (damage <= armor)
        {
            armor -= damage;
            //-----------------------s

        }

        //If not, reduce damage to how much damage done to armor, set
        //armor to 0 and lower health by remaining damage
        else
        {
            damage -= armor;
            armor = 0;
            health -= damage;
        }

        // Trigger the red flash
        flashScreen.FlashRed(30f);

        //Update health and armor sliders
        gm.barUi.ArmorSlider();
        gm.barUi.HealthSlider();

        //Set isAlive to false if no more health.
        if (!(isAlive = health > 0))
            Death();
    }

    public void AddHealth(float healingValue)
    {
        health += healingValue;
        if (health >= maxHealth)
            health = maxHealth;
        gm.barUi.HealthSlider();
    }

    public void AddArmor(float armorValue)
    {
        armor += armorValue;
        if (armor >= maxArmor)
            armor = maxArmor;
        gm.barUi.ArmorSlider();
    }
    private void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
    public void Death()
    {
        //-------------------
        //gm.playerStuff.coins = 0;
        gm.inventory.Container.Clear();
        DeathPanel.SetActive(true);
        Invoke("LoadScene", delayTime);
    }
}

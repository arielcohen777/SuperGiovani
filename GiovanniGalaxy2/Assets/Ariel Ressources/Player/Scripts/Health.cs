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
    //--------------------s
    private FlashScreen flashScreen;
    public GameObject DeathPanel;
    public float delayTime = 5f;
    public string sceneName = "Main Menu";


    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
        flashScreen = FindObjectOfType<FlashScreen>();
        health = maxHealth;
        armor = maxArmor;
    }
    public void Update()
    {
        //diabled for now because of armor problem with flash screen UI
       /* if(armor > 0 || health > 50)
        {
            flashScreen.StopFlashing();
        }else flashScreen.FlashRed(0.1f);*/
       
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

        //Update health and armor sliders
        gm.barUi.ArmorSlider();
        gm.barUi.HealthSlider();

        //Set isAlive to false if no more health.
        if (!(isAlive = health >0))
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
        //-------------------s
        DeathPanel.SetActive(true);
        Invoke("LoadScene", delayTime);


    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BatteryCounter : MonoBehaviour
{
    public Text interText;
    public Text countertext;
    public Text timerText;
    public List<Battery> items = new List<Battery>();

    public int batteryCtr = 0;
    public float timeValue = 0;
    public string mssgText = "";

    public bool increasedTime;
    public static bool isCharging;

    private GameManager gm;

    void Start()
    {
        timerText.gameObject.SetActive(false);
        RefreshUI();
        gm = GameManager.Instance;

    }
    private void Update()
    {
        interText.text = mssgText;

        if (timeValue > 0)
        {
            if ((batteryCtr == 1 || gm.Portal.BatteriesAdded == 1) && !increasedTime)
            {
                timeValue = timeValue * 2;
                increasedTime = true;
            }
            else if ((batteryCtr == 2 || gm.Portal.BatteriesAdded == 2) && !increasedTime)
            {
                timeValue = timeValue * 3;
                increasedTime = true;
            }

            isCharging = true;
            timerText.gameObject.SetActive(true);
            timeValue -= Time.deltaTime;
        }
        else
        {
            isCharging = false;
            increasedTime = false;
            timeValue = 0;
            timerText.gameObject.SetActive(false);
        }

        DisplayTime(timeValue);

    }
    public void Add(Battery item)
    {
        items.Add(item);
        batteryCtr++;
        RefreshUI();
    }

    public void Remove(Battery item)
    {
        items.Remove(item);
        //batteryCtr--;
        RefreshUI();
    }

    public void RefreshUI()
    {
        countertext.text = batteryCtr.ToString();
    }

    public void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

    }
}

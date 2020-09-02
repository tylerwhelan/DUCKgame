using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreadScript : MonoBehaviour
{
    private int breadLevel;
    public SpriteRenderer breadSprite;
    public Sprite[] breadSpriteList;
    private int ability = 0;
    private byte trueLevel = 0;
    private PowerDuckFlight duckRef;
    public GameObject powerPanel;
    public Text powerText;
    float panelCooldown;
    bool panelTicking;

    //Powerup count
    const int basicCount = 2;
    const int mediumCount = 2;
    const int strongCount = 1;
    const int superCount = 1;

    private void Start()
    {
        duckRef = FindObjectOfType<PowerDuckFlight>();
    }

    public int BreadLevel
    {
        set
        {
            breadLevel = value;
            UpdateBread();
        }
    }

    void Update()
    {
        if (transform.position.x <= -15)
        {
            EndSelf(true);
        }

        if (panelTicking)
        {
            if (panelCooldown > 0)
            {
                panelCooldown -= Time.deltaTime;
            }
            else
            {
                HidePowerPanel();
            }
        }
    }

    void UpdateBread()
    {
        int number = Random.Range(0, 50) + Mathf.Clamp(breadLevel, 0, 25);
        if (number <= 25)
        {
            breadSprite.sprite = breadSpriteList[0];
            trueLevel = 0;
            if (basicCount > 0)
            {
                ability = Random.Range(0, basicCount - 1);
            }
        }
        else if (number <= 47)
        {
            breadSprite.sprite = breadSpriteList[1];
            trueLevel = 1;
            if (mediumCount > 0)
            {
                ability = Random.Range(0, mediumCount - 1);
            }
        }
        else if (number <= 55)
        {
            breadSprite.sprite = breadSpriteList[2];
            trueLevel = 2;
            if (strongCount > 0)
            {
                ability = Random.Range(0, strongCount - 1);
            }
        }
        else
        {
            breadSprite.sprite = breadSpriteList[3];
            trueLevel = 3;
            if (superCount > 0)
            {
                ability = Random.Range(0, superCount - 1);
            }
        }
    }

    public void ConsumeBread()
    {
        string text = "";
        switch (trueLevel)
        {
            #region Basic Abilities
            default:
                text = "You picked up some Bread:";
                switch (ability)
                {
                    #region Jump Increase
                    default:
                        ShowPowerPanel(text + "\n Health restored!");
                        duckRef.ActivateAbility(2);
                        break;
                    #endregion
                    #region Jump Decrease
                    case 1:
                        ShowPowerPanel(text + "\n Max Health increased!");
                        duckRef.ActivateAbility(3);
                        break;
                        #endregion
                }
                break;
            #endregion
            #region Medium Abilities
            case 1:
                text = "You picked up some bRed:";
                switch (ability)
                {
                    default:
                        ShowPowerPanel(text + "\n Slowdown Ability Time Increased!");
                        duckRef.ActivateAbility(0);
                        break;
                    case 1:
                        ShowPowerPanel(text + "\n Slowdown Ability Strength Increased!");
                        duckRef.ActivateAbility(1);
                        break;
                }
                break;
            #endregion
            #region Strong Abilities
            case 2:
                text = "You picked up some Ghost Bread:";
                switch (ability)
                {
                    default:
                        ShowPowerPanel(text + "\n This bread doesn't do anything yet!");
                        break;
                }
                break;
            #endregion
            #region Super Abilities
            case 3:
                text = "You picked up some Sourdough:";
                switch (ability)
                {
                    default:
                        ShowPowerPanel(text + "\n This bread doesn't do anything yet!");
                        break;
                }
                break;
                #endregion
        }
        EndSelf(false);
    }

    public void MoveBread(float vel)
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(vel, 0);
    }

    void ShowPowerPanel(string panelText)
    {
        powerText.text = panelText;
        powerPanel.SetActive(true);
        panelCooldown = 3f;
        panelTicking = true;
    }

    void HidePowerPanel()
    {
        panelTicking = false;
        powerPanel.SetActive(false);
        EndSelf(true);
    }

    void EndSelf(bool full)
    {
        if (full)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.position = new Vector3(0, -15, 0);
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }
}
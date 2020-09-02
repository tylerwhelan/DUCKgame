using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerDuckFlight : DuckFlightScript
{
    float slowStrength = 0;
    float slowTime = 0;
    float abilityCooldown = 0;
    public float initialAbilityCooldown = 1;
    public int health = 20;
    public int healthLoss = 10;
    float damageCooldown;
    bool invulnerable = false;
    public GameObject textHealth;
    public int maxHealth = 20;

    public void ModifyJump(float modifier)
    {
        jumpForce += modifier;
    }

    public void ActivateAbility(int abilityID)
    {
        switch (abilityID)
        {
            default:
                slowStrength *= 0.9f;
                if (slowTime <= 0)
                {
                    slowTime = 0.5f;
                }
                break;
            case 1:
                if (slowTime <= 0)
                {
                    slowTime = 0.5f;
                }
                if (slowStrength <= 0)
                {
                    slowStrength = 0.9f;
                }
                break;
            case 2:
                if (health < maxHealth)
                {
                    health += 5;
                }
                break;
            case 3:
                maxHealth++;
                break;
        }
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        //Detects collision with powerups
        if (collision.CompareTag("Powerup"))
        {
            if (collision.GetComponent<BreadScript>())
            {
                collision.GetComponent<BreadScript>().ConsumeBread();
            }
        }

        //Detects collision with enemies
        if (collision.CompareTag("Obstacle") && !invulnerable)
        {
            TakeDamage();
        }
    }

    void TakeDamage()
    {
        damageCooldown = 3f;
        health -= healthLoss;
        invulnerable = true;
        if (textHealth.GetComponent<Text>())
        {
            textHealth.GetComponent<Text>().text = "Health: " + health;
        }
    }

    public override void Update()
    {
        base.Update();
        if (!dead)
        {
            if (abilityCooldown <= 0)
            {
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    if (slowTime > 0)
                    {
                        BeginSlow();
                        Invoke("EndSlow", slowTime);
                    }
                    Debug.Log("Shift Ability");
                }
                if (Input.GetKeyDown(KeyCode.W))
                {

                }
                if (Input.GetKeyDown(KeyCode.A))
                {

                }
                if (Input.GetKeyDown(KeyCode.D))
                {

                }
                abilityCooldown = initialAbilityCooldown;
            }
            else
            {
                abilityCooldown -= Time.deltaTime;
            }

            if (damageCooldown > 0)
            {
                damageCooldown -= Time.deltaTime;
                if (GetComponent<SpriteRenderer>())
                {
                    GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 140);
                }
            }
            else if (invulnerable)
            {
                invulnerable = false;
                if (GetComponent<SpriteRenderer>())
                {
                    GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
                }
            }
        }
    }

    void BeginSlow()
    {
        Time.timeScale = slowStrength;
    }

    void EndSlow()
    {
        Time.timeScale = 1;
    }
}

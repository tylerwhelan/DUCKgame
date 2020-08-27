using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerDuckFlight : DuckFlightScript
{
    float slowStrength = 0;
    float slowTime = 0;
    float abilityCooldown = 0;
    public float initialAbilityCooldown = 1;

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerDuckFlight : DuckFlightScript
{
    public void ModifyJump(int modifier)
    {
        jumpForce += modifier;
    }
}

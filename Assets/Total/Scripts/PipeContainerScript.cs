using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeContainerScript : MonoBehaviour
{
    public Rigidbody lowerPipe;
    public Rigidbody upperPipe;

    void Update()
    {
        if (lowerPipe.gameObject.transform.position.x <= -15)
        {
            Destroy(gameObject);
        }
    }
    public void UpdateVelocity(float vel)
    {
        lowerPipe.velocity = new Vector3(vel, 0, 0);
        upperPipe.velocity = new Vector3(vel, 0, 0);
    }
}

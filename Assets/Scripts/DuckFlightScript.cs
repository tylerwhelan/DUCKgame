using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DuckFlightScript : MonoBehaviour
{
    public float jumpForce = 1;
    public Vector2 xConstraints;
    public Vector2 yConstraints;
    public float maxGravity; // negative number
    float score = 0;
    public bool dead = false;
    public GameObject textScore;

    public virtual void Update()
    {
        if (!dead)
        {
            Vector3 moveVector = Vector3.zero;
            //Up-down movement
            if (Input.GetKey(KeyCode.Space))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector3(0, jumpForce, 0);
            }

            transform.position = new Vector3(Mathf.Clamp(transform.position.x, xConstraints.x, xConstraints.y), Mathf.Clamp(transform.position.y, yConstraints.x, yConstraints.y), 0);
            GetComponent<Rigidbody2D>().velocity = new Vector3(0, Mathf.Clamp(GetComponent<Rigidbody2D>().velocity.y, maxGravity, jumpForce), 0);

            //Score
            score += 1 * Time.deltaTime;
            if (textScore.GetComponent<Text>())
            {
                textScore.GetComponent<Text>().text = "Score: " + Mathf.RoundToInt(score);
            }
        }
    }

    //Run when player dies
    void TargetHit()
    {
        dead = true;
        Time.timeScale = 0;
        Debug.Log("Score: " + Mathf.RoundToInt(score));
    }

    public void IncreaseScore(int increase)
    {
        score += increase;
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        //Detects collision with enemies
        if (collision.CompareTag("Obstacle"))
        {
            TargetHit();
        }
    }
}

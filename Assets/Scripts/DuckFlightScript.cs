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
    bool dead = false;
    public GameObject textScore;

    void Update()
    {
        if (!dead)
        {
            Vector2 moveVector = Vector2.zero;
            //Up-down movement
            if (Input.GetKey(KeyCode.Space))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, jumpForce);
            }

            transform.position = new Vector2(Mathf.Clamp(transform.position.x, xConstraints.x, xConstraints.y), Mathf.Clamp(transform.position.y, yConstraints.x, yConstraints.y));
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, Mathf.Clamp(GetComponent<Rigidbody2D>().velocity.y, maxGravity, jumpForce));

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Detects collision with enemies
        if (collision.CompareTag("Obstacle"))
        {
            TargetHit();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedupScript : MonoBehaviour
{
    List<SpeedupHolder> info = new List<SpeedupHolder>();
    public float globalSpeed = 1;
    public float globalSpeedIncrease = 1;
    public GameObject obstaclePrefab;
    public float obstacleTimer = 3;
    public float obstacleTimerInitial = 3;
    public float obstacleTimerSpeed = 1;
    public Vector2 obstacleConstraints;
    public float obstacleSpawnPosX;
    public bool breadMode = false;
    private int breadCounter = 0;
    public int breadCounterInitial = 10;
    public GameObject breadPrefab;

    private void Start()
    {
        obstacleTimer = obstacleTimerInitial;
        breadCounter = breadCounterInitial;
        for (int i = 0; i < FindObjectsOfType<SpeedupHolder>().Length; i++)
        {
            info.Add(FindObjectsOfType<SpeedupHolder>()[i]);
        }
    }

    void Update()
    {
        for (int i = 0; i < info.Count; i++)
        {
            if (info[i].targetObject.transform.position.x <= -15)
            {
                info[i].targetObject.transform.position = new Vector3((info[i].targetObject.transform.position.x + 30) - (info[i].speed + globalSpeed) * Time.deltaTime, info[i].targetObject.transform.position.y, info[i].targetObject.transform.position.z);
            }
            else
            {
                info[i].targetObject.transform.position = new Vector3(info[i].targetObject.transform.position.x - (info[i].speed + globalSpeed) * Time.deltaTime, info[i].targetObject.transform.position.y, info[i].targetObject.transform.position.z);
            }
        }
        globalSpeed += globalSpeedIncrease * Time.deltaTime;

        if (obstacleTimer <= 0)
        {
            obstacleTimer = obstacleTimerInitial;
            GameObject obj = Instantiate(obstaclePrefab);
            Vector3 vec = new Vector3(obstacleSpawnPosX, Random.Range(obstacleConstraints.x, obstacleConstraints.y), 0);
            obj.transform.position = vec;
            if (obj.GetComponent<PipeContainerScript>())
            {
                obj.GetComponent<PipeContainerScript>().UpdateVelocity(-(3 + globalSpeed / 10));
            }
            if (breadMode)
            {
                breadCounter = breadCounterInitial;
                GameObject bread = Instantiate(breadPrefab);
                if (bread.GetComponent<BreadScript>())
                {
                    bread.GetComponent<BreadScript>().BreadLevel = Mathf.RoundToInt(globalSpeed);
                }
                bread.transform.position = vec;
            }
        }
        else
        {
            obstacleTimer -= obstacleTimerSpeed * (1 + (globalSpeed / 10)) * Time.deltaTime;
        }
    }
}
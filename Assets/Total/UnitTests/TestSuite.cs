using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using NUnit.Framework;

public class TestSuite
{
    private List<SpeedupHolder> spdList = new List<SpeedupHolder>();
    private List<float> posList = new List<float>();
    private DuckFlightScript player = null;
    private int checkCount = 40;

    [SetUp]
    public void SetUp()
    {
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
    }

    [UnityTest]
    public IEnumerator ObjectMoves()
    {
        if (!CheckSpeedHolder())
        {
            yield return new WaitForSecondsRealtime(0.1f);
            FillSpdList();
        }
        if (!player)
        {
            yield return new WaitForSecondsRealtime(0.1f);
            player = Object.FindObjectOfType<DuckFlightScript>();
            player.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
        UpdatePosList();
        yield return new WaitForSecondsRealtime(0.1f);
        Assert.Less(spdList[0].transform.position.x, posList[0]);
    }

    [UnityTest]
    public IEnumerator ObjectWraps()
    {
        if (!CheckSpeedHolder())
        {
            yield return new WaitForSecondsRealtime(0.1f);
            FillSpdList();
        }
        if (!player)
        {
            yield return new WaitForSecondsRealtime(0.1f);
            player = Object.FindObjectOfType<DuckFlightScript>();
            player.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
        bool wrapped = false;
        while (checkCount > 0 && !wrapped)
        {
            UpdatePosList();
            yield return new WaitForSecondsRealtime(0.1f);
            for (int i = 0; i < spdList.Count; i++)
            {
                if (spdList[i].transform.position.x > posList[i])
                {
                    wrapped = true;
                    break;
                }
            }
            checkCount--;
        }
        Debug.Log(wrapped);
        Assert.True(wrapped);
        player.gameObject.SetActive(true);
    }

    [UnityTest]
    public IEnumerator PlayerCanTakeDamage()
    {
        if (!CheckSpeedHolder())
        {
            yield return new WaitForSecondsRealtime(0.1f);
            FillSpdList();
        }
        if (!player)
        {
            yield return new WaitForSecondsRealtime(0.1f);
            Debug.Log("ERROR");
            player = Object.FindObjectOfType<DuckFlightScript>();
            player.gameObject.SetActive(true);
            Time.timeScale = 1;
        }
        checkCount = 30;
        Object.FindObjectOfType<SpeedupScript>().SpawnObstacle(new Vector3(player.transform.position.x, player.transform.position.y + 2f, player.transform.position.z));
        int health = (player as PowerDuckFlight).health;
        int changedHealth = (player as PowerDuckFlight).health;
        bool yes = false;
        while (checkCount > 0 && !yes)
        {
            yield return new WaitForSecondsRealtime(0.1f);
            if (changedHealth < health)
            {
                yes = true;
            }
            checkCount--;
        }
        Debug.Log(yes);
        Assert.True(yes);
    }

    [TearDown]
    public void Teardown()
    {
        SceneManager.UnloadSceneAsync(1);
    }

    bool CheckSpeedHolder()
    {
        if (spdList.Count <= 0)
        {
            return false;
        }
        return spdList[0];
    }

    private void FillSpdList()
    {
        spdList.Clear();
        foreach (SpeedupHolder spd in Object.FindObjectsOfType<SpeedupHolder>())
        {
            spdList.Add(spd);
        }
        UpdatePosList();
    }

    private void UpdatePosList()
    {
        posList.Clear();
        for (int i = 0; i < spdList.Count; i++)
        {
            posList.Add(spdList[i].transform.position.x);
        }
    }

    /*
    IEnumerator CheckWrap()
    {
        if (spdHld.transform.position.x > oldPos || checkCount <= 0)
        {
            yield return null;
        }
        else
        {
            yield return new WaitForSeconds(0.1f);
        }
    }
    */
}

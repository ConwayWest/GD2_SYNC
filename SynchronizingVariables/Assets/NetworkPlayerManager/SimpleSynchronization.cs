using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NETWORK_ENGINE;
using UnityEngine.UI;

public class SimpleSynchronization : NetworkComponent
{
    // Synchronized Variables
    public int score = 0;
    public bool textCreated = false;
    public bool scoreCond = true;

    public override void HandleMessage(string flag, string value)
    {
        if(flag == "SCORE")
        {
            if (IsClient)
            {
                score = int.Parse(value);
            }
        }
    }

    public override IEnumerator SlowUpdate()
    {
        // initialize your class
        // Network start code should go here
        while(true)
        {
            // Game Logic Loop
            if (IsClient)
            {
                
            }
            if (IsLocalPlayer)
            {
                transform.GetChild(0).gameObject.SetActive(true);
                
                if(!textCreated)
                {
                    Text playerText = transform.GetChild(0).gameObject.AddComponent<Text>();
                    playerText.raycastTarget = false;
                    StartCoroutine("playerListRefresh", playerText);
                    textCreated = true;
                }
                
            }
            if (IsServer)
            {
                if(scoreCond == true)
                {
                    StartCoroutine("GrowScore");
                }
            }
            yield return new WaitForSeconds(MyCore.MasterTimer);
        }
    }

    public IEnumerator playerListRefresh(Text playerListText)
    {
        
        while (true)
        {
            yield return new WaitForSeconds(1f);
            playerListText.text = "";
            GameObject[] PlayerObjects = GameObject.FindGameObjectsWithTag("Player");
            for (int i = 0; i < PlayerObjects.Length; i++)
            {

                playerListText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
                playerListText.fontSize = 120;
                playerListText.text += "Player " + (i + 1) + ": " + PlayerObjects[i].GetComponent<SimpleSynchronization>().score.ToString() + "\n";

            }
        }
    }

    public IEnumerator GrowScore()
    {
        scoreCond = false;
        yield return new WaitForSeconds(1f);
        //increase score
        setScore(score += 1);
        if (IsDirty)
        {
            // Send all synchronized information
            SendUpdate("SCORE", score.ToString());
            IsDirty = false;
        }
        scoreCond = true;
    }

    public void setScore(int s)
    {
        if (IsServer)
        {
            score = s;
            SendUpdate("SCORE", score.ToString());

        }
    }
}

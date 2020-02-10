using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NETWORK_ENGINE;

public class NetworkPManager : NetworkComponent
{
    public int Score = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void HandleMessage(string flag, string value)
    {
        if(flag == "SC")
        {
            if(IsServer)
            {
                Score++;
                SendUpdate("SC", Score.ToString());
            }
            if(IsClient)
            {
                Score = int.Parse(value);
            }
        }
    }

    public override IEnumerator SlowUpdate()
    {
        while(true)
        {
            if(IsServer)
            {
                if(IsDirty)
                {
                    SendUpdate("SC", Score.ToString());
                    IsDirty = false;
                }
            }
            if(IsClient)
            {
            }
            if(IsLocalPlayer)
            {
                if (Input.GetAxisRaw("Jump") > 0)
                {
                    SendCommand("SC", "1");
                }
            }

            yield return new WaitForSeconds(MyCore.MasterTimer);
        }
    }



}

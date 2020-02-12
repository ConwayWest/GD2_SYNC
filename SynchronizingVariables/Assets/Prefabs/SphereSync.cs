using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NETWORK_ENGINE;

public class SphereSync : NetworkComponent
{
    public GameObject spherePrefab;
    public bool populateCond = false;
    public override void HandleMessage(string flag, string value)
    {
        if(flag == "CS")
        {
            if(IsServer)
            {
                MyCore.NetCreateObject(int.Parse(value), Owner, new Vector3(Random.Range(-2.0f, 2.0f), Random.Range(2.0f, 8.0f), Random.Range(-3.0f, 3.0f)));
            }
        }
    }

    public override IEnumerator SlowUpdate()
    {
        while(true)
        {
            if(IsClient)
            {

            }
            if(IsLocalPlayer)
            {

            }
            if(IsServer)
            {
                
            }
            yield return new WaitForSeconds(MyCore.MasterTimer);
        }
    }

    public void createSphere()
    {
        SendCommand("CS", "0");
    }
}

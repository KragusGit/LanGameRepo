using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class StreakManager : NetworkBehaviour {
    [SyncVar]
    public float meter;
    [SyncVar]
    int lastKilledSinceSeconds;
    // Use this for initialization
    void Start () {
        meter = 0;
        lastKilledSinceSeconds = 0;
        InvokeRepeating("countSeconds", 0, 1);
	}
	
	// Update is called once per frame
	void Update () {
        meter = (float)System.Math.Round(meter, 3);
	if(lastKilledSinceSeconds>10)
        {
            if(!IsInvoking("reduceMeter"))
            {
                Invoke("reduceMeter", 1.5f);
            }
        }
    
	}
   public void raiseMeter()
    {   if (meter < 0.0000000000000000)
        {
            lastKilledSinceSeconds = 0;
            meter += 10;
            meter = Mathf.Clamp(meter, -500, 0);
        }
        else
        {
            lastKilledSinceSeconds = 0;
            meter += 10;
        }
      
    }
    void reduceMeter()
    {
      
        if (meter > 0.00)
        { 
                meter -= 0.1f;
        }
      
    }
    public void reduceMeterToZero()
    {
        if (meter >= 0.1)
        {
            meter -= 5;
            meter = Mathf.Clamp(meter, 0, 500);
        }

       else
        {
            meter -= 10;
        }

        }

void countSeconds()
    {
        lastKilledSinceSeconds++;
    }
}

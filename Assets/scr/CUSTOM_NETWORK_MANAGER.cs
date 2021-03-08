using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class CUSTOM_NETWORK_MANAGER : NetworkManager
{
   public static CUSTOM_NETWORK_MANAGER CNM;
    void Start()
    {
        CNM = this;
    }
  


   
    
    
    public override void OnStartClient(NetworkClient client)
    {
       NETWORK_DISCOVERY_CUSTOM.NDC.showGUI = false;
      
    }

    public override void OnStopClient()
    {
        NETWORK_DISCOVERY_CUSTOM.NDC.showGUI = true;
        scoremanager.SC_MAN.players.Clear();
        
    }
    

    /*
            if (localServers.ContainsKey(data))
            {

                string serverName = data;
                //ArrayList ips = (ArrayList)localServers[data];
                if (localServers.Contains(realIP))
                {
                    Debug.Log("I CONTAIN REALIP");
                    //Do Nothing
                }
                else
                {
                    Debug.Log("I DONT HAVE REALIP");
                    localServers.Clear();
                   localServers.Add(data, realIP);

                      dirty = true;
                    //localServers[data] = ips;
                }
            }
            else
            {
                Debug.Log("Adding a new entry to local servers");
                //ArrayList ips = new ArrayList();
                localServers.Add(data, realIP);

                Debug.Log("There are now: " + localServers.Count.ToString());
                dirty = true;
            }

            if (dirty)
            {
                Debug.Log("I PASSED THIS DIRTY IF");
                Debug.Log("REALIP - " + realIP);

                KeyValuePair<string, string> pair1 = new KeyValuePair<string, string>(data, realIP);
                string name = pair1.Key.ToString();
                //ArrayList list = pair1.Value as string;
                int count = localServers.Count;
                string c = count.ToString();
                string message = name + " has " + c;
                TryConnections(pair1.Key);




            }
            else
            {
                if (!isConnecting)
                {
                    Debug.Log("I PASSED THIS CONNECTION IF");

                    KeyValuePair<string, string> pair2 = new KeyValuePair<string, string>(data, realIP);
                    Debug.Log("Attempting to find a server and connect to the proper IP address for it.");

                    TryConnections(pair2.Key);

                    //singleton.networkAddress = (pair.Value as ArrayList)[0] as string;
                    //Debug.Log("Try to connect to the first one: " + singleton.networkAddress);
                    //NetworkManager.singleton.StartClient();
                    return;


                }
            }

        }

        protected void TryConnections(string connectionName = "")
        {
            Debug.Log("I SOMEHOW ENTERED TRYING CONNECTION");

            if (isConnecting)
            {
                Debug.Log("Already connecting to something, bail!");
                return;
            }
            Debug.Log("Trying connections!");
            if (connectionName != "")
            {
                if (currentIPIndex < localServers.Count)
                {
                Debug.Log("I AM INSIDE <>" + "COUNT = " + currentIPIndex);

                Debug.Log("LOCAL SERVER COUNT = "+ localServers.Count);
                Debug.Log("LOCAL SERVER ADDRESS = " + connectionName);
                singleton.networkAddress = connectionName ;
                    isConnecting = true;
                    NetworkManager.singleton.StartClient();
                    StartCoroutine(ConnectToIPTimeout(5));
                }
                else
                {
                    Debug.Log("Tried all the IPs for " + connectionName + " and they all timed out. Removing it.");
                }
            }
        }

        bool isConnecting = false;
        int currentIPIndex = 0;
        string currentServerName = "";

        IEnumerator ConnectToIPTimeout(float duration)
        {
            yield return new WaitForSeconds(duration);
            isConnecting = false;
            currentIPIndex++;
            TryConnections(currentServerName);
        }
            */
}


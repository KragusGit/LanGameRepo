using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class NETWORK_DISCOVERY_CUSTOM : NetworkDiscovery {
    public static NETWORK_DISCOVERY_CUSTOM NDC;
    public  List< NetworkIdentity> clients;
    
    void Start()
    {
        NDC = this;
        clients = new List<NetworkIdentity>();
    }

  
    int timesBroadcastReceived = 0;
    override public void OnReceivedBroadcast(string fromAddress, string data)
    {
       // Debug.Log("I AM HERE");
      //  Debug.Log("DATA - " + data);
      //  Debug.Log("From Address - " + fromAddress);

        if(timesBroadcastReceived==0)
       OnReceivedBroadcast1(fromAddress, data);


    }
    public void OnReceivedBroadcast1(string fromAddress, string data)
    {
        //Debug.Log("Received a broadcast.");
        //  bool dirty = false;
        string sep = ":";
        string realIP = fromAddress.Substring(fromAddress.LastIndexOf(sep) + 1);
        //Debug.Log("REALIP - " + realIP);

        NetworkManager.singleton.networkAddress = realIP;

        NetworkManager.singleton.networkPort = 7777;
        // Debug.Log("STARTING SERVER");

        NetworkManager.singleton.StartClient();
        //  scoremanager.SC_MAN._playerList.Add(clients.connection.playerControllers[0].unetView);

        timesBroadcastReceived++;
        

    }

    
}

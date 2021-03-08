using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
public class localPlayer:NetworkBehaviour {
    [SyncVar]
    public int score=0;
    
    // Use this for initialization

    public void regesterMe()
    {
        scoremanager.SC_MAN.RegisterPlayer(netId.ToString(), score);
    }
	void Start () {

        this.name =  this.netId + "player";

        regesterMe();
        
        if (isLocalPlayer){
           // GameObject.Find("_debugText").GetComponent<UI_DEBUG_SCRIPT>()._Player = this.gameObject;
			GetComponent<Light>().enabled = true;
			GetComponent<Shoot>().enabled= true;
			GetComponent<movement>().enabled = true;
			GetComponent<Animator>().enabled = true;
            GetComponent<ANIM_Manager>().enabled = true;
  
			CameraFollow  C =	Camera.main.GetComponent<CameraFollow>();
			C.target = this.gameObject.transform;
			C.enabled = true;
            
            
           
        }
		else{
			return;
		}
	}
	

	void OnDisconnectedFromServer(NetworkDisconnection info) {
		Debug.Log("Disconnected from server: " + info);
      
	}
}

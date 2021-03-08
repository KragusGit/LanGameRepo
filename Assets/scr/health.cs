
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class health : NetworkBehaviour
{
    public float hlt = 100;
    public float hitbuff = 0.1f;
    public bool canhit = true;
    string lstBulletHit;
    public setHealthGUI hltGUI;

    // Use this for initialization
    void Start()
    {
        hltGUI.setGUI(hlt / 100);
    }
    void OnTriggerEnter(Collider colsn)
    {

        if (colsn.gameObject.tag == "bullet" && colsn.gameObject.GetComponent<bullet>().bIdentity != (netId + "player"))
        {
            lstBulletHit = colsn.gameObject.GetComponent<bullet>().bIdentity;
            hltGUI.setGUI(hlt / 100);
            Cmd_tkdmg(this.gameObject, colsn.gameObject.GetComponent<bullet>().damage, canhit);
            colsn.gameObject.GetComponent<bullet>().OnKilled();
            Destroy(colsn.gameObject);
        }
    }
  
    // Update is called once per frame
    void Update()
    {


        if (hlt <= 0)
        {
            
            Cmd_incrimentScore();
            GameObject.Find(lstBulletHit).GetComponent<StreakManager>().raiseMeter();

            this.gameObject.GetComponent<StreakManager>().reduceMeterToZero();
            Cmd_respawnPlayer();
        }
    }
    IEnumerator tookdmg()
    {
        yield return new WaitForSeconds(hitbuff);
        canhit = true;
    }
    IEnumerator RSTplr(GameObject G)
    {
        Debug.Log("bro crtn");
        yield return new WaitForSeconds(3);
        G.SetActive(true);
        Debug.Log("reset");

    }
    [ClientRpc]
    public void Rpc_takeDMG(GameObject A, float amt, bool cool)
    {
        if (cool)
        {
            if (canhit)
                A.GetComponent<health>().hlt -= amt;
              
            print(hlt + " " + gameObject);
            canhit = false;
            StartCoroutine("tookdmg");
        }
        else
        {
            hlt -= amt;
        }

    }
  
    [Command]
    public void Cmd_incrimentScore()
    {

        if (GameObject.Find(lstBulletHit))
            GameObject.Find(lstBulletHit).GetComponent<localPlayer>().score += 100;
     
        
    }

    public void Cmd_tkdmg(GameObject A, float amt, bool cool)
    {
        Rpc_takeDMG(A, amt, cool);
    }

    public void Cmd_respawnPlayer()
    {
        Camera.main.GetComponent<spawnManager>().KillPlayer(this.gameObject);




    }
    IEnumerator RSTplr()
    {
        yield return new WaitForSeconds(1);
        Debug.Log("bro crtn");

        this.gameObject.SetActive(true);
        Debug.Log("reset");

    }




}

using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class ANIM_Manager : NetworkBehaviour
{
  public  GameObject[] classes;
    public GameObject CurrentClass;
    [SyncVar]
    public int playerClassNO ;
    Animator A;
	// Use this for initialization


    public void DisableAll()
    {
        
            foreach (GameObject G in classes)
            {
                G.SetActive(false);
            }
        
    }

    public void SetClass()
    {
    //  Debug.Log(playerClassNO + "" + gameObject.name);
     CurrentClass =   classes[playerClassNO+ 1];
        CurrentClass.SetActive(true);
       A = CurrentClass.GetComponent<Animator>();
    }

    public void SetAnim(bool val)
    {
        if(A!=null)
        A.SetBool("walk", val);
    }
    public void SetDodgeAnim()
    {
        if (A != null)
        {
            A.SetTrigger("Dodge");
        }
    }
  
}

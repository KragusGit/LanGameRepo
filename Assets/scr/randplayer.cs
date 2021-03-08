using UnityEngine;
using UnityEngine.Networking;

public class randplayer : NetworkBehaviour
{
    bool reset;
    int currentClass;
    ANIM_Manager A;
    public int no_of_classes;
    Shoot S;
    int r;
  
    [System.Serializable]
    public struct Classes
    {

        public bool spread;
        public bool dualHands;
        public bool UseFan;
        public float ofset;
        public float crsrSpeed;
        public int clipsize;
        public float reloadSpeed;
        public float knockbackFactor;
        public float delay;
        public float CamShakeAmt;
        public float fanArc;
        public float fanSecs ;
        public bullet BLT;
        public Transform T;
        public Transform AL_T;
        public GameObject M1;
        public GameObject M2;
        public Vector3 mos;
        public Vector3 crs;
    


    }

    public Classes[] C = new Classes[2];

    void Start()
    {
        reset = true;
        A = GetComponent<ANIM_Manager>();
        S = this.GetComponent<Shoot>();
        A.DisableAll();
        randSpawn();




    }

    void Update()
    {
        if (reset&&isClient||currentClass!=A.playerClassNO)
        {  
            setPlayer();
            reset = false;
        }
        
    }
    public void randSpawn()
    {
        A.DisableAll();

        if (isServer)
        {
           RandomizeClass();
         
        }
    



     


    }

    
    public void setPlayer()
    {
        int R = this.GetComponent<ANIM_Manager>().playerClassNO;
        currentClass = R;
        S.playerClassNo = R;
        S.ofset = C[R].ofset;
        S.crsrSpeed = C[R].crsrSpeed;
        S.clipsize = C[R].clipsize;
        S.reloadSpeed = C[R].reloadSpeed;
        S.knockbackFactor = C[R].knockbackFactor;
        S.CamShakeAmt = C[R].CamShakeAmt;
        S.BLT = C[R].BLT;
        S.T = C[R].T;
        S.UseFan = C[R].UseFan;
        S.fanArc = C[R].fanArc;
        S.fanSecs = C[R].fanSecs;
        if (!(C[R].AL_T == null))
            S.AL_T = C[R].AL_T;
        if (!(C[R].M1 == null))
            S.M1 = C[R].M1;
        if (!(C[R].M2 == null))
            S.M2 = C[R].M2;
        S.mos = C[R].mos;
        S.crs = C[R].crs;
        S.spread = C[R].spread;
        S.dualHands = C[R].dualHands;
        S.delay = C[R].delay;
        A.DisableAll();
        A.SetClass();

    }
  public void RandomizeClass()
    {

        r = Random.Range(0, no_of_classes);
        Debug.Log("ClassNo: " + r);
        this.GetComponent<ANIM_Manager>().playerClassNO = r;
        setPlayer();
        Rpc_resetClientState();


    }
    [ClientRpc]
    public void Rpc_resetClientState()
    {
        reset = true;
    }
}
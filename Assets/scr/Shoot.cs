using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class Shoot : NetworkBehaviour {
	
	public int playerClassNo;
	public bool spread;	public bool dualHands; public bool UseFan;	public float ofset;
	public float crsrSpeed = 100;
    public int clipsize;
    public float reloadSpeed;
    public float knockbackFactor = 1.2f ;
	public float delay = 0.5f;
	public float CamShakeAmt = 0.1f;
    public float fanArc = 0;
    public float fanSecs = 0;
	public bullet BLT;
	public Transform T;
	public Transform AL_T;
	public GameObject M1;
	public GameObject M2;
    public  Vector3 mos;
    public Vector3 crs;

    
	public AudioClip GunShot;
//	public bool S = false;
	public GameObject crosshair;
	private camshake cam ;




	public string netME;

	Rigidbody R;
	float timeS;
	float lerp =0;
	bool alt;
	float cur;
   public float magsize;
   public bool reloading;
	// Use this for initialization
	public void Start () {
	

        reloading = false;
		//Cursor.visible = false;
        magsize = clipsize;
		cur = crsrSpeed;
		cam = Camera.main.GetComponent<camshake>();
		crosshair = GameObject.Find("ch");
		M1.SetActive(false);
		M2.SetActive(false);
		R = GetComponent<Rigidbody>();
		this.GetComponent<Light>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        
        
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit floorHit;
        if (magsize <= 0 && !reloading)
        {
            reloading = true;
            StartCoroutine("reload");
        }

		if(Physics.Raycast(camRay,out floorHit)){
			crs= Vector3.Lerp(crosshair.transform.position,floorHit.point,Time.deltaTime*cur);
			crosshair.transform.position = crs;
			 mos =crs-transform.position;
			Vector3 bltDir;
			float spreadRange = Random.Range(0,ofset);
     
			//Debug.Log(mos.normalized);
            if (spread)
            {

                float spreadx = Random.Range(0, ofset) - ofset / 2  + mos.normalized.x;
                  float spreadz = Random.Range(0, ofset) - ofset / 2  + mos.normalized.z;
                  bltDir = new Vector3(spreadx, 0, spreadz);
            }
            else
            {
                bltDir = mos;

              //  Debug.Log("Enteringelse");
            }
                bltDir.y=0;
			mos.y = 0;
           
			Quaternion Q = Quaternion.LookRotation(mos);
			R.MoveRotation(Q);
			if(Input.GetMouseButton(0)&& timeS<=Time.time&& magsize>0){

				netME = this.netId+ "player";
				cam.shakeit(0.1f,CamShakeAmt,cam.transform.position);
				knockback(mos.normalized,knockbackFactor);
                magsize--;
				Cmd_shoot(bltDir,netME,playerClassNo);
                //Debug.Log("the magzine is " + magsize);
				this.GetComponent<Light>().enabled = true;
				StartCoroutine("shoot");
				AudioSource.PlayClipAtPoint(GunShot,transform.position);
				timeS =Time.time+delay;
			}
			if(Input.GetMouseButton(0)){
				cur = crsrSpeed;
			}
			else{
				cur = 100;
			}

			
		}
	}

		IEnumerator shoot(){
			yield return new WaitForSeconds(0.1f);
		//cam.shake=0.0f;
		M1.SetActive(false);
		M2.SetActive(false);
		this.GetComponent<Light>().enabled = false;
		}
        IEnumerator reload()
        {
           yield return new WaitForSeconds(reloadSpeed);
           magsize = clipsize;
           reloading = false;
        }
	public void knockback(Vector3 dir, float force){
		R.AddForce(dir*-force,ForceMode.Impulse);
	}
	[Command]
	public void Cmd_shoot(Vector3 mos, string netid , int BT){
		Rpc_shoot(mos,netid,BT);
        if (alt)
        {
            M2.SetActive(true);
        }
        else
        {
            M1.SetActive(true);
        }
	}
	[ClientRpc]
	public void Rpc_shoot(Vector3 mos,string netiD , int BT){
		if(GetComponent<randplayer>().C[BT].dualHands){
            
			if(alt){
			//M2.SetActive(true);
				bullet Bx = Instantiate(GetComponent<randplayer>().C[BT].BLT,GetComponent<randplayer>().C[BT].AL_T.position,Quaternion.identity) as bullet;
			Bx.GetComponent<Rigidbody>().velocity = mos.normalized*1;
			alt = false;
			Bx.bIdentity = netiD ;
          
			}
		else{
			//M1.SetActive(true);
				bullet Bx = Instantiate(GetComponent<randplayer>().C[BT].BLT,GetComponent<randplayer>().C[BT].T.position,Quaternion.identity) as bullet;
			Bx.GetComponent<Rigidbody>().velocity = mos.normalized*1;
			alt = true;
			Bx.bIdentity = netiD;
           
			}
		}
		else{
            if (GetComponent<randplayer>().C[BT].UseFan)
            {
                for (int i = 0; i <= GetComponent<randplayer>().C[BT].fanSecs; i++)
                {
                    float secDelta = (GetComponent<randplayer>().C[BT].fanArc / GetComponent<randplayer>().C[BT].fanSecs) / GetComponent<randplayer>().C[BT].fanArc;
                    Vector3 Nmos = Quaternion.Euler(0, Mathf.Lerp(-1 * GetComponent<randplayer>().C[BT].fanArc /2, GetComponent<randplayer>().C[BT].fanArc /2, secDelta*i),0) * mos;
                    bullet By = Instantiate(GetComponent<randplayer>().C[BT].BLT, GetComponent<randplayer>().C[BT].T.position, Quaternion.identity) as bullet;
                    By.GetComponent<Rigidbody>().velocity = Nmos.normalized * 1;
                    By.bIdentity = netiD;
                 }
                return;

            }
			M1.SetActive(true);
			bullet Bx = Instantiate(GetComponent<randplayer>().C[BT].BLT,GetComponent<randplayer>().C[BT].T.position,Quaternion.identity) as bullet;
			Bx.GetComponent<Rigidbody>().velocity = mos.normalized*1;
			alt = true;
			Bx.bIdentity = netiD;
           
    }

	}

}

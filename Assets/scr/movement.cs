using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class movement :NetworkBehaviour {
	public Vector3 dir;
	private float speed = 5;
	public float speedD = 5;
	Rigidbody R;
	ANIM_Manager A;
	bool alt;
	float timeS;
    bool dashing;
    Vector3 tempPos;
    float templerp = 0;
	// Use this for initialization
	void Start () {
		SetSpeed(speedD);
		A = GetComponent<ANIM_Manager>();
		 R = GetComponent<Rigidbody>();
        
	}

	
	// Update is called once per frame
	void FixedUpdate () {
        if(Input.GetKeyDown(KeyCode.Space) && dashing==false)
        {
            A.SetDodgeAnim();
            templerp = 0;
            dashing = true;
            tempPos = transform.position + (GetComponent<Shoot>().mos.normalized *15);

        }
        if (dashing == true)
        {
            if (transform.position != tempPos){
              
                transform.position = Vector3.Lerp(transform.position, tempPos,templerp);
                templerp += 0.001f;
                // R.MovePosition(transform.position + tempPos* speed *10* Time.deltaTime);

            }
            else
            {
                // templerp = 0;
            
                dashing = false;

            }
        }
        float h = Input.GetAxisRaw("Horizontal");
		float v = Input.GetAxisRaw("Vertical");

        if (!dashing)
        {
            dir = new Vector3(h, 0, v);

            R.MovePosition(transform.position + dir.normalized * speed * Time.deltaTime);
            //R.velocity = dir * speed;
        }
		bool RUN = h!=0||v!=0;
        A.SetAnim(RUN);
		}
	public void SetSpeed(float x){
		speed = x;
	}
	public void ResetSpeed(float x){
		speed = speedD;
	}
    public void StartDodge()
    {
        Debug.Log("start");
    }
    public void StopDodge()
    {
        Debug.Log("stop");

    }
  

}


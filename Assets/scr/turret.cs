using UnityEngine;
using System.Collections;

public class turret : MonoBehaviour {
	public GameObject PLAYAP; 
	public bullet bullet;
	public GameObject CN;
	public AudioClip GunShot;
	Rigidbody R;
	// Use this for initialization
	void Start () {
		R = CN.GetComponent<Rigidbody>();
		StartCoroutine("shoot");
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = PLAYAP.transform.position - CN.transform.position;
		pos.y = 0;
		Quaternion Q = Quaternion.LookRotation(pos);
		R.MoveRotation(Q);
	//CN.transform.LookAt(PLAYAP.transform,Vector3.up);
      
	}
IEnumerator shoot(){
		yield return new WaitForSeconds(3);
		AudioSource.PlayClipAtPoint(GunShot,transform.position);
		Vector3 P = (PLAYAP.transform.position - CN.transform.position).normalized;
		P.y = 0;
		bullet B =  Instantiate(bullet,CN.transform.position,Quaternion.identity) as bullet;
		B.GetComponent<Rigidbody>().velocity = P*1;

		StartCoroutine("shoot");
	}
}

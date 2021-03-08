using UnityEngine;
using System.Collections;

public class destroySelf : MonoBehaviour {
	public float delay = 0.2f;
	// Use this for initialization
	void Start () {
		Destroy(gameObject,delay);
	}
	

}

using UnityEngine;
using System.Collections;
public class bullet : MonoBehaviour
{
    public float acc = 30;
    public float limit = 120;
    public GameObject burst;
    public float damage;
    float speed;
    public float liftime = 1;
    public float sine;
    [HideInInspector]
    public string bIdentity = "a";
    // Use this for initialization
    void Start()
    {
		Debug.Log(bIdentity + "'s bullets");
        Destroy(gameObject, liftime);
    }

    // Update is called once per frame
    void Update()
    {
        if (speed <= limit)
        {
            speed += acc;
        }
        GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * speed;

        //GetComponent<TrailRenderer>().time = 0.02f;

    }

    void OnCollisionEnter(UnityEngine.Collision CL)
    {
        if (bIdentity != CL.gameObject.name)
        {
            OnKilled();
            Destroy(this.gameObject);
        }

    }

    public void OnKilled()
    {
        Instantiate(burst, transform.position, Quaternion.identity);
    }

    void OnDestroy()
    {
        OnKilled();
    }

}

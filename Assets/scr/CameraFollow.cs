using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{ public static CameraFollow ST;

	public Transform target;            // The position that that camera will be following.
	public float smoothing = 0.2f;        // The speed with which the camera will be following.
	
	Vector3 offset;                     // The initial offset from the target.
   
 

   Vector3 PlayerInView;

   float playerplacementx;
   float playerplacementy;
   Vector3 dirToMove;
    
   Vector3 CursorInView;
   float camminx;
    float ypos;


    public void repositionMe(Vector3 pos)
    {
        this.transform.position = pos;
    }

    void Start ()
	{	ST = this;
        // Calculate the initial offset.
        ypos = this.transform.position.y;


    }

	void FixedUpdate ()
    {   if(target!=null)
        {
           

            PlayerInView = Camera.main.WorldToViewportPoint(target.position);
           
            CursorInView = Camera.main.WorldToViewportPoint(target.GetComponent<Shoot>().crs);


        }
        if (!target)
            return;
        // Create a postion the camera is aiming for based on the offset from the target.
        Vector3 targetCamPos = target.position + offset;

        //Move Camera to make sure fov is correct

   
        playerplacementx = 1 - CursorInView.x;
        playerplacementy = 1 - CursorInView.y;
        playerplacementx = Mathf.Clamp(playerplacementx, 0.2f, 0.8f);

        playerplacementy = Mathf.Clamp(playerplacementy, 0.2f, 0.8f);
        if (PlayerInView.x > (playerplacementx+0.02f))
        {
            transform.position = Vector3.Lerp(transform.position, transform.position +new Vector3(0.2f, 0, 0), smoothing * Time.deltaTime);
        }
        if (PlayerInView.x < (playerplacementx-0.02f))
        {
            transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(-0.2f, 0, 0), smoothing * Time.deltaTime);
        }


        if (PlayerInView.y > playerplacementy+0.02f)
        {
            transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(0, 0,0.2f), smoothing * Time.deltaTime);
        }
        if (PlayerInView.y < playerplacementy-0.02f)
        {
            transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(0, 0, -0.2f), smoothing * Time.deltaTime);
        }


        transform.position = new Vector3(transform.position.x, ypos, transform.position.z);
        


    }
         


}
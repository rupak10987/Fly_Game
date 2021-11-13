using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{
    // Start is called before the first frame update
    public  float delay_btn_shots=0.02f;
    private float timer;
    public int mag_size = 20;
    private Vector3 cam_offset=Vector3.one;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool firing = Input.GetButton("Fire1");
      
        if(firing)
        {
           
            if (timer>delay_btn_shots)
            {
                timer = 0f;
                fire_bullet();
                cam_shake();
            }
            timer += Time.fixedDeltaTime;
        }
        timer += Time.fixedDeltaTime;
    }
    void fire_bullet()
    {
        GameObject finding = GameObject.Find("bullets");
        GameObject capsule = GameObject.Instantiate(finding);
        capsule.transform.forward = transform.forward-0.05f*(transform.up)+new Vector3(Random.Range(-0.03f,0.03f),0,0);
        capsule.transform.position = transform.position;
        capsule.AddComponent<bullet>();
        bullet rb= capsule.GetComponent<bullet>();
        rb.speed = 400;
        rb.xnot = capsule.transform.position.x;
        rb.ynot = capsule.transform.position.y;
        rb.znot = capsule.transform.position.z;
        Destroy(capsule,3f);
    }
    void cam_shake()
    {
        Vector3 vel = Vector3.zero;
        cam_offset = Camera.main.transform.position+5f*Camera.main.transform.forward;
        Camera.main.transform.position = Vector3.SmoothDamp(Camera.main.transform.position, cam_offset, ref vel, 0.09f);
       ///cam_offset = Vector3.SmoothDamp( cam_offset,Vector3.one, ref vel, 0.1f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{
    // Start is called before the first frame update
    public  float delay_btn_shots=0.02f;
    private float timer;
    public int mag_size = 20;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool firing = Input.GetButton("Fire1");
        if(firing)
        {
            if(timer>delay_btn_shots)
            {
                timer = 0f;
                fire_bullet();
            }
            timer += Time.fixedDeltaTime;
        }
        timer += Time.fixedDeltaTime;
    }
    void fire_bullet()
    {
        GameObject finding = GameObject.Find("bullets");
        GameObject capsule = GameObject.Instantiate(finding);
        capsule.transform.forward = transform.forward.normalized+new Vector3(Random.Range(-0.03f,0.03f),0,0);
        capsule.transform.position = transform.position;
        capsule.AddComponent<bullet>();
        bullet rb= capsule.GetComponent<bullet>();
        rb.speed = 400;
        rb.xnot = capsule.transform.position.x;
        rb.ynot = capsule.transform.position.y;
        rb.znot = capsule.transform.position.z;
        Destroy(capsule,3f);
    }
}

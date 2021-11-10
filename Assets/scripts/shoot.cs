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
        GameObject capsule = GameObject.Instantiate(finding,null);
        capsule.transform.position = transform.position + transform.forward *5 + transform.up*Random.Range(-1,1)+transform.right* Random.Range(-1, 1);
        capsule.transform.rotation = transform.rotation;
        capsule.AddComponent<Rigidbody>();
        Rigidbody rb= capsule.GetComponent<Rigidbody>();
        rb.AddForce((transform.forward+new Vector3(Random.Range(-0.02f,0.02f),0,0) )* 20000f);
        Destroy(capsule,1f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class release_bomb : MonoBehaviour
{
    // Start is called before the first frame update
    public float delay_btn_bombs = 2f;
    private float timer;
    public int mag_size = 4;
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool bombing = Input.GetKey(KeyCode.E);
        if (bombing)
        {
            if (timer > delay_btn_bombs)
            {
                timer = 0f;
                relez_mother_f_bomb();
            }
            timer += Time.fixedDeltaTime;
        }
        timer += Time.fixedDeltaTime;
    }
    void relez_mother_f_bomb()
    {
        GameObject finding = GameObject.Find("bom");
        GameObject littleboy = GameObject.Instantiate(finding);
        littleboy.transform.forward = transform.forward.normalized;
        littleboy.transform.position = transform.position-transform.up*1f;
        littleboy.transform.localScale=new Vector3(900,900,900);
       
        littleboy.AddComponent<bullet>();
        bullet rb = littleboy.GetComponent<bullet>();
        //getting the speed of the plane
        plane_controll plane_speed = GetComponent<plane_controll>();
        rb.speed = plane_speed.f_speed;
        rb.gravity = 50;
        rb.xnot = littleboy.transform.position.x;
        rb.ynot = littleboy.transform.position.y;
        rb.znot = littleboy.transform.position.z;
        Destroy(littleboy, 4f);
    }
}

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour {
    
    //public GameObject obj;
    public float speed;
    private Rigidbody rb;
    private Animation ani;
    public bool is_onland;
    private TimeCounter attm;
    public GameObject sword;
    private double damege;
    public double hpMax;
    private double point;
    private ParticleSystem ps;
    public AudioSource be_hint;

    //private TimeCounter jptm;
    void Start () {
        ani = GetComponent<Animation>();
        rb = GetComponent<Rigidbody>();
        attm = new TimeCounter();
        //jptm = new TimeCounter();
        is_onland = false;
        damege = 0;
        point = 0;
        ps = GetComponent<ParticleSystem>();
        ps.Stop();
    }

    class TimeCounter
    {
        bool isstop = true;
        double ans;
        double starttime;
        public void startC()
        {
            ans = 0;
            isstop = false;
            starttime = Time.time;
        }
        public void pauseC()
        {
            ans += Time.time - starttime;
            starttime = Time.time;
        }
        public void restartC()
        {
            starttime = Time.time;
        }
        public double countC()
        {
            return Time.time - starttime + ans;
        }
        public void endC()
        {
            isstop = true;
        }
        public bool is_stop()
        {
            return isstop;
        }
    }
    // Update is called once per frame
    void Update () {
        int t = (int)Time.timeSinceLevelLoad;
        /*
        //测试一下动作
        if(t%5 == 0)
        {
            ani.Play("Attack");
        }
        else if(t%5 == 1)
        {
            ani.Play("idle");
        }
        else if (t % 5 == 2)
        {
            ani.Play("Jump");
        }
        else if (t % 5 == 3)
        {
            ani.Play("Run");
        }
        else if (t % 5 == 4)
        {
            ani.Play("Walk");
        }
        */
        if (ani.IsPlaying("Attack") == true)
        {
            if (attm.countC() >= 0.5 && attm.countC() <= 0.6 || attm.countC() >= 1.2 && attm.countC() <= 1.3)
            {
                sword.GetComponent<Collider>().enabled = true;
            }
            else
            {
                sword.GetComponent<Collider>().enabled = false;
            }
            attm.endC();
        }
        else
        {
            sword.GetComponent<Collider>().enabled = false;

        }

        if(hpMax-damege<=0)
        {
            endgame();
        }

    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        if (Input.GetMouseButtonDown(0) && ani.IsPlaying("Attack") == false)
        {
            ani.Play("Attack");
            Debug.Log("Attack");
            attm.startC();
        }
        else if(ani.IsPlaying("Attack") == false)
        {
            if (Input.GetKeyDown(KeyCode.Space) && ani.IsPlaying("Jump") == false && is_onland == true)
            {
                Debug.Log("Jump");
                rb.velocity += new Vector3(moveHorizontal * speed, 10, moveVertical * speed);
                ani.Play("Jump");
                //jptm.startC();
            }
            else if (movement.magnitude < 0.001 && ani.IsPlaying("Jump") == false)
            {
                ani.Play("idle");
                //Debug.Log("idle");
            }
            else if (ani.IsPlaying("Jump") == false && is_onland == true)
            {
                ani.Play("Run");
                Debug.Log("Run");
                movement.Set(moveHorizontal, 0.0f, moveVertical);
                movement = movement.normalized * speed * Time.deltaTime;
                rb.MovePosition(transform.position + movement);
                /*
                if (is_onland == false)
                {
                    rb.MovePosition(transform.position + new Vector3(moveHorizontal, 0.2f, moveVertical));
                }
                */
                transform.LookAt(movement + transform.position);
            }
            else if(ani.IsPlaying("Jump") == false )
            {
                ani.Play("idle");
            }
            
            
        }
        
    }
    /*
    void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.name.CompareTo("land") == 0)
        {
            is_onland = true;
        }
    }
        

    void OnCollisionExit(Collision collider)
    {
        if (collider.gameObject.name.CompareTo("land") == 0)
        {
            is_onland = false;
        }
    }
    */

    public void somedamege(double d)
    {
        damege += d;
        ps.Stop();
        Invoke("psplay", 0.3f);
        Invoke("be_hint_play", 0.3f);
        Debug.Log("under attack2" + damege);
    }

    public void be_hint_play()
    {
        be_hint.Play();
    }
    public void addpoint(double d)
    {
        point += d;
    }

    void psplay()
    {
        ps.Play();
    }

    public double getpoint()
    {
        return point;
    }

    public double getHP()
    {
        return hpMax - damege;
    }

    public double getHPmax()
    {
        return hpMax;
    }

    void endgame()
    {
        UserData.ud.setftime(Time.timeSinceLevelLoad);
        UserData.ud.sethp(Math.Max(hpMax - damege, 0));
        UserData.ud.setpoint(point);
        Application.LoadLevel("WinScene");
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag.CompareTo("end") == 0 )
        {
            endgame();
        }
    }
}

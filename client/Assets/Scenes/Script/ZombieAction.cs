using UnityEngine;
using System.Collections;
using UnityEngine.AI;//先加入AI

public class ZombieAction : MonoBehaviour
{

    // Use this for initialization
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

    private Rigidbody rb;
    public float speed;
    private Animator an;
    public GameObject player;
    public GameObject real;
    private TimeCounter fltm;
    private Vector3 offsetF;
    private double damege;
    public double hpMax;
    private ParticleSystem ps;
    public AudioSource be_hint;

    private bool isDead;

    public bool getruntm()
    {
        return !fltm.is_stop();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        an = GetComponent<Animator>();
        fltm = new TimeCounter();
        ps = GetComponent <ParticleSystem>();
        ps.Stop();
        hpMax = 5;
        isDead = false;
    }


    // Update is called once per frame
    void Update()
    {
        if(rb.velocity.magnitude<0.1)
        {
            an.SetBool("isStop", true);
        }
        else
        {
            an.SetBool("isStop", false);
        }

        if(!fltm.is_stop())
        {
            real.GetComponent<Rigidbody>().AddForce(-offsetF * 10);
            real.GetComponent<NavMeshAgent>().enabled = false;
            if(fltm.countC()>=0.5)
            {
                Debug.Log("endC");
                real.GetComponent<NavMeshAgent>().enabled = true;
                fltm.endC();
            }
        }

        if(damege>= hpMax && !isDead)
        {
            an.SetBool("isDead", true);
            player.GetComponent<PlayerControl>().addpoint(1);
            isDead = true;
            Destroy(real, 0.633f);
            //Destroy(this, 0.633f);
        }
    }

    public void attack(double time)
    {
        if(damege < hpMax)
            Invoke("attack", (float)time);
    }
    public void attack()
    {
        an.SetTrigger("Attack");
        double l = (player.transform.position - real.transform.position).magnitude;
        if(l<=2.3)
        {
            player.GetComponent<PlayerControl>().somedamege(1);
        }
        Debug.Log("under attack ");
    }

    void FixedUpdate()
    {

    }

    void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject.tag.CompareTo("weapon") == 0 && fltm.is_stop())
        {
            Debug.Log("startC");
            damege += 1;
            ps.Stop();
            ps.Play();
            be_hint.Play();
            fltm.startC();
            Vector3 offsetf = new Vector3(player.transform.position.x - real.transform.position.x, 0, player.transform.position.z - real.transform.position.z).normalized;
            offsetf = (offsetf + new Vector3(0, -0, 0)).normalized;
            offsetF = offsetf;
        }
    }
}

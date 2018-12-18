using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;//先加入AI

public class AutoStart : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    public GameObject player;
    public GameObject wall;
    public GameObject zombie;
    private TimeCounter antm;
    private Rigidbody rb;
    private float speed;
    public float direction;

    bool island;
    bool isActive;

    private int i;

    public void Setisland(bool iss)
    {
        island = iss;
    }

    class TimeCounter
    {
        double ans;
        double starttime;
        public void startC()
        {
            ans = 0;
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
    }
    // Use this for initialization
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();//获取navmeshagent
        
    }
    void Start()
    {
        antm = new TimeCounter();
        antm.startC();
        rb = GetComponent<Rigidbody>();
        i = 0;
        navMeshAgent.enabled = false;
        island = false;
        isActive = false;
        speed = zombie.GetComponent<ZombieAction>().speed;
    }
    // Update is called once per frame
    void Update()
    {
        if(isActive)
        {
            if ((player.transform.position - transform.position).magnitude <= 2.5 && antm.countC() >= 1.2 && !zombie.GetComponent<ZombieAction>().getruntm())
            {
                zombie.GetComponent<ZombieAction>().attack(1);
                antm.startC();

            }
            else if (antm.countC() >= 1.2)
            {
                if (navMeshAgent.enabled)
                    navMeshAgent.SetDestination(player.transform.position);//设置导航的目标点
            }

            //island判断有bug,还没改
            if (/*island && */isActive)
            {
                //Debug.Log("onland");
                navMeshAgent.enabled = true;
            }
            else
            {
                //Debug.Log("notonland");
                //navMeshAgent.enabled = false;
            }


        }
        else
        {
            if((player.transform.position - transform.position).magnitude <= 10)
            {
                isActive = true;
            }

            if((int)(Time.timeSinceLevelLoad/5)%2==0)
            {
                float moveHorizontal = 1;
                float moveVertical = 0;

                Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
                movement.Set(moveHorizontal, 0.0f, moveVertical);
                movement = movement.normalized * speed * direction * Time.deltaTime;
                rb.MovePosition(transform.position + movement);
                transform.LookAt(movement + transform.position);
            }
            else
            {
                float moveHorizontal = -1;
                float moveVertical = 0;

                Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
                movement.Set(moveHorizontal, 0.0f, moveVertical);
                movement = movement.normalized * speed * direction * Time.deltaTime;
                rb.MovePosition(transform.position + movement);
                transform.LookAt(movement + transform.position);

            }
        }
        
    }

    

}
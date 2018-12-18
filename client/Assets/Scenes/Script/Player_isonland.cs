using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Player_isonland : MonoBehaviour
{
    private bool is_onland;
    public float speed;

    public bool getonland()
    {
        return is_onland;
    }
    // Use this for initialization
    void Start()
    {
        is_onland = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.CompareTo("samuzai") == 0)
        {
            other.gameObject.GetComponent<PlayerControl>().is_onland = true;
            is_onland = true;
            //Debug.Log("on");
        }

        if(other.gameObject.tag.CompareTo("zombie") == 0)
        {
            Debug.Log("on1");
            other.gameObject.GetComponent<ZombieAction>().real.GetComponent<AutoStart>().Setisland(true);
        }

    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name.CompareTo("samuzai") == 0)
        {
            other.gameObject.GetComponent<PlayerControl>().is_onland = true;
            is_onland = true;
            //Debug.Log("on");
        }

        if (other.gameObject.tag.CompareTo("zombie") == 0)
        {
            Debug.Log("on2");
            other.gameObject.GetComponent<ZombieAction>().real.GetComponent<AutoStart>().Setisland(true);
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.CompareTo("samuzai") == 0)
        {
            other.gameObject.GetComponent<PlayerControl>().is_onland = false;
            is_onland = false;
            //Debug.Log("off");
        }

        if (other.gameObject.tag.CompareTo("zombie") == 0)
        {
            Debug.Log("off");
            other.gameObject.GetComponent<ZombieAction>().real.GetComponent<AutoStart>().Setisland(false);
        }
    }

}

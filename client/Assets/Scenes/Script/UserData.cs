using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData : MonoBehaviour {

    public static UserData ud;
    private string _name;
    private double finaltime;
    private double point;
    private double hp;

    private void Awake()
    {
        if (ud == null)
        {
            DontDestroyOnLoad(gameObject);
            ud = this;
        }
        else if (ud != null)
        {
            Destroy(gameObject);
        }
    }

    public void setname(string n)
    {
        _name = n;
    }

    public string getname()
    {
        return _name;
    }

    public void setftime(double n)
    {
        finaltime = n;
    }

    public double getftime()
    {
        return finaltime;
    }

    public void setpoint(double n)
    {
        point = n;
    }

    public double getpoint()
    {
        return point;
    }

    public void sethp(double n)
    {
        hp = n;
    }

    public double gethp()
    {
        return hp;
    }
    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

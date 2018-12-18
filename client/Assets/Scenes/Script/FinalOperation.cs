using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class FinalOperation : MonoBehaviour {

    // Use this for initialization
    public Text finalText;

    void Start ()
    {
        finalText.text = "username:\t" + UserData.ud.getname() + "\n"
            + "finaltime:\t" + Math.Round(UserData.ud.getftime(), 4).ToString() + "\n"
            + "finalpoint:\t" + Math.Round(UserData.ud.getpoint(), 0).ToString() + "\n"
            + "finalhp:\t" + Math.Round(UserData.ud.gethp(), 0).ToString() + "\n";
        //RanklistData.rd.r.Add(new RanklistData.rankdata(UserData.ud.getname(), Math.Round(UserData.ud.getftime(), 4), Math.Round(UserData.ud.getpoint(), 0), Math.Round(UserData.ud.gethp(), 0)));
        //RanklistData.rd.SortList();
        CSocket cs = new CSocket();
        cs.Update_grade(UserData.ud.getname(), Math.Round(UserData.ud.gethp(), 0).ToString(), Math.Round(UserData.ud.getftime(), 4).ToString(), Math.Round(UserData.ud.getpoint(), 0).ToString());
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

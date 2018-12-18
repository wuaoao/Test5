using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ToRanklist : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Press()
    {
        bool flag = false;
        if (!flag)
        {
            CSocket cs = new CSocket();
            String[] msgs=cs.GetRanklist();
            RanklistData.rd.ClearList();
            for (int i=0;i<5;i++)
            {
                RanklistData.rd.r.Add(new RanklistData.rankdata(msgs[0 + i * 5], msgs[3 + i * 5], msgs[1 + i * 5], msgs[2 + i * 5], int.Parse(msgs[4 + i * 5])));   
            }

            RanklistData.rd.SortList();

            //password.text = count.ToString();
            Application.LoadLevel("Ranklist");
        }

    }
}

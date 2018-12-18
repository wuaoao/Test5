using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RanklistData : MonoBehaviour {

    public static RanklistData rd;
    public class rankdata
    {
        string username;
        string ftime;
        string point;
        string hp;
        int rank;
        public rankdata(string s, string d, string p, string _hp,int _rank)
        {
            username = s;
            ftime = d;
            point = p;
            hp = _hp;
            rank = _rank;
        }
        public string getname()
        {
            return username;
        }

        public string gettime()
        {
            return ftime;
        }

        public string gethp()
        {
            return hp;
        }

        public string getpoint()
        {
            return point;
        }

        public int getrank()
        {
            return rank;
        }

    }
    public List<rankdata> r = new List<rankdata>();
    /*
    private int SortListCompare(rankdata a, rankdata b)
    {
        if(a.gethp() > b.gethp())
        {
            return -1;
        }
        else if (a.gethp() < b.gethp())
        {
            return 1;
        }
        else if (a.getpoint() > b.getpoint())
        {
            return -1;
        }
        else if (a.getpoint() < b.getpoint())
        {
            return 1;
        }

        else if (a.gettime() < b.gettime())
        {
            return -1;
        }
        else if (a.gettime() > b.gettime())
        {
            return 1;
        }
        return 0;
    }
    */
    private int SortListCompareF(rankdata a, rankdata b)
    {
        if (a.getrank() < b.getrank())
        {
            return -1;
        }
        else if (a.getrank() > b.getrank())
        {
            return 1;
        }
        return 0;
    }

    public void SortList()
    {
        r.Sort(SortListCompareF);
    }

    public void ClearList()
    {
        r.Clear();
    }

    private void Awake()
    {
        if (rd == null)
        {
            DontDestroyOnLoad(gameObject);
            rd = this;
        }
        else if (rd != null)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

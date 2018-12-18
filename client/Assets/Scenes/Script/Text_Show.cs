using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Text_Show : MonoBehaviour {

    // Use this for initialization
    public Text countText;
    public Text hpText;
    public Text timeText;
    public GameObject player;

    void reflash_Text()
    {
        countText.text = "分数：" + Math.Round(player.GetComponent<PlayerControl>().getpoint(),0);
        hpText.text="生命："+ Math.Round(player.GetComponent<PlayerControl>().getHP(), 0) + "/"+ Math.Round(player.GetComponent<PlayerControl>().getHPmax(),0);
        timeText.text = "时间："+ Math.Round(Time.timeSinceLevelLoad, 4);
    }
    void Start () {
        reflash_Text();
    }
	
	// Update is called once per frame
	void Update () {
        reflash_Text();
    }
}

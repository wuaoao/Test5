using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginClick : MonoBehaviour {
    
    public Text username;
    public Text password;
    public GameObject passwordText;
    public GameObject hintText;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Press()
    {
        bool flag = true;
        CSocket cs = new CSocket();

        string pw = passwordText.GetComponent<InputField>().text;
        flag = cs.Login(username.text, pw);

        
        if (flag)
        {
            UserData.ud.setname(username.text);
            //password.text = count.ToString();
            Application.LoadLevel("SampleScene");
        }
        else
        {
            hintText.GetComponent<Text>().text = "password is wrong!!";
            hintText.SetActive(true);
        }
        
    }
    
}

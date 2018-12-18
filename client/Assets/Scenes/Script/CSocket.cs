using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net.Sockets;
using System.Net;
using System.Linq;
using System.Text;
using System.IO;
using Json;
using LitJson;

//using Newtonsoft.Json.Linq;

using System.Runtime.Serialization;

public class CSocket : MonoBehaviour {

    // Use this for initialization

    private static Socket ClientSocket;

    private String IP = "127.0.0.1";
    private int port = 3307;

    class Msg
    {
        public String action;
        public String uid;
        public String pw;
        public String point;
        public String ftime;
        public String hp;
        public Msg(String _a)
        {
            action = _a;
        }
        public Msg(String _a,String _u,String _p)
        {
            action = _a;
            uid = _u;
            pw = _p;
        }
        public Msg(String _a,String _u,String _p,String _h,String _f)
        {
            action = _a;
            uid = _u;
            point = _p;
            hp = _h;
            ftime = _f;
        }

    }

    private void Connect()
    {
        IPAddress ip = IPAddress.Parse(IP);  //将IP地址字符串转换成IPAddress实例
        ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//使用指定的地址簇协议、套接字类型和通信协议
        IPEndPoint endPoint = new IPEndPoint(ip, port); // 用指定的ip和端口号初始化IPEndPoint实例
        ClientSocket.Connect(endPoint);  //与远程主机建立连接
    }


    private void Send(Msg msg)
    {
        Console.WriteLine("开始发送消息");
        byte[] message = MToJson(msg);
        //byte[] message = Encoding.ASCII.GetBytes(msg);  //通信时实际发送的是字节数组，所以要将发送消息转换字节
        ClientSocket.Send(message);
        //Console.WriteLine("发送消息为:" + Encoding.ASCII.GetString(message));
    }


    private String Receive()
    {
        byte[] receive = new byte[1024];
        int length = ClientSocket.Receive(receive);  // length 接收字节数组长度
        //Console.WriteLine("接收消息为：" + Encoding.ASCII.GetString(receive));
        //return Encoding.ASCII.GetString(receive);
        Debug.Log("r "+length +" "+ Encoding.ASCII.GetString(receive,4,length-4));
        return Encoding.ASCII.GetString(receive, 4, length - 4);
    }

    private void Disconnect()
    {
        ClientSocket.Close();
    }

    private byte[] MToJson(Msg msg)
    {
        JsonData data = new JsonData();
        data["action"] = msg.action;
        data["uid"] = msg.uid;
        data["pw"] = msg.pw;
        data["point"] = msg.point;
        data["hp"] = msg.hp;
        data["ftime"] = msg.ftime;

        // 转换成json字符串
        string content = data.ToJson();
        int l = content.Length + 4;

        byte[] byte1 = new byte[] {
                        (byte) (l & 0xFF),
                        (byte) ((l >> 8) & 0xFF),
                        (byte) ((l >> 16) & 0xFF),
                        (byte) ((l >> 24) & 0xFF)
                    };
        

        byte[] byte2 = System.Text.Encoding.UTF8.GetBytes(content);

        byte[] byte3 = new byte[4 + byte2.Length];
        byte1.CopyTo(byte3, 0);
        byte2.CopyTo(byte3, 4);
        /*
        String json= "{\"action\":\""+msg.action+ "\",\"uid\":\"" + msg.uid + "\",\"pw\":\"" + msg.pw + "\",\"point\":\"" + msg.point + "\",\"hp\":\"" + msg.hp + "\",\"ftime\":\"" + msg.ftime + "\"}";

        int l=json.Length+4;

        byte[] byte1 = new byte[] {
                        (byte) (l & 0xFF),
                        (byte) ((l >> 8) & 0xFF),
                        (byte) ((l >> 16) & 0xFF),
                        (byte) ((l >> 24) & 0xFF)
                    };
        */
        return byte3;
    }

    private JsonObject BToJson(byte[] r)
    {
        string str = System.Text.Encoding.UTF8.GetString(r,4,r.Length-4);

        Debug.Log("btoj "+str + " end");

        // 将json字符串文件解析成需要的数据文件
        JsonObject jsonObj = new JsonObject(str);

        return jsonObj;
    }

    public bool Login(String username,String passwd)
    {
        Connect();
        Msg msg = new Msg("login", username, passwd);
        Send(msg);
        String str = Receive();
        //Array.Copy(b, 0, b, 4, b.Length-4);
        //Debug.Log(System.Text.Encoding.UTF8.GetString(b));
        JsonObject json = new JsonObject(str);
        Disconnect();


        if (json["msg"].ToString().CompareTo("\"ok\"") ==0)
        {
            Debug.Log("Login ok");
            return true;
        }
        else if (json["msg"].ToString().CompareTo("\"new\"") == 0)
        {
            Debug.Log("Login new");
            return true;
        }
        else if(json["msg"].ToString().CompareTo("\"no\"") == 0)
        {
            Debug.Log("Login no");
            return false;
        }
        Debug.Log(json["msg"].ToString());
        return false;
    }

    public void Update_grade(String uid,String hp,String ftime,String score)
    {
        Connect();
        Msg msg = new Msg("push_grade", uid, score, hp, ftime);
        //Debug.Log(hp + " " + ftime + " " + score);
        //Debug.Log(msg.point + " " + msg.hp + " " + msg.ftime);
        Send(msg);
        String str = Receive();
        JsonObject json = new JsonObject(str);
        Disconnect();
    }

    public String[] GetRanklist()
    {
        Connect();
        Msg msg = new Msg("get_ranklist");
        Send(msg);
        String str = Receive();
        JsonObject json = new JsonObject(str);
        Disconnect();

        Debug.Log(str);
        Debug.Log(json["rank"][0].ToString());
        //JsonObject json1 = json["rank"].ToString();
        String[] msgs = new String[25];
        for (int i=0;i<5;i++)
        {
            msgs[0 + i * 5] = json["rank"][i]["uid"].ToString().Replace("\"", "");
            msgs[1 + i * 5] = json["rank"][i]["score"].ToString().Replace("\"", "");
            msgs[2 + i * 5] = json["rank"][i]["hp"].ToString().Replace("\"", "");
            msgs[3 + i * 5] = json["rank"][i]["ftime"].ToString().Replace("\"", "");
            msgs[4 + i * 5] = json["rank"][i]["rank"].ToString().Replace("\"", "");
        }
        
        return msgs;

    }

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

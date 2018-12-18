# -*- coding: utf-8 -*-
"""
记录登录
"""
import dbctrl.saveobject
import timecontrol
import pubcore
import pubdefines
import pubglobalmanager
import math

class CDemo(dbctrl.saveobject.CSaveData):
    def GetKey(self):
        return "loginrecord"

    def Init(self):
        super(CDemo, self).Init()
        self.CheckTimer()

    def CheckTimer(self):
        timecontrol.Remove_Call_Out("loginrecord")
        pubdefines.FormatPrint("定时器统计：目前总连接记录 %s" % self.m_Data.get("total", 0))
        #pubdefines.FormatPrint("定时器统计：目前总连接记录2 %s" % self.m_Data.get("total2", 0))
        timecontrol.Call_Out(pubcore.Functor(self.CheckTimer), 300, "loginrecord")

    def NewItem(self):
        self.m_Data.setdefault("total", 0)
        #self.m_Data.setdefault("total2", 0)
        self.m_Data["total"] += 1
        #self.m_Data["total2"] += 1
        self.Save()


    def CalPos(self, oClient, dData):
        print ("CalPos")
        print (dData)
        r1 = dData["radius1"]
        r2 = dData["radius2"]
        lPos1 = dData["pos1"]
        lPos2 = dData["pos2"]
        print(r1+" "+r2+" "+lPos1+" "+lPos2)
        iDicstacne = "1"
        print(iDicstacne)
        dReturn = {
            "action": "show",
            "flag" : iDicstacne <= r1+r2,
        }
        print(dReturn)
        oClient.Send(dReturn)

    def Login(self, oClient, dData):
        print ("Login")
        print (dData)
        n_uid = dData["uid"]
        pw = dData["pw"]

        msg = ""

        sSql1 = "select * from user_msg where uid = "+n_uid
        sSql2 = "insert into user_msg (`uid`, `password`) values('%s', '%s')" % (n_uid, pw)

        is_exist = pubglobalmanager.CallManagerFunc("dbctrl", "Query", sSql1)
        if is_exist:
            if is_exist[0][1] == pw:
                msg = "ok"
            else:
                msg = "no"
        else:
            pubglobalmanager.CallManagerFunc("dbctrl", "ExecSql", sSql2)
            msg = "new"


        dReturn = {
            "action": "login",
            "flag" : "ok",
            "msg" : msg
        }
        print(dReturn)
        oClient.Send(dReturn)

    def Addscore(self, oClient, dData):
        print ("Addscore")
        print (dData)

        uid = dData["uid"]
        score = dData["point"]
        hp = dData["hp"]
        ftime = dData["ftime"]

        sSql1 = "insert into `ranklist` (`uid`, `hp`, `score`, `finaltime`) VALUES ('%s', '%s', '%s', '%s')" % (uid, hp, score, ftime)

        pubglobalmanager.CallManagerFunc("dbctrl", "ExecSql", sSql1)

        dReturn = {
            "action": "push_grade",
            "flag" : "ok"
        }
        print(dReturn)
        oClient.Send(dReturn)

    def GetRanklist(self, oClient, dData):
        print ("GetRanklist")
        print (dData)

        sSql1 = "SELECT * FROM db_demo.ranklist order by hp*2000+score*4000+finaltime desc LIMIT 5"

        rs = pubglobalmanager.CallManagerFunc("dbctrl", "Query", sSql1)

        ans = []
        i = 0
        for msg in rs:
            
            infm = {
                "uid" : msg[1],
                "hp" : msg[2],
                "score" : msg[3],
                "ftime" : msg[4],
                "rank" : i + 1
            }

            ans.append(infm)
            i = i + 1

        while( i < 5 ):
            
            infm = {
                "uid" : "----",
                "hp" : "----",
                "score" : "----",
                "ftime" : "----",
                "rank" : i + 1
            }

            ans.append(infm)
            i = i + 1

        dReturn = {
            "action": "get_ranklist",
            "flag" : "ok",

            "rank" : ans
        }
        print(dReturn)
        oClient.Send(dReturn)


def Init():
    if pubglobalmanager.GetGlobalManager("demo"):
        return
    oManger = CDemo()
    pubglobalmanager.SetGlobalManager("demo", oManger)
    oManger.Init()

def Record():
    pubglobalmanager.CallManagerFunc("demo", "NewItem")

def OnCommand(oClient, dData):
    pubglobalmanager.CallManagerFunc("demo", "CalPos", oClient, dData)

def OnLogin(oClient, dData):
    pubglobalmanager.CallManagerFunc("demo", "Login", oClient, dData)

def OnPush(oClient, dData):
    pubglobalmanager.CallManagerFunc("demo", "Addscore", oClient, dData)

def OnGetRanklist(oClient, dData):
    pubglobalmanager.CallManagerFunc("demo", "GetRanklist", oClient, dData)       

# -*- coding: utf-8 -*- 

import pubdefines
import mutexlock

class CCommandManager:
    def __init__(self):
        self.InitCommand()
    
    def InitCommand(self):
        self.m_CommandDict = {
        "updatepos" : ("demo","OnCommand"),
        "login" : ("demo","OnLogin"),
        "push_grade" : ("demo","OnPush"),
        "get_ranklist": ("demo","OnGetRanklist")
        }

    @mutexlock.AutoLock("oncommand")
    def OnCommand(self, oClient, dData):
        print("dData")
        print (dData)
        sAction = dData.get("action", "")
        print("sAction")
        print (sAction)
        if not sAction or not sAction in self.m_CommandDict:
            pubdefines.FormatPrint("未定义客户端的调用%s" % sAction)
            return 
        sImport, sFunc = self.m_CommandDict[sAction]
        oModule = __import__(sImport)
        oFunc = getattr(oModule, sFunc, None)
        if not oFunc:
            pubdefines.FormatPrint("客户端触发%s行为，%s模块未找到接口%s" % (sAction, sImport, sFunc))
            return
        oFunc(oClient, dData)

        '''
         or not sAction in self.m_CommandDict 
        '''
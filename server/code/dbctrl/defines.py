# -*- coding: utf-8  -*-
DBCTRL_MANAGER_NAME = "dbctrl"
DATABASE_NAME = "db_demo"


TABLE_GLOBAL = """
CREATE TABLE tbl_global
(
    rl_sName varchar(30) NOT NULL COMMENT '名字',
    rl_dmSaveTime datetime NOT NULL COMMENT '存档时间',
    rl_sData MEDIUMBLOB NOT NULL COMMENT '数据块',
    PRIMARY KEY (rl_sName)
)ENGINE=InnoDB default charset=utf8 comment='全局数据'
"""

UESR_MSG = """
CREATE TABLE user_msg 
(
  uid varchar(30) NOT NULL COMMENT '用户名',
  password varchar(30) COMMENT '密码',
  PRIMARY KEY (uid)
)ENGINE=InnoDB default charset=utf8 comment='用户'
"""

RANKLIST = """
CREATE TABLE ranklist
(
	id int NOT NULL  AUTO_INCREMENT COMMENT 'id',
    uid varchar(30) NOT NULL COMMENT '名字',
    hp double NOT NULL COMMENT '剩余生命',
    score double NOT NULL COMMENT '分数',
    finaltime double NOT NULL COMMENT '所用时间',
    PRIMARY KEY (`id`),
  	CONSTRAINT `uid`
    FOREIGN KEY (`uid`)
    REFERENCES `user_msg` (`uid`)
)ENGINE=InnoDB default charset=utf8 comment='排名'
"""


TABLE_ALL = {
    "tbl_global":TABLE_GLOBAL,
    "user_msg":UESR_MSG,
    "ranklist":RANKLIST
}
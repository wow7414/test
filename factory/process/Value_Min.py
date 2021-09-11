import pymssql  
import time
from datetime import datetime

from pymssql import _mssql
from pymssql import _pymssql
import uuid
import decimal
_mssql.__version__
decimal.__version__

class db():
    def __init__(self,i):
        if(i == 1):
            self.database = 'Runtime'
        else:
            self.database = 'ZDB'
            
    def conn(self):
        self.conn = pymssql.connect(server='192.168.10.230', user='sa', password='#HQTP@9090', database = self.database)  
        self.cursor = self.conn.cursor()
    def get_datatable(self,sql):
        self.conn()
        self.cursor.execute(sql)
        x = self.cursor.fetchall()
        self.close()
        return x
    def run_cmd(self,sql):
        self.conn()
        self.cursor.execute(sql)   
        self.conn.commit()
        self.close()
    def close(self):
        self.cursor.close()
        self.conn.close()
        
while True:
    sql = "select * from TagList"
    data = db(2)
    try:
        datatable = data.get_datatable(sql)
        for i in datatable:
            if(i[6] == True):
                data = db(1)
                sql = "SELECT top 1* FROM History WHERE TagName = '%s' AND DateTime >= DATEADD(n,DATEDIFF(n,0,getdate()),0) AND DateTime <= DATEADD(hh,1,DATEADD(hh,DATEDIFF(hh,0,getdate()),0))" % i[1]
                dt = data.get_datatable(sql)
                for j in dt:
                    data = db(2)
                    sql = "INSERT INTO Value_Min values('%s','%s','%s', %s)" % (j[0],j[1],j[25],j[2])
                    data.run_cmd(sql)
    except:
        print("連接不到資料庫")
    
    print('目前時間:'+datetime.now().strftime('%Y-%m-%d %H:%M:%S'))
    s = int(datetime.now().strftime('%S'))
    time.sleep(62-s)

    
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
        

sql = "select * from TagList"
data = db(2)
try:
    datatable = data.get_datatable(sql)
    for i in datatable:
        if(i[5] == True):
            #加上現在時間
            x = int(datetime.now().strftime('%H'))
            for j in range(0,721+x):
                data = db(1)
                sql = "SELECT top 1* FROM History WHERE TagName = '%s' AND DateTime <= DATEADD(hh,%d,DATEADD(hh,DATEDIFF(hh,0,convert(datetime,convert(date,getdate()-30))),0))" % (i[1],j+1)
                dt = data.get_datatable(sql)
                for k in dt:
                    data = db(2)
                    if(k[2] == None):
                        name = 0
                    else:
                        name = k[2]
                    print(k[0],k[25])
                    sql = "IF NOT EXISTS (SELECT * FROM Value_Hour WHERE TagName = '%s' AND DataDateTime ='%s') BEGIN INSERT INTO Value_Hour VALUES ('%s','%s','%s',%s) END" % (k[25],k[0],k[0],k[1],k[25],name)
                    data.run_cmd(sql)
except:
    print("連接不到資料庫")
x = input('執行完畢，請自行關閉')


    
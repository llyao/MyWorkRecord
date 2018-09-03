#coding=utf-8

import sys
import os
import codecs
#x 表示要更新的文件名称
#y 表示要被替换的内容
#z 表示 替换后的内容
#s 默认参数为 1 表示只替换第一个匹配到的字符串
# 如果参数为 s = 'g' 则表示全文替换
def string_switch(x,y,z):
    with open(x, "r") as f:
        #readlines以列表的形式将文件读出
        lines = f.readlines()
 
    with codecs.open(proto_name+"_Register.cs", "w") as f_w:
        #定义一个数字，用来记录在读取文件时在列表中的位置
      
        for line in lines:
            if y in line:
                line = line.replace(y,z)
            f_w.write(line)
if __name__ == '__main__' :
    """入口"""
    print 'system encoding: ',sys.getdefaultencoding()
    print  os.getcwd()
   
    proto_name =  sys.argv[1]
    #proto_name = "xx"
    print  proto_name
    string_switch("../AutoGenConfigTemplate.cs","Template",proto_name)
      
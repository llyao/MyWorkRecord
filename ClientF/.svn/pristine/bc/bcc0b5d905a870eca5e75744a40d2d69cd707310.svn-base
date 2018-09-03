#coding=utf-8

import sys
import os
import codecs
#x 表示要更新的文件名称
#y 表示要被替换的内容
#z 表示 替换后的内容
#s 默认参数为 1 表示只替换第一个匹配到的字符串
# 如果参数为 s = 'g' 则表示全文替换

if __name__ == '__main__' :
    """入口"""
    print 'system encoding: ',sys.getdefaultencoding()
    print  os.getcwd()
   
    proto_file_path =  sys.argv[1]

    f = open("../ConfigManager.cs","r")
    # print(f.read())
    get = f.read()
    f.close()
    data=""
    try:
        path=proto_file_path.decode('utf-8')
    except BaseException, e:
        print len(bytes(proto_file_path[1:]))
        path="."+bytes(proto_file_path[1:])[2:].decode('utf-8')
    print( path)
        #path=xls_file_path[1:].decode('utf-8')
    with open(path, "r") as f:
        #readlines以列表的形式将文件读出
        lines = f.readlines()
        for line in lines:
            index=line.find(".proto")
            line2=line[:index]
            data+="\n\t\t"+line2+"_Register.LoadFromDisk();"
    data+="\n\t"
    start=get.find("Init")
    start=get.find("}",start)
    data=get[:start] + data+ get[start:]
    #print data
    file_name = "./ConfigManager.cs"
    file = open(file_name, 'w')
    file.write(data)
    file.close()

#!/usr/bin/python
#-*-coding:utf-8-*-       #指定编码格式，python默认unicode编码
#coding=utf-8
import os
import xls2protobuf_v3
import subprocess


directory = "./xls2proto"
directoryMain = "../"


cwd = os.getcwd()  #获取当前目录即dir目录下
print("------------------------current working directory------------------")
 
def deleteFile():
    """删除小于minSize的文件（单位：K）"""
    files = os.listdir(os.getcwd())  #列出目录下的文件
    for file in files:
        os.remove(file)    #删除文件
        print(file + " deleted")
    return
 
if __name__ == '__main__' :
    os.chdir(directory)  #切换到directory目录
    deleteFile()
    f = open("../ConvertList.txt","r")
    # print(f.read())
    get = f.read()
    #get = get.encode('utf-8').decode('utf-8-sig')
    result = get.split('\n')
    for i in range(len(result)):
        #print(result[i])
        print("******")
        if i==0:
            xls2protobuf_v3.Xls2Protobuf(result[i].decode('utf-8')[1:])
        else:
            xls2protobuf_v3.Xls2Protobuf(result[i].decode('utf-8'))

    f.close()
    os.chdir(directoryMain)
    child = subprocess.Popen('ResConvertAllPython2.bat', shell=False)









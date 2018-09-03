import os
import sys
import hashlib

def walk_dir(path,fileinfo,topdown=True):
	md5v = sumfile(path)
	fileinfo.write(str(md5v))

def sumfile(fpath):
    f=open(fpath,"rb")
    content=f.read()
    l=len(content)
    f.close()
    return l

if __name__ == '__main__':
	argv = sys.argv
	dir=argv[1]
	txt = argv[2]
	print dir
	print txt
	fileinfo = open(txt,'w')
   	walk_dir(dir,fileinfo)
    
    
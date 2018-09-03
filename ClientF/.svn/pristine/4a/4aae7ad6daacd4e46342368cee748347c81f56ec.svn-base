import os
import sys
import hashlib

def walk_dir(dir,txt,filters,topdown=True):
    result=''
    for root, dirs, files in os.walk(dir, topdown):
        te=os.path.split(root)
        if(te[1]=="bin" or te[1]==".svn"):
        	dirs[:]=[]
        	continue
        for name in files:
        	if(name.endswith(".manifest") or name.endswith(".meta")):
        		continue
        	path = os.path.join(root,name)
        	f=open(path,"rb")
        	content=f.read()
        	l=len(content)
        	f.close()
        	m=hashlib.md5()
        	m.update(content)
        	md5v=m.hexdigest()[:9]

        	newpath = path.replace(dir,'')
        	newpath = newpath.replace('\\','/')
        	
        	b=0;
        	for i in filters:
        		b=newpath.find(i);
        		if(b==0):
        			break
        	if(b==0):	        	
        		result+= newpath + ':' + md5v+':'+str(l)+ '\n'
    fileinfo = open(txt,'w')
    fileinfo.write(result)
    fileinfo.close()

if __name__ == '__main__':
	argv = sys.argv
	dir=argv[1]
	txt = argv[2]
	
	filters=[]
	if(len(argv)>3):
		filters=argv[3].split(',')
	print(txt)
	
	walk_dir(dir,txt,filters)
    
    

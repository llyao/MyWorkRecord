@echo off


::-------------------------------------
:: ת����������
::-------------------------------------

echo.
echo =========Compilation  All xls=========


::---------------------------------------------------
::��һ������xls����xls_deploy_toolת��bin��proto
::---------------------------------------------------
set STEP1_XLS2PROTO_PATH=xls2proto

@echo on
cd %STEP1_XLS2PROTO_PATH%

@echo off
echo TRY TO DELETE TEMP FILES:
del *_pb2.py
del *_pb2.pyc
del *.proto
del *.bin
del *.log
del *.txt

@echo on

for /f "delims=" %%i in (..\ConvertList.txt) do   python ..\xls2protobuf_v3_Client.py %%i


::---------------------------------------------------
::�ڶ�������proto�����cs
::---------------------------------------------------
cd ..

set STEP2_PROTO2CS_PATH=.\proto2cs
set PROTO_DESC=proto.protodesc
set SRC_OUT=.\


::for cpp
set STEP2_PROTO2CPP_PATH=.\proto2cpp

cd %STEP2_PROTO2CS_PATH%

@echo off
echo TRY TO DELETE TEMP FILES:
del *.cs
del *.protodesc
del *.txt

@echo on
dir ..\%STEP1_XLS2PROTO_PATH%\*.proto /b  > protolist.txt

@echo on
for /f "delims=." %%i in (protolist.txt) do protoc --descriptor_set_out=%%i.protodesc --proto_path=..\%STEP1_XLS2PROTO_PATH% ..\%STEP1_XLS2PROTO_PATH%\%%i.proto
::for /f "delims=." %%i in (protolist.txt) do ProtoGen\protogen -i:%PROTO_DESC% -o:%%i.cs
for /f "delims=." %%i in (protolist.txt) do protoc --proto_path=..\%STEP1_XLS2PROTO_PATH% ..\%STEP1_XLS2PROTO_PATH%\%%i.proto --csharp_out=%SRC_OUT%
for /f "delims=." %%i in (protolist.txt) do  python ..\GenRegisterCS.py %%i


::for cpp
for /f "delims=." %%i in (protolist.txt) do protoc --proto_path=..\%STEP1_XLS2PROTO_PATH% ..\%STEP1_XLS2PROTO_PATH%\%%i.proto --cpp_out=%SRC_OUT%
python ..\GenRegisterCSManager.py protolist.txt
cd ..

move /y %STEP2_PROTO2CS_PATH%\*pb.cc %STEP2_PROTO2CPP_PATH%
move /y %STEP2_PROTO2CS_PATH%\*pb.h %STEP2_PROTO2CPP_PATH%

::---------------------------------------------------
::����������bin��cs����Assets��
::---------------------------------------------------

@echo off
set OUT_PATH=..\..\TestGame\Assets
set DATA_DEST=..\..\ReleaseResource\data\config
set CS_DEST=Scripts\Hot\Config
  
del %DATA_DEST%\*.bin
del %OUT_PATH%\%CS_DEST%\*.cs
@echo on
copy %STEP1_XLS2PROTO_PATH%\*.bin %DATA_DEST%
copy %STEP2_PROTO2CS_PATH%\*.cs %OUT_PATH%\%CS_DEST%

::---------------------------------------------------
::���Ĳ�������м��ļ�
::---------------------------------------------------
@echo off
echo TRY TO DELETE TEMP FILES:
cd %STEP1_XLS2PROTO_PATH%
del *_pb2.py
del *_pb2.pyc
:: del *.proto
:: del *.bin
:: del *.log
:: del *.txt
:: cd ..
:: cd %STEP2_PROTO2CS_PATH%
:: del *.cs
:: del *.protodesc
:: del *.txt
:: cd ..

::---------------------------------------------------
::���岽������
::---------------------------------------------------
cd ..

@echo on


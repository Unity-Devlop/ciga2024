set WORKSPACE=..
set LUBAN_DLL=%WORKSPACE%\Tools\Luban\Luban.dll
set CONF_ROOT=.
set TARGET_GROUP=client
set CODE_TARGET=cs-simple-json
set DATA_TARGET=json

dotnet %LUBAN_DLL% ^
    -t %TARGET_GROUP% ^
    -d %DATA_TARGET% ^
    -c %CODE_TARGET% ^
    --conf %CONF_ROOT%\luban.conf ^
    -x outputDataDir=../Assets/Resources/Tables/ ^
    -x outputCodeDir=../Assets/Game/Runtime/Tables/Gen

pause
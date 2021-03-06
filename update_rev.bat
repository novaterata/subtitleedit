@ECHO OFF
SETLOCAL

PUSHD %~dp0

IF EXIST "src\UpdateAssemblyInfo\bin\Release\UpdateAssemblyInfo.exe" (
  "src\UpdateAssemblyInfo\bin\Release\UpdateAssemblyInfo.exe" "src\Properties\AssemblyInfo.cs.template" "src\Properties\AssemblyInfo.cs"
) ELSE (
  "src\UpdateAssemblyInfo\bin\Debug\UpdateAssemblyInfo.exe" "src\Properties\AssemblyInfo.cs.template" "src\Properties\AssemblyInfo.cs"
)
IF %ERRORLEVEL% NEQ 0 GOTO SubError


:END
POPD
ENDLOCAL
EXIT /B

:SubError
ECHO Something went wrong when generating the revision number.
EXIT /B

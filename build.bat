@echo off
REM ******************************************************
REM * VS Buid Script when using GhostScript for chm file *
REM * and building nuget packages			 *
REM ******************************************************
REM ******************************************************
REM USE Build Script below
REM $(SolutionDir)postbuild.bat $(SolutionDir) $(ProjectDir) $(ConfigurationName) $(ProjectName)
REM ******************************************************
SET SolutionDir=%1
SET ProjectDir=%2
SET ConfigurationName=%3
SET HELPFILENAME=%4
SET DEBUG="Debug"
SET RELEASE="Release"

cd "%ProjectDir%"
copy /Y "%SolutionDir%Help\%HELPFILENAME%.chm" "%ProjectDir%bin\%ConfigurationName%\%HELPFILENAME%.chm"
cd "%ProjectDir%"
del /Q %ProjectDir%*.nupkg

if "%ConfigurationName%" == %DEBUG% (
	echo "nuget Dev packing"
	nuget pack
)

if "%ConfigurationName%" == %RELEASE% (
	echo "nuget Production Packing"
	nuget pack -Prop Configuration=Release
)
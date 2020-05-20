@echo off
cls

REM Delete old packages
if exist *.nupkg del *.nupkg

REM Generate NuGet package
%cd%\tools\nuget.exe pack FluentNHibernate.AspNetCore.Identity.nuspec

REM Push to GutHub Packages
dotnet nuget push *.nupkg --source "github"
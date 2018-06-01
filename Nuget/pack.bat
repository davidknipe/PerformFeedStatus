@Echo Removing old files
del Package\lib /s /q
del Package\content /s /q
del Package\tools /s /q

@Echo Setting up folder structure
md Package\lib\net45\
md Package\tools\
md Package\content\modules\_protected\PerformFeed.UI\

@Echo Copying new files
copy ..\PerformFeedStatus\bin\Release\PerformFeedStatus.dll Package\lib\net45\
xcopy ..\PerformFeedStatus\modules\_protected\PerformFeed.UI\*.* Package\content\modules\_protected\PerformFeed.UI\ /S

@Echo Packing files
"..\NugetExe\nuget.exe" pack Package\PerformFeedStatus.nuspec

@Echo Moving package
move /Y *.nupkg c:\_nugetFeed\

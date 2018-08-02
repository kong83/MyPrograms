msbuild SurgeryHelper.sln /p:Configuration=Release /t:Clean
msbuild SurgeryHelper.sln /p:Configuration=Release

ren SurgeryHelper\bin\Release\ElectronicIntern.exe Temp.exe

ILMerge.exe SurgeryHelper\bin\Release\Temp.exe SurgeryHelper\bin\Release\zlib.net.dll SurgeryHelper\bin\Release\Istrib.Sound.dll SurgeryHelper\bin\Release\Office.dll SurgeryHelper\bin\Release\Microsoft.Office.Interop.Word.dll SurgeryHelper\bin\Release\Microsoft.Office.Interop.Excel.dll /t:winexe /allowDup /out:SurgeryHelper\bin\Release\ElectronicIntern.exe
del SurgeryHelper\bin\Release\Temp.exe
del SurgeryHelper\bin\Release\zlib.net.dll
del SurgeryHelper\bin\Release\Istrib.Sound.dll
del SurgeryHelper\bin\Release\Office.dll
del SurgeryHelper\bin\Release\Microsoft.Office.Interop.Word.dll
del SurgeryHelper\bin\Release\Microsoft.Office.Interop.Excel.dll
BASENAME=UpdateDatabase

DEFTARGETTYPE=CSEXE

SRC32_CS = \
    $(PROJPATH)\UNITS\bv7\Multimode_V14\UpdateDatabase\Properties\AssemblyInfo.cs \
    $(PROJPATH)\UNITS\bv7\Multimode_V14\UpdateDatabase\Configuration.cs \
    $(PROJPATH)\UNITS\bv7\Multimode_V14\UpdateDatabase\Configuration.Designer.cs \
    $(PROJPATH)\UNITS\bv7\Multimode_V14\UpdateDatabase\MyColumn.cs \
    $(PROJPATH)\UNITS\bv7\Multimode_V14\UpdateDatabase\MyForeignKey.cs \
    $(PROJPATH)\UNITS\bv7\Multimode_V14\UpdateDatabase\MyIndex.cs \
    $(PROJPATH)\UNITS\bv7\Multimode_V14\UpdateDatabase\MySmoObjectBase.cs \
    $(PROJPATH)\UNITS\bv7\Multimode_V14\UpdateDatabase\MyStoredProcedure.cs \
    $(PROJPATH)\UNITS\bv7\Multimode_V14\UpdateDatabase\MyTable.cs \
    $(PROJPATH)\UNITS\bv7\Multimode_V14\UpdateDatabase\MyTrigger.cs \
    $(PROJPATH)\UNITS\bv7\Multimode_V14\UpdateDatabase\Database.cs \
    $(PROJPATH)\UNITS\bv7\Multimode_V14\UpdateDatabase\DatabaseEngine.cs \
    $(PROJPATH)\UNITS\bv7\Multimode_V14\UpdateDatabase\Program.cs \
    $(PROJPATH)\UNITS\bv7\Multimode_V14\UpdateDatabase\UpgradeScriptGenerator.cs \
    $(PROJPATH)\UNITS\bv7\Multimode_V14\UpdateDatabase\SmoWorkarounds.cs \
    $(PROJPATH)\UNITS\bv7\Multimode_V14\UpdateDatabase\PathsEngine.cs \
    $(PROJPATH)\UNITS\bv7\GlobalAssemblyInfo.cs \

REF32_CS= \
  /r:$(SYSTEMROOT)\assembly\GAC_MSIL\Microsoft.SqlServer.ConnectionInfo\9.0.242.0__89845dcd8080cc91\Microsoft.SqlServer.ConnectionInfo.dll \
  /r:$(SYSTEMROOT)\assembly\GAC_MSIL\Microsoft.SqlServer.Smo\9.0.242.0__89845dcd8080cc91\Microsoft.SqlServer.Smo.dll \
  /r:$(SYSTEMROOT)\assembly\GAC_MSIL\Microsoft.SqlServer.SmoEnum\9.0.242.0__89845dcd8080cc91\Microsoft.SqlServer.SmoEnum.dll \
  /r:$(SYSTEMROOT)\assembly\GAC_MSIL\Microsoft.SqlServer.SqlEnum\9.0.242.0__89845dcd8080cc91\Microsoft.SqlServer.SqlEnum.dll \
  /r:$(SYSTEMROOT)\assembly\GAC_MSIL\Microsoft.SqlServer.ServiceBrokerEnum\9.0.242.0__89845dcd8080cc91\Microsoft.SqlServer.ServiceBrokerEnum.dll \
  /r:$(SYSTEMROOT)\Microsoft.NET\Framework\v2.0.50727\System.dll \
  /r:$(SYSTEMROOT)\Microsoft.NET\Framework\v2.0.50727\System.Configuration.dll \
  /r:"$(PROGRAMFILES)\Reference Assemblies\Microsoft\Framework\v3.5\System.Core.dll" \
  /r:$(SYSTEMROOT)\Microsoft.NET\Framework\v2.0.50727\System.Data.dll \
  /r:$(SYSTEMROOT)\Microsoft.NET\Framework\v2.0.50727\System.Xml.dll \
  

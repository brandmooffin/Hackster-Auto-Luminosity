<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="AutoLuminosityService.Azure" generation="1" functional="0" release="0" Id="0ebecc7f-21dd-4ee7-8236-16b9b60e2806" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="AutoLuminosityService.AzureGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="AutoLuminosityService:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/AutoLuminosityService.Azure/AutoLuminosityService.AzureGroup/LB:AutoLuminosityService:Endpoint1" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="AutoLuminosityService:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/AutoLuminosityService.Azure/AutoLuminosityService.AzureGroup/MapAutoLuminosityService:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="AutoLuminosityServiceInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/AutoLuminosityService.Azure/AutoLuminosityService.AzureGroup/MapAutoLuminosityServiceInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:AutoLuminosityService:Endpoint1">
          <toPorts>
            <inPortMoniker name="/AutoLuminosityService.Azure/AutoLuminosityService.AzureGroup/AutoLuminosityService/Endpoint1" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="MapAutoLuminosityService:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/AutoLuminosityService.Azure/AutoLuminosityService.AzureGroup/AutoLuminosityService/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapAutoLuminosityServiceInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/AutoLuminosityService.Azure/AutoLuminosityService.AzureGroup/AutoLuminosityServiceInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="AutoLuminosityService" generation="1" functional="0" release="0" software="C:\Projects\Auto Luminosity\AutoLuminosityService\AutoLuminosityService.Azure\csx\Release\roles\AutoLuminosityService" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="-1" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="80" />
            </componentports>
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;AutoLuminosityService&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;AutoLuminosityService&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/AutoLuminosityService.Azure/AutoLuminosityService.AzureGroup/AutoLuminosityServiceInstances" />
            <sCSPolicyUpdateDomainMoniker name="/AutoLuminosityService.Azure/AutoLuminosityService.AzureGroup/AutoLuminosityServiceUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/AutoLuminosityService.Azure/AutoLuminosityService.AzureGroup/AutoLuminosityServiceFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="AutoLuminosityServiceUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="AutoLuminosityServiceFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="AutoLuminosityServiceInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="480f6502-5b84-492c-804d-40a977624a9c" ref="Microsoft.RedDog.Contract\ServiceContract\AutoLuminosityService.AzureContract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="aa7b426c-5065-4948-8b68-68138b5abcb5" ref="Microsoft.RedDog.Contract\Interface\AutoLuminosityService:Endpoint1@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/AutoLuminosityService.Azure/AutoLuminosityService.AzureGroup/AutoLuminosityService:Endpoint1" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>
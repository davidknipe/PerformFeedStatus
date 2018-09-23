
██████╗ ███████╗ █████╗ ██████╗     ███╗   ███╗███████╗██╗
██╔══██╗██╔════╝██╔══██╗██╔══██╗    ████╗ ████║██╔════╝██║
██████╔╝█████╗  ███████║██║  ██║    ██╔████╔██║█████╗  ██║
██╔══██╗██╔══╝  ██╔══██║██║  ██║    ██║╚██╔╝██║██╔══╝  ╚═╝
██║  ██║███████╗██║  ██║██████╔╝    ██║ ╚═╝ ██║███████╗██╗
╚═╝  ╚═╝╚══════╝╚═╝  ╚═╝╚═════╝     ╚═╝     ╚═╝╚══════╝╚═╝
                                                          
-------------------------------------
- Perform Feed status install notes -
-------------------------------------

Please note: This pacakage is built against v2.0 of EPiServer.Personalization.Commerce. However the package has 
a number of updates since v2.0 but there is no assembly redirect as for most Episerver assemblies.

So I recommend adding an assembly redirect into web.config as follows:

<configuration>
  <runtime>
    <assemblyBinding xmlns="urn:schemas-microsoft-com:asm.v1">
      <dependentAssembly>
        <assemblyIdentity name="EPiServer.Personalization.Commerce" publicKeyToken="8fe83dea738b45b7" culture="neutral"/>
        <bindingRedirect oldVersion="0.0.0.0-2.0.5.0" newVersion="2.0.5.0"/>
      </dependentAssembly>
    </assemblyBinding>
  </runtime>
</configuration>

The example redirects to v2.0.5 which is the current released version of EPiServer.Personalization.Commerce
SET AUTOREST=..\..\solutions\packages\autorest.0.17.3\tools\AutoRest.exe

%AUTOREST% -CodeGenerator CSharp -Modeler Swagger -AddCredentials true -Input http://localhost/Performance.AutoRest.Facade/swagger/docs/v1 -Namespace Services.AutoRestClients.Facade -Output Facade.Generated

PAUSE

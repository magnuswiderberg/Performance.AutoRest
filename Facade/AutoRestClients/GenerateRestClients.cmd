SET AUTOREST=..\..\solutions\packages\autorest.0.17.3\tools\AutoRest.exe

%AUTOREST% -CodeGenerator CSharp -Modeler Swagger -AddCredentials true -Input http://localhost/Performance.AutoRest.Api/swagger/docs/v1 -Namespace Facade.AutoRestClients.Api -Output Api.Generated

PAUSE

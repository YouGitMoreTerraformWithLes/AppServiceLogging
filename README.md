# AppServiceLogging
A sample ASP.NET Core 3.1 web application that implements application logging into Azure Application Insights

## Based on this example
Only real difference is I configured the Application Insights configuration field 
to use the default configuration field that is in Azure Web Application Configuration tab.
"APPINSIGHTS_INSTRUMENTATIONKEY"\
https://referbruv.com/blog/posts/integrating-aspnet-core-api-logs-to-azure-application-insights-via-ilogger

I also had to downgrade the Microsoft.Extensions.Logging.ApplicationInsights package to 2.13.1 in order to 
eliminate a version compatibility warning in Visual Studio

## Application Insights Query
traces\
| order by timestamp desc\
| extend cd = parse_json(customDimensions)\
| where cd.CategoryName == "AppServiceLogging.Controllers.HealthController"

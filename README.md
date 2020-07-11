# AppServiceLogging
A sample .NET Core 3.1/ASP.NET that implements application logging into Azure Application Insights

## Application Insights Query
traces
| order by timestamp desc
| extend cd = parse_json(customDimensions)
| where cd.CategoryName == "AppServiceLogging.Controllers.HealthController"

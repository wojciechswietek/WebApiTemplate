# Web Api Template
My starting point for creating C#/ASP.NET Core Web Api.

# WeatherForecast.csproj

ASP.NET Core Web API with basic CRUD endpoints and MSSQL database.


## WeatherForecast.Tests.csproj

Integration test suite to check:
* automatic migrations,
* database creation,
* api controller actions.

Test project uses test double. All operations are carried out on separate database. Production DB remains unchanged.

## ci.yml
[![Continuous Inegration](https://github.com/wojciechswietek/WebApiTemplate/actions/workflows/ci.yml/badge.svg)](https://github.com/wojciechswietek/WebApiTemplate/actions/workflows/ci.yml)
> on branch **dev** push
1. Creates ubuntu container.
2. Starts MSSQL server & creates new database.
3. Builds weatherforecast app.
4. Runs tests.

## cd.yml
[![Continuous Delivery](https://github.com/wojciechswietek/WebApiTemplate/actions/workflows/cd.yml/badge.svg)](https://github.com/wojciechswietek/WebApiTemplate/actions/workflows/cd.yml)
> on branch **main** push
1. Creates ubuntu container.
2. Starts MSSQL server & creates new database.
3. Builds weatherforecast app.
4. Deploys app to Microsoft Azure.

## Licensing

This project is licensed under GNU General Public License v3.0.


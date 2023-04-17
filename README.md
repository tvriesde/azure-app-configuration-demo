# azure-app-configuration-demo

.NET core app based on https://learn.microsoft.com/en-us/azure/azure-app-configuration/quickstart-feature-flag-aspnet-core?source=recommendations&tabs=core6x

Assumes you have a App  Configuration store already deployed.

## Set your store credentials using:

dotnet user-secrets init
dotnet user-secrets set ConnectionStrings:AppConfig "<your_connection_string>"

## Configuration of App Configuration
Set the following key-values in configuration explorer
Key	Value
TestApp:Settings:BackgroundColor	white
TestApp:Settings:FontColor	black
TestApp:Settings:FontSize	24
TestApp:Settings:Message	Data from Azure App Configuration

Create a Feature flag named Beta to enable the Beta menu on the webapp.

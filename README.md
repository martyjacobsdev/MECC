# Emergency Care Centre App 🏥

Demo: https://witty-glacier-0a744e500.1.azurestaticapps.net/

## Architecture

This app follows cloud native principles, taking advantage of the distributed computing offered by the Azure cloud delivery model. The application is written in C# and is built on Blazor, a free and open-source web framework that enables developers to create web apps using C# and HTML. Developed by Microsoft. This application draws from the cloud services offered in the Azure environment, and GitHub Actions, enabling the setup of a full CI/CD pipeline.

- <b>Compute</b> - The app is deployed using Azure Static Web Apps
- <b>Database</b> - The database is Azure Table Storage (a flexible NoSQL storage option)
- <b>API</b> - A C# Azure Functions API, which the Blazor application uses to interoperably communicate between components 
- <b>Web Frameworks</b> - Microsoft Blazor, MudBlazor (Front-end CSS framework), Bootstrap, HTML/CSS




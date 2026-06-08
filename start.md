# 1. Create the folder and solution
mkdir Folio && cd Folio
dotnet new sln -n Folio

# 2. Create each project
dotnet new webapi -n Folio.Api
dotnet new worker -n Folio.Worker
dotnet new classlib -n Folio.Core
dotnet new classlib -n Folio.Infrastructure
dotnet new xunit -n Folio.Tests

# 3. Add all projects to the solution
dotnet sln add Folio.Api/Folio.Api.csproj
dotnet sln add Folio.Worker/Folio.Worker.csproj
dotnet sln add Folio.Core/Folio.Core.csproj
dotnet sln add Folio.Infrastructure/Folio.Infrastructure.csproj
dotnet sln add Folio.Tests/Folio.Tests.csproj

# 4. Wire up project references
dotnet add Folio.Infrastructure reference Folio.Core
dotnet add Folio.Api reference Folio.Core Folio.Infrastructure
dotnet add Folio.Worker reference Folio.Core Folio.Infrastructure
dotnet add Folio.Tests reference Folio.Core Folio.Infrastructure Folio.Api

# 5. Open in VS Code
code .
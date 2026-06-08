# Folio

## What It Is
A system where users upload documents (PDF, DOCX, TXT), the files are stored
in the cloud, the text is extracted asynchronously, and the content becomes
searchable via a full-text search engine.

## Architecture
CLIENT
  |
  | POST /api/files/upload
  | GET  /api/search?q=
  ↓
[Azure App Service — Load Balancer]
  |
  ├── [API Instance 1]  ──────────────────────────────────┐
  └── [API Instance 2]  (stateless — either can serve)    │
         |                                                 │
         │ save file                  query search         │
         ↓                                ↑               │
  [Azure Blob Storage]          [Solr Search Index]        │
  (files live here)             (Azure Container Instance) │
         |                                ↑               │
         │ "process this file"            │ index doc      │
         ↓                                │               │
  [Azure Service Bus Queue]               │               │
         |                                │               │
         ↓                                │               │
  [Worker Service]  ─── reads ──→  [Azure SQL Database] ←─┘
  (Azure App Service)                (metadata, status)
         |
         └─ also reads from: [Azure Cache for Redis]

## Tech Stack
- API: .NET 8, ASP.NET Core
- Worker: .NET 8 BackgroundService
- Storage: Azure Blob Storage
- Queue: Azure Service Bus
- Database: Azure SQL / EF Core
- Search: Solr 9
- Cache: Redis
- CI/CD: GitHub Actions

## Project Structure
FileIndexSearch.sln
├── FileIndexSearch.Api/          ← Web API (the public-facing layer)
|── FileIndexSearch.Application/  ← Business logic
├── FileIndexSearch.Worker/       ← Background service (processes files)
├── FileIndexSearch.Core/         ← Shared models and interfaces (no Azure dependencies)
├── FileIndexSearch.Infrastructure/ ← Azure services, EF Core, Solr client
└── FileIndexSearch.Tests/        ← xUnit tests

## Current Phase
Phase X — [what's built, what's next]

## Key Decisions
- [Why you chose X over Y]
- [Any constraints to be aware of]

## Local Dev
[how to run it locally — Azurite, Docker Solr, etc.]

## Do Not
- Store secrets in appsettings.json
- Return EF entities directly from controllers
- [anything else Claude keeps getting wrong]
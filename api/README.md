# MeetMind API

Backend REST API built with C# / ASP.NET Core following Clean Architecture and CQRS.

## Project Structure

```
api/
├── MeetMind.Domain/           # Entities, Value Objects, domain exceptions
├── MeetMind.Application/      # CQRS Commands/Queries, handlers, validators, interfaces
├── MeetMind.Infrastructure/   # EF Core, DbContext, repositories, security
├── MeetMind.API/              # Controllers, middleware, DI composition
└── MeetMind.slnx
```

**Dependency flow:** `Domain ← Application ← Infrastructure ← API`

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- EF Core CLI tools:

```bash
dotnet tool install --global dotnet-ef
# or, if already installed:
dotnet tool update --global dotnet-ef
```

## Database

PostgreSQL runs in Docker, exposed on port **5434** (to avoid conflicts with local instances on 5432).

```bash
# From the repo root
cd db
docker compose up -d          # start
docker compose ps             # check status
docker compose logs -f        # follow logs
docker compose down           # stop (keeps data)
docker compose down -v        # stop and delete volume (wipes all data)
```

Connection string lives in `MeetMind.API/appsettings.Development.json`.

| Setting  | Value              |
|----------|--------------------|
| Host     | localhost          |
| Port     | 5434               |
| Database | meetmind           |
| User     | meetminduser       |
| Password | meetmindpassword   |

## Migrations

All commands run from the `api/` folder. Both flags are required because the `DbContext` lives in Infrastructure while the connection string lives in API.

### Create a migration

```bash
dotnet ef migrations add <MigrationName> \
  --project MeetMind.Infrastructure \
  --startup-project MeetMind.API
```

This generates files under `MeetMind.Infrastructure/Migrations/` but does **not** touch the database. Review the generated migration before applying it.

### Apply migrations

```bash
dotnet ef database update \
  --project MeetMind.Infrastructure \
  --startup-project MeetMind.API
```

### Undo the last migration

Only works if the migration has **not** been applied to the database yet.

```bash
dotnet ef migrations remove \
  --project MeetMind.Infrastructure \
  --startup-project MeetMind.API
```

### Roll back to a specific migration

```bash
dotnet ef database update <PreviousMigrationName> \
  --project MeetMind.Infrastructure \
  --startup-project MeetMind.API
```

### List all migrations

```bash
dotnet ef migrations list \
  --project MeetMind.Infrastructure \
  --startup-project MeetMind.API
```

### Reset the database from scratch

```bash
dotnet ef database drop --force \
  --project MeetMind.Infrastructure \
  --startup-project MeetMind.API

dotnet ef database update \
  --project MeetMind.Infrastructure \
  --startup-project MeetMind.API
```

> Table and column names use **snake_case** via `EFCore.NamingConventions`. This is configured in `Infrastructure/DependencyInjection.cs` with `.UseSnakeCaseNamingConvention()`.

## Running the API

```bash
dotnet restore                        # restore dependencies
dotnet build                          # compile
dotnet run --project MeetMind.API     # run
dotnet watch --project MeetMind.API   # run with hot reload
```

The console prints the assigned port on startup.

## API Documentation

Swagger UI is available in Development only:

```
https://localhost:<port>/docs
```

The raw OpenAPI document is at `/swagger/v1/swagger.json`.

## Testing

```bash
dotnet test
```

## Architecture Notes

### CQRS with MediatR

Every operation is a Command (writes) or a Query (reads), each with its own handler. Controllers hold no business logic — they only dispatch through `IMediator.Send()`.

```
Controller → IMediator.Send() → ValidationBehavior → Handler → Repository → EF Core
```

### Validation

FluentValidation validators live next to their commands. The `ValidationBehavior` pipeline runs them automatically before any handler executes — no manual validation calls needed.

### Error handling

`ExceptionHandlingMiddleware` maps domain exceptions to HTTP status codes:

| Exception                | Status |
|--------------------------|--------|
| `NotFoundException`      | 404    |
| `ConflictException`      | 409    |
| `BadRequestException`    | 400    |
| `ArgumentException`      | 400    |
| anything else            | 500    |

### Endpoints

| Resource         | Route                                          |
|------------------|------------------------------------------------|
| Users            | `api/users`                                    |
| Meetings         | `api/meetings`                                 |
| Participants     | `api/meetings/{meetingId}/participants`        |
| Audio recordings | `api/meetings/{meetingId}/audio-recordings`    |
| Transcript       | `api/meetings/{meetingId}/transcript`          |
| Summary          | `api/meetings/{meetingId}/summary`             |
| Key decisions    | `api/summaries/{summaryId}/key-decisions`      |
| Action items     | `api/action-items`                             |
| Teams            | `api/teams`                                    |
| Team members     | `api/teams/{teamId}/members`                   |

## Troubleshooting

**`dotnet ef` not recognized** — Install the CLI tools: `dotnet tool install --global dotnet-ef`

**Cannot connect to database** — Confirm the container is running with `docker compose ps` from the `db/` folder, and that nothing else is bound to port 5434.

**Migration fails with a column-not-found error on `__EFMigrationsHistory`** — Usually caused by adding the snake_case convention after the first migration was applied. Drop the database, delete the `Migrations/` folder, and recreate a clean migration.

**Container exits immediately after `docker compose up`** — PostgreSQL 18+ changed the data directory layout. The volume must mount to `/var/lib/postgresql`, not `/var/lib/postgresql/data`.

# MeetMind

> **🚧 This project is currently under active development.** Features, architecture, and documentation may change as the project evolves. See the [Roadmap](#roadmap) for current progress.

AI-powered meeting assistant that records, transcribes, and summarizes your meetings using artificial intelligence.

## What is MeetMind?

MeetMind captures audio from your meetings, transcribes them in real-time using OpenAI Whisper, and generates intelligent summaries with action items using GPT-4. It provides semantic search across all your meeting history using RAG (Retrieval-Augmented Generation) with pgvector.

### Key Features

- **Real-time transcription** — Live speech-to-text during meetings with speaker identification
- **AI summaries** — Automatic generation of summaries, key decisions, and action items
- **Semantic search** — Ask questions about past meetings in natural language
- **Desktop app** — Native app for Mac and Windows with system tray, audio capture, and offline support
- **Web dashboard** — Full-featured web interface with team collaboration and sharing
- **GraphQL + REST API** — Flexible querying with both API styles
- **MCP Server** — Integration with Claude and Cursor for AI-assisted workflows

## Tech Stack

### Api
| Technology | Purpose |
|---|---|
| C# / ASP.NET Core 8 | REST + GraphQL API |
| Entity Framework Core 8 | ORM + PostgreSQL |
| MediatR | CQRS pattern |
| FluentValidation | Request validation |
| SignalR | Real-time WebSocket communication |
| MassTransit + RabbitMQ | Async message processing |
| Redis | Distributed cache + pub/sub |
| HotChocolate | GraphQL server |

### AI / ML
| Technology | Purpose |
|---|---|
| OpenAI Whisper | Speech-to-text transcription |
| GPT-4 / Claude | Summarization + function calling |
| pgvector | Vector embeddings for semantic search |

### Frontend
| Technology | Purpose |
|---|---|
| React 19 + TypeScript | UI components |
| Tauri 2.0 | Cross-platform desktop app |
| Next.js 15 | SSR web application |
| Zustand | Client state management |
| TanStack Query | Server state + caching |
| shadcn/ui + Tailwind | Design system |
| Nx | Monorepo + code sharing |

### Infrastructure
| Technology | Purpose |
|---|---|
| Docker + Compose | Containerization |
| GitHub Actions | CI/CD pipeline |
| Terraform | Infrastructure as Code |
| AWS (ECS, RDS, S3) | Cloud hosting |

## Architecture

The api follows **Clean Architecture** with four layers:

```
meetmind/
├── api/
│   ├── MeetMind.Domain/           # Entities, Value Objects, interfaces
│   ├── MeetMind.Application/      # Use cases (CQRS Commands/Queries)
│   ├── MeetMind.Infrastructure/   # EF Core, Redis, RabbitMQ, OpenAI
│   ├── MeetMind.API/              # Controllers, Middleware, SignalR Hubs
│   └── MeetMind.sln
├── frontend/
│   ├── apps/
│   │   ├── desktop/               # Tauri 2.0 + React
│   │   └── web/                   # Next.js 15
│   └── libs/
│       ├── ui/                    # Shared components (shadcn/ui)
│       ├── hooks/                 # Shared React hooks
│       ├── api-client/            # TanStack Query + API layer
│       ├── store/                 # Zustand stores
│       └── types/                 # Shared TypeScript types
├── infra/
│   ├── docker/
│   ├── terraform/
│   └── .github/workflows/
└── docker-compose.yml
```

### Dependency Flow

```
Domain ← Application ← Infrastructure ← API
```

- **Domain** — Zero dependencies. Pure business logic, entities, and interfaces.
- **Application** — Depends on Domain. Contains use cases, CQRS handlers, and service interfaces.
- **Infrastructure** — Depends on Application. Implements repositories, external APIs, and persistence.
- **API** — Depends on Infrastructure. HTTP layer, controllers, middleware, and dependency injection setup.

### Data Flow

```
Audio Capture → SignalR Streaming → Whisper Transcription → GPT Summarization
                                                         → Embeddings → pgvector (semantic search)
                                  → Real-time broadcast to participants
```

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- [Node.js 20+](https://nodejs.org/) (for frontend, coming in Phase 2)
- [Rust](https://rustup.rs/) (for Tauri desktop app, coming in Phase 2)

### Run the API

```bash
cd api

# Restore dependencies
dotnet restore

# Start infrastructure (PostgreSQL, Redis, RabbitMQ)
docker compose up -d

# Run the API
dotnet run --project MeetMind.API
```

### Run Tests

```bash
cd api
dotnet test
```

## Roadmap

- [ ] **Phase 1** — [**In Progress**] Backend & API (C#, Clean Architecture, CQRS, SignalR, AI integration)
- [ ] **Phase 2** — Desktop App (Tauri 2.0 + React 19) 
- [ ] **Phase 3** — Web App (Next.js 15, SSR, collaboration features)
- [ ] **Phase 4** — Infrastructure & Deployment (Docker, CI/CD, Terraform, AWS)

## Author

**Ricardo Caicedo** — Software Engineer
- [Web](https://rikycg.com)
- [LinkedIn](https://linkedin.com/in/ricardcaicedo)

## License

This project is licensed under the MIT License.

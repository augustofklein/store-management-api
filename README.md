# 🛠️ Store Management API - Backend

## 📖 Problem Definition

The definition of the business problem and context for this system can be found in the following repository, which contains the frontend source code and project vision:

🔗 [Store Management - Frontend & Project Vision](https://github.com/augustofklein/store-management)

---

## 📌 Overview

This backend project is part of a complete store management system, developed to support a small business specialized in selling sportswear and accessories. The backend is responsible for handling business logic, data persistence, and integration with external services.

The solution will follow **best development practices**, including:

- Clean Architecture
- Separation of concerns (Application, Core, Infrastructure, WebApi)
- Use of the **CQRS** pattern for command/query separation
- Modular and maintainable codebase
- Integration with PostgreSQL and external storage services

---

## ⚙️ Technologies & Architecture

- **Language**: C# (.NET)
- **Framework**: .NET 8 (or latest LTS)
- **Architecture**: Clean Architecture (Application, Core, Infrastructure, WebApi)
- **Pattern**: CQRS (Command and Query Responsibility Segregation)
- **Database**: PostgreSQL hosted on **[Neon](https://neon.tech/)** (free-tier)
- **Frontend Hosting**: [Vercel](https://vercel.com/) (Next.js)
- **Backend Hosting**: Azure App Service (via individual subscription)
- **File Storage**: Cloud file storage (for product images and document attachments)

---

## 🗂️ Project Structure

StoreManagement.Api/
- **Application**/     → Business rules and CQRS handlers
- **Core**/            → Domain models and interfaces
- **Infrastructure**/  → Data access, services, external integrations
- **WebApi**/          → API endpoints and middleware

## ☁️ Deployment Strategy

| Component      | Platform     | Details                              |
|----------------|--------------|--------------------------------------|
| Backend API    | Azure        | Hosted via Azure App Service         |
| Frontend       | Vercel       | React + Next.js app                  |
| Database       | Neon         | PostgreSQL (serverless & scalable)   |
| File Storage   | TBD (Cloud)  | For product images and documents     |

---

## 🚧 Current Status

The project is currently in the **design phase**, with UI/UX layouts being developed in **Figma**.  
Once the layouts are finalized and approved, the development of backend and frontend components will begin accordingly.

---

## 📎 Related Repositories

- [Frontend (React + Next.js)](https://github.com/augustofklein/store-management)

---

## 🧩 Future Features

- Authentication & authorization
- Role-based access control
- Product image uploads
- Purchase/sale invoice management
- Stock level tracking
- Customer and supplier management
- Import of purchase XML files

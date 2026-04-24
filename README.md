# 📚 Library Management System (LibraryMS)

![.NET Version](https://img.shields.io/badge/.NET-8.0-blueviolet)
![Database](https://img.shields.io/badge/Database-SQLite-blue)
![License](https://img.shields.io/badge/License-MIT-green)

A robust, modern digital library management system built with **ASP.NET Core MVC**. This project provides a complete solution for indexing books, managing authors, and tracking loans with a specialized role-based security model. Developed as an 11th-grade school project.

---

## 🚀 Key Features

### 👤 Identity & Security
* **Role-Based Access Control (RBAC):**
    * **Admin:** Full system control, statistics, and user management.
    * **Librarian:** Catalog management (Add/Edit Books) and loan tracking.
    * **Reader:** Personal dashboard to view the catalog and track borrowed books ("My Books").
* **Secure Auth:** Login/Register functionality with data persistence using Microsoft Identity.

### 📖 Catalog Management
* **Searchable Index:** Filter books by Title, Author, or Genre.
* **Dynamic Authors/Genres:** Detailed pages for authors showing their total book count.
* **Loan Logic:** Automatic calculation of return dates and tracking of active vs. overdue borrowings.

---

## 🛠 Tech Stack

* **Framework:** .NET 8.0 (ASP.NET Core MVC)
* **Database:** SQLite with **Entity Framework Core**
* **Security:** Microsoft Identity Platform
* **UI/UX:** Bootstrap 5, Bootstrap Icons, jQuery
* **Pattern:** Service/Repository pattern with DTOs (Data Transfer Objects)

---

## 🏗 Database & Relationship Logic

The system architecture ensures high data integrity:
* **ApplicationUser:** Inherits from `IdentityUser<Guid>` to ensure unique global identifiers.
* **Foreign Keys:** Mapped relationships between `Guid` (Users) and `int` (Books/Authors/Categories).
* **Relational Integrity:** Cascading rules for Categories and Authors to prevent orphaned book records.

---

## ⚙️ Installation & Setup

1.  **Clone the repository:**
    ```bash
    git clone [https://github.com/your-username/project-name.git](https://github.com/your-username/project-name.git)
    cd LibraryManager
    ```

2.  **Restore NuGet Packages:**
    ```bash
    dotnet restore
    ```

3.  **Initialize Database:**
    Delete any existing `library.db` files and the `Migrations/` folder (if you wish to start fresh), then run:
    ```bash
    dotnet ef migrations add InitialLibrarySetup
    dotnet ef database update
    ```

4.  **Launch Project:**
    ```bash
    dotnet run
    ```
    The application will be accessible at `http://localhost:5052`.

### 🔑 Default Admin Account
A default admin user is seeded automatically during the first database update:
* **Email:** `admin@library.com`
* **Password:** `Admin123!`

---

## 📂 Project Structure

| Directory | Description |
| :--- | :--- |
| **Controllers/** | Route handling and Action logic. |
| **Services/** | Business Logic implementation (Loan, Book, and Account services). |
| **Data/** | DbContext configuration, Entity models, and Seed data. |
| **DTOs/** | Data Transfer Objects for secure data binding and validation. |
| **Views/** | Responsive Razor templates styled with Bootstrap 5. |
| **wwwroot/** | Static files (CSS, JS, Images). |

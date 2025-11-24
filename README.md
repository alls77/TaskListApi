## TaskLists API

## 📌 Overview

This project implements a backend service for managing **task lists** with support for:

- personal ownership
    
- sharing lists with other users
    
- CRUD operations
    
- querying with sorting, pagination, and access rules
    

The solution uses **ASP.NET Core 8**, **MongoDB**, **Clean Architecture principles**, **logging**, and includes **unit tests**.

## ✔ Features

### “Get all task lists for a user (owner OR shared) with sorting and pagination”

#### 📝 Summary

A user can view:

- lists they **own**
    
- lists that are **shared** with them
    

The endpoint supports:

- filtering
    
- sorting
    
- pagination
    

#### Feature Details

**Endpoint**

`GET /api/taskLists?userId={userId}&page={page}&pageSize={pageSize}

**Example Request**

`GET /api/taskLists?userId=user-1&page=1&pageSize=10

---

## 🔍 Getting Started

#### 1. Clone the Repository

`git clone https://github.com/alls77/TaskListApi.git cd TaskListApi`

#### 2. Restore Dependencies

`dotnet restore`

#### 3. Run the API

`dotnet run --project TaskListApi`

#### 4. Accessing the Application

- **Swagger UI (API documentation)**  
    [https://localhost:7076/swagger](https://localhost:7076/swagger)
    
- **Application Logs (Serilog)**  
    `./Logs/taskList-api.log`
    
- **Database (MongoDB)**  
    Use the scripts under:  
    `Infrastructure/Scripts/`  
    to **create the database**, **apply validators**, **create indexes**, and **seed initial test data**.  
    These scripts are idempotent and can be run multiple times safely.

---

## 🗂 Decomposition of Remaining Tasks

Beyond the implemented feature, a **complete task breakdown** was prepared for remaining operations described in the specification.

These task descriptions are stored in:

📁 **`Tasks/` directory**

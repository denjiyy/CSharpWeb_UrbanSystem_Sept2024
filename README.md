# CSharpWeb Urban System - September 2024

## Getting Started

Follow the steps below to set up and run the application:

### Step 1: Download the Zip File

Download the zip file from the `WorkingApplicationZip` branch.

### Step 2: Open the Project in Visual Studio

1. Select the `UrbanSystem.Data` project in Visual Studio.
3. Open the **Package Manager Console**.

### Step 3: Run the Database Migrations

In the Package Manager Console, run the following commands:

```powershell
Add-Migration Initial
Update-Database

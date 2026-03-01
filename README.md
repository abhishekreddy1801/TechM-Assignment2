# TechM-Assignment2

📌 Project Overview

This repository contains both:

    ✅ Manual Test Design Document

    ✅ Automated UI Test Solution (Selenium + NUnit)
The assignment validates checkout behavior on the OpenCart demo site.

Application Under Test: https://parabank.parasoft.com/ 

Technology Stack:

Language: C# (.NET 6) or above
UI Automation: Selenium WebDriver
Test Framework: NUnit
Assertion Library: Fluent / Default Assertions 
Version Control: Git

🎯 Objective

Validate end-to-end banking workflow in ParaBank when:

    User registers with valid details
    System creates the account successfully and logs in automatically
    User opens a new Savings Account
    System confirms account creation
    User applies for a loan with high amount → Loan is Denied
    User applies for a loan with lower amount → Loan is Approved
    User navigates to Accounts Overview
    System displays updated total balance
    Total balance is greater than 1000
    User logs out successfully
    System redirects to Customer Login page

🧾 Manual Test Coverage

The TestDesign.docx document includes:

    Test scenarios
    Step-by-step actions
    Expected results

🤖 Automation Scope

    The automated test validates:
    User registration with valid details
    Successful account creation confirmation
    Automatic login after registration
    Opening of a new Savings Account
    Account creation success message validation
    Loan request submission (Rejection scenario)
    Loan request submission (Approval scenario)
    Navigation to Accounts Overview page
    Total account balance extraction and validation
    Successful logout functionality
    Redirection to Customer Login page

🚀 How to Run the Automation Code

    1. Open Visual Studio
    2. Create a new NUnit Test Project (.NET 6)
    3. Install required NuGet packages:
    4. Selenium.WebDriver
    5. Selenium.Support
    6. Copy repository class files into the project
    7. Build the solution (Ctrl + Shift + B)
    8. Open cmd and navigate to path where dll is preset
    9. run command 
        "dotnet test Project.dll"
    10. Replace Project.dll with the actual project name you created.
        For example, if your project name is TechM.Assignment, then run:
        "dotnet test TechM.Assignment.dll"

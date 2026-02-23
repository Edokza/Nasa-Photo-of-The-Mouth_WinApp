# NasaPhoto WPF (.NET 8)

A WPF desktop application built with **Clean Architecture** that
displays NASA Astronomy Picture of the Day (APOD).

## 🚀 Tech Stack

-   .NET 8
-   WPF
-   Clean Architecture
-   HttpClient
-   xUnit (Unit Testing)

## 🏗 Architecture

The solution is structured using Clean Architecture principles:

-   **Domain** -- Core entities and business rules
-   **Application** -- Use cases and interfaces
-   **Infrastructure** -- External services (NASA API)
-   **Wpf** -- UI layer (MVVM)
-   **Tests** -- Unit tests

## ▶ How to Run

1.  Open the solution in Visual Studio 2022
2.  Set `NasaPhoto_WinApp.Wpf` as Startup Project
3.  Press `F5`

Or run from published folder:

    

## 📦 Features

-   Fetch APOD by date
-   Display image, title and explanation
-   Layered architecture with separation of concerns
-   Unit test coverage for application layer

------------------------------------------------------------------------

Built for learning and portfolio purposes.

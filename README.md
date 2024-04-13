# Outbox Pattern Demo in .NET

A simple and minimal solution that showcases the concept of Outbox Pattern. This project is inspired by watching Milan Jovanović's tutorials. Please see references below.

## Setup

Clone the solution and restore all dependencies. This should be done automatically by Visual Studio upon opening of the solution. Otherwise, if it doesn't restore the dependencies, run the following in the Package Manager console.

```
dotnet restore
```

## Database Setup

The solution is using code-first approach. Thus, it will automatically create a database (if it does not exist yet) and run the migrations to the target database connection found in the appsettings.json. To update the database, run the following in the Package Manager console.

```
update-database
```

## References

1. https://www.youtube.com/watch?v=XALvnX7MPeo
2. https://www.milanjovanovic.tech/blog/outbox-pattern-for-reliable-microservices-messaging
3. https://the-dotnet-weekly.ck.page/outbox

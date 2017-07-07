[LinqToDB](https://github.com/linq2db/linq2db) Identity store provider for [ASP.NET Core Identity](https://github.com/aspnet/Identity)
===

* Current build status [![Build status](https://ci.appveyor.com/api/projects/status/2d8k9n1x5ggsuv3f?svg=true)](https://ci.appveyor.com/project/igor-tkachev/linqtodb-identity)
* Master build status [![Build status](https://ci.appveyor.com/api/projects/status/2d8k9n1x5ggsuv3f/branch/master?svg=true)](https://ci.appveyor.com/project/igor-tkachev/linqtodb-identity/branch/master)


## Feeds
* Release builds can be found on [NuGet](https://www.nuget.org/packages?q=linq2db)
* [MyGet](https://www.myget.org/gallery/linq2db)
  * V2 `https://www.myget.org/F/linq2db/api/v2`
  * V3 `https://www.myget.org/F/linq2db/api/v3/index.json`

## Usage
Install package:

`PM> Install-Package linq2db.Identity`

In general this is the same as for Entity Framework, just call `AddLinqToDBStores` instead of `AddEntityFrameworkStores` in your `Startup.cs` like [here](https://github.com/linq2db/LinqToDB.Identity/blob/master/samples/IdentitySample.Mvc/Startup.cs#L62):
```cs
services.AddIdentity<ApplicationUser, IdentityRole>(options => {
    options.Cookies.ApplicationCookie.AuthenticationScheme = "ApplicationCookie";
    options.Cookies.ApplicationCookie.CookieName = "Interop";
    options.Cookies.ApplicationCookie.DataProtectionProvider = DataProtectionProvider.Create(new DirectoryInfo("C:\\Github\\Identity\\artifacts"));
})
    .AddLinqToDBStores(new DefaultConnectionFactory()) //here
    .AddDefaultTokenProviders();
```

The main difference with Entity Framework Core storage provider are:
* We do not use hardcoded classes - interfaces like `IIdentityUser<TKey>` are used (but yes, we do have default implementation)
* Data connection factory is used for calling to database

## Special
All source code is based on original Microsoft Entity Framework Core storage provider for [ASP.NET Core Identity](https://github.com/aspnet/Identity).

Tests and sample are just adopted for using [LinqToDB](https://github.com/linq2db/linq2db). For inmemory storage tests SQLite inmemory database is used.

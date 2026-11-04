> [!IMPORTANT]
> **This repository is archived.** `linq2db.Identity` was overhauled and moved into the main linq2db repository: it
> now uses the standard ASP.NET Core Identity entity types and EF Core's `AspNet*` schema, takes any linq2db
> `IDataContext`, and follows linq2db's own release line. The new version ships with **linq2db 6.6.0** under the same
> NuGet package id, [`linq2db.Identity`](https://www.nuget.org/packages/linq2db.Identity).
>
> * Current source and documentation: [`Source/LinqToDB.Identity`](https://github.com/linq2db/linq2db/tree/master/Source/LinqToDB.Identity) in [linq2db/linq2db](https://github.com/linq2db/linq2db)
> * Moving from this version: the *Migrating from the standalone `linq2db.Identity`* section of that readme
> * The migration pull request: [linq2db/linq2db#5640](https://github.com/linq2db/linq2db/pull/5640)
> * Issues and questions: [linq2db/linq2db issues](https://github.com/linq2db/linq2db/issues)
>
> The 6.0.0 API documented below is not compatible with the new version. The code here is kept for reference only.

# [LinqToDB](https://github.com/linq2db/linq2db) Identity store provider for [ASP.NET Core Identity](https://github.com/aspnet/Identity)

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

### Identity and other mapping

We do not use any default mapping attributes on default POCOs (`IdentityUser`, `IdentityRole` and so on). This is because this types can be used on different environvents.

If in some reason you prefere using default POCOs you can use [fluent mapping](https://linq2db.github.io/api/LinqToDB.Mapping.FluentMappingBuilder.html) in your `Startup.cs` to define needed attributes.

For example to treat `Id` as identity:

```cs
    LinqToDB.Mapping.MappingSchema.Default.GetFluentMappingBuilder()
        .Entity<IdentityUser<int>>()
        .HasIdentity(_ => _.Id);
```

## Special

All source code is based on original Microsoft Entity Framework Core storage provider for [ASP.NET Core Identity](https://github.com/aspnet/Identity).

Tests and sample are just adopted for using [LinqToDB](https://github.com/linq2db/linq2db). For inmemory storage tests SQLite inmemory database is used.

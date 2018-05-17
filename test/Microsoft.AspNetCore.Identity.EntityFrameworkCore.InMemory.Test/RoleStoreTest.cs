// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Threading.Tasks;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB.DataProvider.SQLite;
using LinqToDB.Identity;
using Microsoft.AspNetCore.Identity.Test;
using Microsoft.Extensions.DependencyInjection;
using Xunit;


namespace Microsoft.AspNetCore.Identity.EntityFrameworkCore.InMemory.Test
{
    using IdentityRole=LinqToDB.Identity.IdentityRole;
	public class RoleStoreTest : IClassFixture<InMemoryStorage>
	{
		public RoleStoreTest(InMemoryStorage storage)
		{
			_storage = storage;
		}

		private readonly InMemoryStorage _storage;

		private IConnectionFactory GetConnectionFactory()
		{
			var connectionString = _storage.ConnectionString;

			var factory = new TestConnectionFactory(new SQLiteDataProvider(), "RoleStoreTest", connectionString);
			factory.CreateTables<LinqToDB.Identity.IdentityUser, LinqToDB.Identity.IdentityRole, string>();
			return factory;
		}

		[Fact]
		public async Task CanCreateRoleWithSingletonManager()
		{
			var services = TestIdentityFactory.CreateTestServices();
			//services.AddEntityFrameworkInMemoryDatabase();
			services.AddSingleton(GetConnectionFactory());
			services.AddTransient<IRoleStore<LinqToDB.Identity.IdentityRole>, RoleStore<LinqToDB.Identity.IdentityRole>>();
			services.AddSingleton<RoleManager<LinqToDB.Identity.IdentityRole>>();
			var provider = services.BuildServiceProvider();
			var manager = provider.GetRequiredService<RoleManager<LinqToDB.Identity.IdentityRole>>();
			Assert.NotNull(manager);
			IdentityResultAssert.IsSuccess(await manager.CreateAsync(new IdentityRole("someRole")));
		}

		[Fact]
		public async Task CanCreateUsingAddRoleManager()
		{
			var manager = TestIdentityFactory.CreateRoleManager(GetConnectionFactory());
			Assert.NotNull(manager);
			IdentityResultAssert.IsSuccess(await manager.CreateAsync(new IdentityRole("arole")));
		}

		[Fact]
		public async Task CanUpdateRoleName()
		{
			var manager = TestIdentityFactory.CreateRoleManager(GetConnectionFactory());
			var role = new IdentityRole("UpdateRoleName");
			IdentityResultAssert.IsSuccess(await manager.CreateAsync(role));
			Assert.Null(await manager.FindByNameAsync("New"));
			IdentityResultAssert.IsSuccess(await manager.SetRoleNameAsync(role, "New"));
			IdentityResultAssert.IsSuccess(await manager.UpdateAsync(role));
			Assert.NotNull(await manager.FindByNameAsync("New"));
			Assert.Null(await manager.FindByNameAsync("UpdateAsync"));
		}

		[Fact]
		public async Task RoleStoreMethodsThrowWhenDisposedTest()
		{
			var store = new RoleStore<IdentityRole>(GetConnectionFactory());
			store.Dispose();
			await Assert.ThrowsAsync<ObjectDisposedException>(async () => await store.FindByIdAsync(null));
			await Assert.ThrowsAsync<ObjectDisposedException>(async () => await store.FindByNameAsync(null));
			await Assert.ThrowsAsync<ObjectDisposedException>(async () => await store.GetRoleIdAsync(null));
			await Assert.ThrowsAsync<ObjectDisposedException>(async () => await store.GetRoleNameAsync(null));
			await Assert.ThrowsAsync<ObjectDisposedException>(async () => await store.SetRoleNameAsync(null, null));
			await Assert.ThrowsAsync<ObjectDisposedException>(async () => await store.CreateAsync(null));
			await Assert.ThrowsAsync<ObjectDisposedException>(async () => await store.UpdateAsync(null));
			await Assert.ThrowsAsync<ObjectDisposedException>(async () => await store.DeleteAsync(null));
		}

		[Fact]
		public async Task RoleStorePublicNullCheckTest()
		{
			Assert.Throws<ArgumentNullException>("factory",
				() => new RoleStore<IdentityRole>(null));
			var store = new RoleStore<IdentityRole>(GetConnectionFactory());
			await Assert.ThrowsAsync<ArgumentNullException>("role", async () => await store.GetRoleIdAsync(null));
			await Assert.ThrowsAsync<ArgumentNullException>("role", async () => await store.GetRoleNameAsync(null));
			await Assert.ThrowsAsync<ArgumentNullException>("role", async () => await store.SetRoleNameAsync(null, null));
			await Assert.ThrowsAsync<ArgumentNullException>("role", async () => await store.CreateAsync(null));
			await Assert.ThrowsAsync<ArgumentNullException>("role", async () => await store.UpdateAsync(null));
			await Assert.ThrowsAsync<ArgumentNullException>("role", async () => await store.DeleteAsync(null));
		}
	}
}
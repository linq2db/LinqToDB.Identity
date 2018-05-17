using LinqToDB.Identity;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
// ReSharper disable InconsistentNaming

namespace Microsoft.AspNetCore.Identity.EntityFrameworkCore.Test
{
	using IdentityUser = LinqToDB.Identity.IdentityUser;
	using IdentityRole = LinqToDB.Identity.IdentityRole;

	using IdentityUserL = LinqToDB.Identity.IdentityUser<long>;
	using IdentityRoleL = LinqToDB.Identity.IdentityRole<long>;

	public class ProvideConfigutayionTest
	{
		[Fact]
		public void AddLinqToDBStores0()
		{
			var services = new ServiceCollection();
			services
				.AddIdentity<IdentityUser, IdentityRole>()
				.AddLinqToDBStores(new DefaultConnectionFactory());

			var sp = services.BuildServiceProvider();

			Assert.NotNull(sp.GetService<IUserStore<IdentityUser>>());
			Assert.NotNull(sp.GetService<IRoleStore<IdentityRole>>());
		}

		[Fact]
		public void AddLinqToDBStores1()
		{
			var services = new ServiceCollection();
			services
				.AddIdentity<IdentityUserL, IdentityRoleL>()
				.AddLinqToDBStores<long>(new DefaultConnectionFactory());

			var sp = services.BuildServiceProvider();

			Assert.NotNull(sp.GetService<IUserStore<IdentityUserL>>());
			Assert.NotNull(sp.GetService<IRoleStore<IdentityRoleL>>());
		}

		[Fact]
		public void AddLinqToDBStores6()
		{
			var services = new ServiceCollection();
			services
				.AddIdentity<LinqToDB.Identity.IdentityUser<decimal>, LinqToDB.Identity.IdentityRole<decimal>>()
				.AddLinqToDBStores<
					decimal, 
					LinqToDB.Identity.IdentityUserClaim<decimal>, 
					LinqToDB.Identity.IdentityUserRole<decimal>, 
					LinqToDB.Identity.IdentityUserLogin<decimal>, 
					LinqToDB.Identity.IdentityUserToken<decimal>,
					LinqToDB.Identity.IdentityRoleClaim<decimal>>(new DefaultConnectionFactory());

			var sp = services.BuildServiceProvider();

			Assert.NotNull(sp.GetService<IUserStore<LinqToDB.Identity.IdentityUser<decimal>>>());
			Assert.NotNull(sp.GetService<IRoleStore<LinqToDB.Identity.IdentityRole<decimal>>>());
		}

	}
}
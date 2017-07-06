using LinqToDB.Identity;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
// ReSharper disable InconsistentNaming

namespace Microsoft.AspNetCore.Identity.EntityFrameworkCore.Test
{
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
				.AddIdentity<IdentityUser<long>, IdentityRole<long>>()
				.AddLinqToDBStores<long>(new DefaultConnectionFactory());

			var sp = services.BuildServiceProvider();

			Assert.NotNull(sp.GetService<IUserStore<IdentityUser<long>>>());
			Assert.NotNull(sp.GetService<IRoleStore<IdentityRole<long>>>());
		}

		[Fact]
		public void AddLinqToDBStores6()
		{
			var services = new ServiceCollection();
			services
				.AddIdentity<IdentityUser<decimal>, IdentityRole<decimal>>()
				.AddLinqToDBStores<
					decimal, 
					IdentityUserClaim<decimal>, 
					IdentityUserRole<decimal>, 
					IdentityUserLogin<decimal>, 
					IdentityUserToken<decimal>, 
					IdentityRoleClaim<decimal>>(new DefaultConnectionFactory());

			var sp = services.BuildServiceProvider();

			Assert.NotNull(sp.GetService<IUserStore<IdentityUser<decimal>>>());
			Assert.NotNull(sp.GetService<IRoleStore<IdentityRole<decimal>>>());
		}

	}
}
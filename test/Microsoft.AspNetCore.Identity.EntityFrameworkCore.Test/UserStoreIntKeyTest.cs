// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using LinqToDB.Identity;

namespace Microsoft.AspNetCore.Identity.EntityFrameworkCore.Test
{
	public class IntUser : LinqToDB.Identity.IdentityUser<int>
	{
		private static volatile int _id;

		public IntUser()
		{
			Id = ++_id;
			UserName = Guid.NewGuid().ToString();
		}
	}

	public class IntRole : LinqToDB.Identity.IdentityRole<int>
	{
		private static volatile int _id;

		public IntRole()
		{
			Id = ++_id;
			Name = Guid.NewGuid().ToString();
		}
	}

	public class UserStoreIntTest : SqlStoreTestBase<IntUser, IntRole, int>
	{
		public UserStoreIntTest(ScratchDatabaseFixture fixture)
			: base(fixture)
		{
		}
	}
}
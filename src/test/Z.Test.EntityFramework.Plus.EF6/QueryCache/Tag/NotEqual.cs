﻿// Description: EF Bulk Operations & Utilities | Bulk Insert, Update, Delete, Merge from database.
// Website & Documentation: https://github.com/zzzprojects/Entity-Framework-Plus
// Forum: http://zzzprojects.uservoice.com/forums/283924-entity-framework-plus
// License: http://www.zzzprojects.com/license-agreement/
// More projects: http://www.zzzprojects.com/
// Copyright (c) 2015 ZZZ Projects. All rights reserved.

using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Z.EntityFramework.Plus;

namespace Z.Test.EntityFramework.Plus
{
    public partial class QueryCache_Tag
    {
        [TestMethod]
        public void Tag_NotEqual()
        {
            var testCacheKey = Guid.NewGuid().ToString();

            EntitySimpleHelper.Clear();
            EntitySimpleHelper.AddOne();

            using (var ctx = new EntityContext())
            {
                // BEFORE
                var itemCountBefore = ctx.EntitySimples.FromCache(testCacheKey).Count();
                var cacheCountBefore = QueryCacheManagerHelper.GetCacheCount();

                EntitySimpleHelper.Clear();

                // AFTER
                var itemCountAfter = ctx.EntitySimples.FromCache(testCacheKey, Guid.NewGuid().ToString()).Count();
                var cacheCountAfter = QueryCacheManagerHelper.GetCacheCount();

                // TEST: The item count are NOT equal (A new cache key is used, the query is materialized)
                Assert.AreNotEqual(itemCountBefore, itemCountAfter);
                Assert.AreEqual(0, itemCountAfter);

                // TEST: The cache count are NOT equal (A new cache key is used)
                Assert.AreEqual(cacheCountBefore + 1, cacheCountAfter);
            }
        }
    }
}
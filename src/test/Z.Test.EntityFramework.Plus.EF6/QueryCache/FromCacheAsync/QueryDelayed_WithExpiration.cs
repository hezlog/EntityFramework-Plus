﻿// Description: EF Bulk Operations & Utilities | Bulk Insert, Update, Delete, Merge from database.
// Website & Documentation: https://github.com/zzzprojects/Entity-Framework-Plus
// Forum: http://zzzprojects.uservoice.com/forums/283924-entity-framework-plus
// License: http://www.zzzprojects.com/license-agreement/
// More projects: http://www.zzzprojects.com/
// Copyright (c) 2015 ZZZ Projects. All rights reserved.

#if EF5 || EF6
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Z.EntityFramework.Plus;

namespace Z.Test.EntityFramework.Plus
{
    public partial class QueryCache_FromCacheAsync
    {
        [TestMethod]
        public void FromCacheAsync_QueryDelayed_WithExpirations()
        {
            EntitySimpleHelper.Clear();
            EntitySimpleHelper.AddOne();

            using (var ctx = new EntityContext())
            {
                // BEFORE
                var itemCountBefore = ctx.EntitySimples.DelayedCount().FromCacheAsync(DateTime.Now.AddMinutes(2)).Result;
                var cacheCountBefore = QueryCacheManagerHelper.GetCacheCount();

                EntitySimpleHelper.Clear();

                // AFTER
                var itemCountAfter = ctx.EntitySimples.DelayedCount().FromCacheAsync(DateTime.Now.AddMinutes(2)).Result;
                var cacheCountAfter = QueryCacheManagerHelper.GetCacheCount();

                // TEST: The item count are equal
                Assert.AreEqual(itemCountBefore, itemCountAfter);

                // TEST: The cache count are equal
                Assert.AreEqual(cacheCountBefore, cacheCountAfter);
            }
        }
    }
}

#endif
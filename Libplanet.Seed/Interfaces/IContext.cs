﻿using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GraphQL.Types;
using Libplanet.Seed.Queries;

namespace Libplanet.Seed.Interfaces
{
    public interface IContext
    {
        ConcurrentDictionary<Address, PeerInfo>? Peers { get; }
    }

    public static class SeedContext
    {
        private static ConditionalWeakTable<object, Schema> _schemaObjects =
            new ConditionalWeakTable<object, Schema>();

        public static Schema GetSchema(this IContext context)
        {
            return _schemaObjects.GetValue(
                context,
                (_) =>
                {
                    var s = new Schema
                    {
                        Query = new Query(context.Peers),
                    };
                    return s;
                });
        }
    }
}

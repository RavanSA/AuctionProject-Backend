using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Extensions;
public static class EnumerableChunkExtensions
{
    public static IEnumerable<List<T>> ChunkBy<T>(
        this IEnumerable<T> source,
        int size)
    {
        var bucket = new List<T>(size);

        foreach (var item in source)
        {
            bucket.Add(item);
            if (bucket.Count == size)
            {
                yield return bucket;
                bucket = new List<T>(size);
            }
        }

        if (bucket.Count > 0)
            yield return bucket;
    }
}

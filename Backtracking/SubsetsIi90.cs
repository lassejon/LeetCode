using System;
using System.Collections.Generic;
using System.Linq;

namespace Backtracking;

public class SubsetsIi90
{
    public IList<IList<int>> SubsetsWithDup(int[] nums)
    {
        var cache = new HashSet<List<int>>(new ListEqualityComparer());

        var subset = new List<int>();
        var result = new List<IList<int>>();

        this.BacktrackWithDup(0, nums, subset, result, cache);

        return result;
    }

    private void BacktrackWithDup(int i, int[] nums, List<int> subset, List<IList<int>> result, HashSet<List<int>> cache)
    {
        if (i >= nums.Length)
        {
            if (!cache.Contains(subset))
            {
                var subsetToAdd = new List<int>(subset);
                result.Add(subsetToAdd);
                cache.Add(subsetToAdd);
            }
            
            return;
        }
        
        subset.Add(nums[i]);
        this.BacktrackWithDup(i + 1, nums, subset, result, cache);

        subset.Remove(nums[i]);
        this.BacktrackWithDup(i + 1, nums, subset, result, cache);
    }

    private class ListEqualityComparer : IEqualityComparer<List<int>>
    {
        public bool Equals(List<int> x, List<int> y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            if (x.Capacity != y.Capacity && x.Count != y.Count) return false;

            return x.SequenceEqual(y);
        }

        public int GetHashCode(List<int> obj)
        {
            return HashCode.Combine(obj.Capacity, obj.Count);
        }
    }
    
}
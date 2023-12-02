using System;
using System.Collections.Generic;

namespace Backtracking;

public class CombinationSumII40
{
    public IList<IList<int>> CombinationSum2(int[] candidates, int target) 
    {
        var result = new List<IList<int>>();
        var subset = new List<int>();
        
        Array.Sort(candidates);
        
        Backtrack(0, candidates, target, subset, result);
        
        return result;
    }

    private void Backtrack(int i, int[] candidates, int target, List<int> subset, List<IList<int>> result)
    {
        switch (target)
        {
            case 0:
                result.Add(new List<int>(subset));
                return;
            case < 0:
                return;
        }

        for (var j = i; j < candidates.Length; j++)
        {
            if (j > i && candidates[j] == candidates[j - 1])
            {
                continue;
            }
            
            subset.Add(candidates[j]);
            Backtrack(j + 1, candidates, target - candidates[j], subset, result);
            subset.RemoveAt(subset.Count - 1);
        }
    }
    
    public IList<IList<int>> CombinationSum2Cache(int[] candidates, int target) 
    {
        var result = new List<IList<int>>();
        var subset = new List<int>();
        var cache = new HashSet<HashSet<int>>(new HashSetEqualityComparer<int>());
        
        BacktrackCache(0, candidates, target, subset, result, cache, new HashSet<int>());
        
        return result;
    }

    private void BacktrackCache(int i, int[] candidates, int target, List<int> subset, List<IList<int>> result,
        HashSet<HashSet<int>> cache, HashSet<int> set)
    {
        switch (target)
        {
            case 0:
                if (!cache.Contains(set))
                {
                    result.Add(new List<int>(subset));
                    cache.Add(new HashSet<int>(set));
                }
                return;
            case < 0:
                return;
        }

        for (var j = i; j < candidates.Length; j++)
        {
            subset.Add(candidates[j]);
            set.Add(candidates[j]);
            BacktrackCache(j + 1, candidates, target - candidates[j], subset, result, cache, set);
            subset.RemoveAt(subset.Count - 1);
            set.Remove(candidates[j]);
        }
    }

    public class HashSetEqualityComparer<T> : IEqualityComparer<HashSet<T>>
    {
        public bool Equals(HashSet<T> x, HashSet<T> y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            
            return x.Count == y.Count && x.SetEquals(y);
        }

        public int GetHashCode(HashSet<T> obj)
        {
            return HashCode.Combine(obj.Comparer, obj.Count);
        }
    }
}
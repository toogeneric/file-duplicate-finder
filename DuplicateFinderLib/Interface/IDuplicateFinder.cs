using System.Collections.Generic;
using DuplicateFinderLib.Model;

namespace DuplicateFinderLib.Interface
{
    public interface IDuplicateFinder
    {
        IEnumerable<IDuplicate> CollectCandidates(string path);
        IEnumerable<IDuplicate> CollectCandidates(string path, CompareMode mode);
        
        IEnumerable<IDuplicate> CheckCandidates(IEnumerable<IDuplicate> duplicates);
    }
}
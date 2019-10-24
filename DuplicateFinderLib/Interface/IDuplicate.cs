using System.Collections.Generic;

namespace DuplicateFinderLib.Interface
{
    public interface IDuplicate
    {
        IEnumerable<string> FilePaths { get; }
    }
}
using System.Collections.Generic;
using DuplicateFinderLib.Interface;

namespace DuplicateFinderLib.Model
{
    public class Duplicate : IDuplicate
    {
        public IEnumerable<string> FilePaths { get; }
        
        public Duplicate(IEnumerable<string> filePaths)
        {
            this.FilePaths = filePaths;
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using DuplicateFinderLib.Interface;
using DuplicateFinderLib.Model;

namespace DuplicateFinderLib
{
    public class DuplicateFinder : IDuplicateFinder
    {
        public IEnumerable<IDuplicate> CollectCandidates(string path)
        {
            return CollectCandidates(path, CompareMode.SizeAndName);
        }

        public IEnumerable<IDuplicate> CollectCandidates(string path, CompareMode mode)
        {
            var di = new DirectoryInfo(path);
            var fileInfos = di.EnumerateFiles("*.*", SearchOption.AllDirectories);

            switch (mode)
            {
                case CompareMode.Size:
                    var groupsBySize = fileInfos.GroupBy(fi => fi.Length).Where(g => g.Count() > 1);
                    return groupsBySize.Select(g => new Duplicate(g.Select(fi => fi.FullName)));
                case CompareMode.SizeAndName:
                    var groupsBySizeAndName = fileInfos.GroupBy(fi => new {fi.Length, fi.Name}).Where(g => g.Count() > 1);
                    return groupsBySizeAndName.Select(g => new Duplicate(g.Select(fi => fi.FullName)));
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }

        public IEnumerable<IDuplicate> CheckCandidates(IEnumerable<IDuplicate> duplicates)
        {
            var duplicatesByHash = new List<IDuplicate>();
            var md5Provider = new MD5CryptoServiceProvider();
            
            foreach (var duplicate in duplicates)
            {
                var groupsByHash = duplicate.FilePaths
                    .GroupBy(fp => BitConverter.ToString(md5Provider.ComputeHash(File.ReadAllBytes(fp))))
                    .Where(g => g.Count() > 1);

                duplicatesByHash.AddRange(groupsByHash.Select(g => new Duplicate(g)));
            }

            return duplicatesByHash;
        }
    }
}
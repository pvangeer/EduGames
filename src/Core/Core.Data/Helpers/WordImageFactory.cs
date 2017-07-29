using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Core.Data.Helpers
{
    public static class WordImageFactory
    {
        private static Dictionary<string, string> allImages = null;
        private static Dictionary<IEnumerable, IEnumerable<KeyValuePair<string, string>>> imageFilesByCategory;

        public static readonly IEnumerable<char> Klinkers = new[] { 'e', 'u', 'i', 'o', 'a' };
        public static readonly IEnumerable<string> DubbeleKlinkers = new[] { "aa", "ee", "uu", "oo" };
        public static readonly IEnumerable<string> DubbelKlanken = new[] {"au", "ou", "ui", "eu", "ie", "ij"};
        public static readonly IEnumerable<string> SamengesteldeKlanken = new[] {"ch", "sch", "ng", "nk"};

        public static bool IncludeKlinkers = true;
        public static bool IncludeDubbeleKlinkers = true;
        public static bool IncludeDubbelKlanken = true;
        public static bool IncludeSamengesteldeKlanken = true;

        public static Dictionary<string, string> Images
        {
            get
            {
                if (allImages == null)
                {
                    LoadAllImages();
                }
                return FilterImages();
            }
        }

        public static Dictionary<string, string> AllImages
        {
            get
            {
                if (allImages == null)
                {
                    LoadAllImages();
                }
                return allImages;
            }
        }

        private static void LoadAllImages()
        {
            var directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");
            var ext = new List<string> { ".jpeg", ".jpg", ".gif", ".png", ".bmp" };
            allImages = Directory.GetFiles(directory, "*.*", SearchOption.AllDirectories)
                .Where(s => ext.Any(s.EndsWith))
                .ToDictionary(Path.GetFileNameWithoutExtension, f => f);
            imageFilesByCategory = new Dictionary<IEnumerable, IEnumerable<KeyValuePair<string, string>>>();
            imageFilesByCategory[SamengesteldeKlanken] = allImages.Where(i => SamengesteldeKlanken.Any(k => i.Key.Contains(k)));
            imageFilesByCategory[DubbelKlanken] = allImages.Except(imageFilesByCategory[SamengesteldeKlanken])
                .Where(i => DubbelKlanken.Any(k => i.Key.Contains(k)));
            imageFilesByCategory[DubbeleKlinkers] = allImages
                .Except(imageFilesByCategory[SamengesteldeKlanken])
                .Except(imageFilesByCategory[DubbelKlanken])
                .Where(i => DubbeleKlinkers.Any(k => i.Key.Contains(k)));
            imageFilesByCategory[Klinkers] = allImages
                .Except(imageFilesByCategory[SamengesteldeKlanken])
                .Except(imageFilesByCategory[DubbelKlanken])
                .Except(imageFilesByCategory[DubbeleKlinkers])
                .Where(i => DubbeleKlinkers.Any(k => i.Key.Contains(k)));
        }

        private static Dictionary<string, string> FilterImages()
        {
            var imagesToReturn = new Dictionary<string, string>();
            if (IncludeKlinkers)
            {
                foreach (var kv in imageFilesByCategory[Klinkers])
                {
                    imagesToReturn.Add(kv.Key, kv.Value);
                }
            }
            if (IncludeDubbeleKlinkers)
            {
                foreach (var kv in imageFilesByCategory[DubbeleKlinkers])
                {
                    imagesToReturn.Add(kv.Key, kv.Value);
                }
            }
            if (IncludeDubbelKlanken)
            {
                foreach (var kv in imageFilesByCategory[DubbelKlanken])
                {
                    imagesToReturn.Add(kv.Key, kv.Value);
                }
            }
            if (IncludeSamengesteldeKlanken)
            {
                foreach (var kv in imageFilesByCategory[SamengesteldeKlanken])
                {
                    imagesToReturn.Add(kv.Key, kv.Value);
                }
            }
            return imagesToReturn;
        }
    }
}

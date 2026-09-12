using System;
using System.Collections.Generic;
using System.Linq;
using LoadBoost.Core;
using UnityEngine;

namespace LoadBoost
{
    internal static class LoadedAssetCollector
    {
        public static List<LoadedModAssets> Collect()
        {
            var map = new Dictionary<string, LoadedModAssets>(StringComparer.OrdinalIgnoreCase);
            try
            {
                var db = GameDatabase.Instance;
                if (db == null) return new List<LoadedModAssets>();
                foreach (var tex in db.databaseTexture)
                    Bump(map, tex != null && tex.file != null ? tex.file.url : null, a => a.TextureCount++);
                foreach (var f in db.databaseModelFiles)
                    Bump(map, f != null ? f.url : null, a => a.ModelCount++);
                foreach (var f in db.databaseAudioFiles)
                    Bump(map, f != null ? f.url : null, a => a.AudioCount++);
            }
            catch (Exception e)
            {
                Debug.LogError("[LoadBoost] 资产采集异常: " + e);
            }
            return map.Values.OrderByDescending(a => a.TextureCount).ToList();
        }

        private static void Bump(Dictionary<string, LoadedModAssets> map, string url, Action<LoadedModAssets> bump)
        {
            var mod = ModOf(url);
            LoadedModAssets a;
            if (!map.TryGetValue(mod, out a))
            {
                a = new LoadedModAssets { ModName = mod };
                map[mod] = a;
            }
            bump(a);
        }

        private static string ModOf(string url)
        {
            if (string.IsNullOrEmpty(url)) return "(unknown)";
            int i = url.IndexOf('/');
            return i > 0 ? url.Substring(0, i) : url;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LabelFeaturesDomena
{
    /// <summary>
    /// Eksporter statystyk etykiet.
    /// </summary>
    public class LabelWriter
    {
        public SłownikEtykiet Labels => _labels;
        SłownikEtykiet _labels;

        public LabelWriter(SłownikEtykiet labels) { _labels = labels; }

        public IEnumerable<string> Zapisz(string fileName, IEnumerable<string> files)
        {
            var records = new List<string>();
            var etykiety = new Dictionary<string, int>();
            foreach (var label in _labels.Etykiety) etykiety.Add(label, 0);
            foreach (var file in files)
            {
                var obraz = new Obraz(file);
                var etykieta = obraz.Etykieta;
                etykiety[etykieta] = etykiety[etykieta] + 1;
            }
            foreach (var kv in etykiety) records.Add(string.Format("{0}: {1}", kv.Key, kv.Value));
            File.WriteAllLines(fileName, records);
            return records;
        }
    }
}

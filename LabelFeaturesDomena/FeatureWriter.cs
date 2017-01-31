using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace LabelFeaturesDomena
{
    /// <summary>
    /// Eksporter etykiet obrazów do pliku.
    /// </summary>
    public class FeatureWriter
    {
        public SłownikEtykiet Labels => _labels;
        SłownikEtykiet _labels;
        public bool ShowFile { get; set; }

        public FeatureWriter(SłownikEtykiet labels) { _labels = labels; }

        /// <summary>
        /// Eksportuje etykiety do pliku.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="files"></param>
        /// <returns>|labels 0 1 0 0 |features 0 99 0 255 0 0 0 0 0 0 0 1</returns>
        public IEnumerable<string> Zapisz(string fileName, IEnumerable<string> files)
        {
            var records = new List<string>();
            foreach (var file in files)
            {
                var obraz = new Obraz(file);
                var etykieta = obraz.Etykieta;
                var labels = _labels.LabelToBinaryArray(etykieta);
                var features = obraz.GetBitmapFeatures();
                var labelsJoin = string.Join(" ", labels);
                var featuresJoin = string.Join(" ", features);
                records.Add("|labels " + labelsJoin + " |features " + featuresJoin);
            }
            File.WriteAllLines(fileName, records);
            if (ShowFile) Process.Start(fileName);
            return records;
        }
    }
}

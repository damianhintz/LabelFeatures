using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LabelFeaturesDomena
{
    /// <summary>
    /// Podział plików na zbiór treningowy i testowy.
    /// </summary>
    public class LabelsFeaturesSplitter
    {
        public void Split(string fileName, int percent)
        {
            //|labels 0 |features 0
            var records = File.ReadAllLines(fileName);
            var groups = records.GroupBy(record => record.Substring(0, record.IndexOf("|features")));
            var records70 = new List<string>();
            var records30 = new List<string>();
            Console.WriteLine("Etykiety: " + groups.Count() + ", " + records.Count() + " plik(i)");
            foreach (var group in groups)
            {
                var key = group.Key;
                var train = (percent * group.Count()) / 100;
                //var count30 = count - count70;
                foreach (var record in group.Take(train)) records70.Add(record);
                foreach (var record in group.Skip(train)) records30.Add(record);
            }
            var count70 = records70.Count();
            Console.WriteLine("Train: " + count70);
            File.WriteAllLines(fileName + "_Train" + percent + "_" + count70 + ".txt", records70);
            var count30 = records30.Count();
            Console.WriteLine("Test: " + count30);
            File.WriteAllLines(fileName + "_Test" + percent + "_" + count30 + ".txt", records30);
        }
    }
}

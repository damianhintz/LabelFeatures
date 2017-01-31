using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Diagnostics;
using LabelFeaturesDomena;

namespace LabelFeatures
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("LabelFeatures v1.0-beta, 31 stycznia 2017");
            Console.WriteLine("Roboczy katalog: {0}", args[0]);
            var files = Directory.GetFiles(args.First(), "*.png", SearchOption.AllDirectories);
            var labels = new SłownikEtykiet();
            if (args.Length > 1)
            {
                var labelsPath = args[1];
                Console.WriteLine("Wczytywanie etykiet z pliku " + labelsPath);
                labels = SłownikEtykiet.Wczytaj(labelsPath);
            }
            else
            {
                Console.WriteLine("Generowanie etykiet z " + files.Length + " plik(i)...");
                labels = SłownikEtykiet.AutoLabel(files);
            }
            Console.WriteLine("Etykiety: {0}", labels.Etykiety.Count());
            foreach (var e in labels.Etykiety) Console.WriteLine(e + ": " + string.Join(",", labels.LabelToBinaryArray(e)));
            var fileName = "LabelFeatures_T" + DateTime.Now.ToShortDateString() + ".txt";
            Console.WriteLine("Etykietowanie " + files.Length + " plików -> " + fileName);
            var writer = new FeatureWriter(labels);
            var records = writer.Zapisz(fileName, files);
            Console.WriteLine("Koniec.");
            Console.Read();
        }

    }
}

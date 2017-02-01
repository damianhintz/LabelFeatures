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
            Console.WriteLine("LabelFeatures v1.2-beta, 1 lutego 2017");
            Console.WriteLine("Etykietuj pliki obrazów i twórz zbiory treningowe i testowe");
            var fileOrFolder = ".";
            if (args.Any()) fileOrFolder = args.First();
            Console.WriteLine(fileOrFolder); //Podany plik lub folder
            var program = new Program();
            if (File.Exists(fileOrFolder))
            {
                program.PodzielPliki(fileOrFolder, args.Length > 1 ? int.Parse(args[1]) : 70);
            }
            else if (Directory.Exists(fileOrFolder))
            {
                program.EtykietujPliki(fileOrFolder, args.Length > 1 ? args[1] : string.Empty);
            }
            else Console.WriteLine("Pierwszy argument nie jest prawidłowym plikiem ani katalogiem!");
            Console.WriteLine("Koniec.");
            Console.Read();
        }

        void PodzielPliki(string file, int procent)
        {
            var split = new LabelsFeaturesSplitter();
            split.Split(file, procent);
        }

        void EtykietujPliki(string folder, string labelsPath = null)
        {
            var files = Directory.GetFiles(folder, "*.png", SearchOption.AllDirectories);
            var labels = new SłownikEtykiet();
            if (File.Exists(labelsPath))
            {
                Console.WriteLine("Wczytywanie słownika etykiet z pliku " + labelsPath);
                labels = SłownikEtykiet.Wczytaj(labelsPath);
            }
            else
            {
                Console.WriteLine("Generowanie słownika etykiet z " + files.Length + " plik(i)...");
                labels = SłownikEtykiet.AutoLabel(files);
            }
            Console.WriteLine("Etykiety: {0}", labels.Etykiety.Count());
            foreach (var e in labels.Etykiety) Console.WriteLine(e + ": " + string.Join(",", labels.LabelToBinaryArray(e)));
            var fileName = "LabelFeatures_T" + DateTime.Now.ToShortDateString() + ".txt";
            Console.WriteLine("Etykietowanie " + files.Length + " plików -> " + fileName);
            var writer = new FeatureWriter(labels);
            writer.Zapisz(fileName, files);
            var labelsWriter = new LabelWriter(labels);
            var labelPath = "LabelFeatures_LabelsT" + DateTime.Now.ToShortDateString() + ".txt";
            Console.WriteLine("Eksportowanie etykiet do pliku -> " + labelPath);
            var records = labelsWriter.Zapisz(labelPath, files);
            foreach (var record in records) Console.WriteLine(record);
        }

    }
}

using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LabelFeaturesDomena;
using Shouldly;

namespace LabelFeaturesTesty
{
    [TestClass]
    public class LabelWriterTest
    {
        [TestMethod]
        public void LabelWriter_ShouldExportLabelsToFile()
        {
            var files = Directory.GetFiles("Samples", "*.png");
            files.Length.ShouldBe(2);
            var labels = SłownikEtykiet.AutoLabel(files);
            labels.Etykiety.Count().ShouldBe(2);
            var writer = new LabelWriter(labels);
            var records = writer.Zapisz("Labels.txt", files);
            records.Count().ShouldBe(2);
            var first = records.First();
            first.ShouldStartWith("I: 1");
            var last = records.Last();
            last.ShouldStartWith("X: 1");
            File.Exists("Labels.txt").ShouldBe(true);
        }
    }
}

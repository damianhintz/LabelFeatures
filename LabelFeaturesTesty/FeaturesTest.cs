using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LabelFeaturesDomena;
using Shouldly;

namespace LabelFeaturesTesty
{
    [TestClass]
    public class FeaturesTest
    {
        [TestMethod]
        public void FeatureWriter_ShouldNotShowFile()
        {
            var labels = new SłownikEtykiet();
            var writer = new FeatureWriter(labels);
            writer.Labels.ShouldBeSameAs(labels);
            writer.ShowFile.ShouldBeFalse();
        }

        [TestMethod]
        public void FeatureWriter_ShouldExportLabelsAndFeaturesToFile()
        {
            var files = Directory.GetFiles("Samples", "*.png");
            files.Length.ShouldBe(2);
            var labels = SłownikEtykiet.AutoLabel(files);
            labels.Etykiety.Count().ShouldBe(2);
            var writer = new FeatureWriter(labels);
            var records = writer.Zapisz("LabelsFeatures.txt", files);
            records.Count().ShouldBe(2);
            var first = records.First();
            first.ShouldStartWith("|labels 1 0 |features ");
            var last = records.Last();
            last.ShouldStartWith("|labels 0 1 |features ");
            File.Exists("LabelsFeatures.txt").ShouldBe(true);
        }
    }
}

using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LabelFeaturesDomena;
using Shouldly;

namespace LabelFeaturesTesty
{
    [TestClass]
    public class ObrazTest
    {
        [TestMethod]
        public void Obraz_ShouldHaveLabel()
        {
            var obraz = new Obraz(fileName: @"c:\Obrazy\Test_Label1.jpg");
            obraz.Etykieta.ShouldBe("Label1");
        }

        [TestMethod]
        public void Obraz_ShouldHaveLabelWithoutUnderscore()
        {
            var obraz = new Obraz(fileName: @"c:\Obrazy\LabelWithoutUnderscore2.jpg");
            obraz.Etykieta.ShouldBe("LabelWithoutUnderscore2");
        }

        [TestMethod]
        public void Obraz_ShouldReturnBitmapAsFeaturesOfBytes()
        {
            var obraz = new Obraz(fileName: @"Samples\0_I.png");
            obraz.Etykieta.ShouldBe("I");
            var features = obraz.GetBitmapFeatures();
            features.Count().ShouldBe(28 * 28);
        }
    }
}

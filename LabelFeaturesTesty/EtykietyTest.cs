using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LabelFeaturesDomena;
using Shouldly;

namespace LabelFeaturesTesty
{
    [TestClass]
    public class EtykietyTest
    {
        [TestMethod]
        public void SłownikEtykiet_ShouldBeEmpty()
        {
            var etykiety = new SłownikEtykiet();
            etykiety.Etykiety.ShouldBeEmpty();
        }

        [TestMethod]
        public void SłownikEtykiet_ShouldNotBeEmpty()
        {
            var etykiety = new SłownikEtykiet();
            etykiety.Dodaj("I", 0);
            etykiety.Etykiety.ShouldHaveSingleItem();
        }

        [TestMethod]
        public void SłownikEtykiet_ShouldReturnLabelAsBinaryArray()
        {
            var etykiety = new SłownikEtykiet();
            for (int i = 0; i < 10; i++)
            {
                etykiety.Dodaj(i.ToString(), i);
            }
            for (int i = 0; i < 10; i++)
            {
                var label = etykiety.LabelToBinaryArray(i.ToString());
                label.Count().ShouldBe(10);
                for (int j = 0; j < 10; j++)
                {
                    if (i == j) label.ElementAt(j).ShouldBe(1);
                    else label.ElementAt(j).ShouldBe(0);
                }
            }
        }

        [TestMethod]
        public void SłownikEtykiet_ShouldBeOrderedByIndeks()
        {
            var etykiety = new SłownikEtykiet();
            etykiety.Dodaj("B", 1);
            etykiety.Dodaj("A", 0);
            etykiety.Etykiety.Count().ShouldBe(2);
            var first = etykiety.Etykiety.First();
            var last = etykiety.Etykiety.Last();
            first.ShouldBe("A");
            last.ShouldBe("B");
        }

        [TestMethod]
        public void SłownikEtykiet_ShouldAllowOnlyUniqueKeys()
        {
            var etykiety = new SłownikEtykiet();
            etykiety.Dodaj("I", 0);
            Should.Throw<InvalidOperationException>(() =>
            {
                etykiety.Dodaj("I", 1);
            });
        }

        [TestMethod]
        public void SłownikEtykiet_ShouldAllowOnlyUniqueValues()
        {
            var etykiety = new SłownikEtykiet();
            etykiety.Dodaj("I", 0);
            Should.Throw<InvalidOperationException>(() =>
            {
                etykiety.Dodaj("B", 0);
            });
        }

        [TestMethod]
        public void SłownikEtykiet_ShouldBeReadFromFile()
        {
            var etykiety = SłownikEtykiet.Wczytaj(@"Samples\Labels.txt");
            etykiety.Etykiety.Count().ShouldBe(19);
            var join = string.Join("", etykiety.Etykiety);
            join.ShouldBe("ABDGIMOPEPGPIPKPKLSSPSTTWZZG");
        }

        [TestMethod]
        public void SłownikEtykiet_ShouldAutoLabelFiles()
        {
            var etykiety = SłownikEtykiet.AutoLabel(new[] {
                @"0_A.txt",
                @"1_B.txt",
                @"0_A.txt",
                @"1_C.txt",
                @"2_C.txt",
            });
            etykiety.Etykiety.Count().ShouldBe(3);
            var join = string.Join("", etykiety.Etykiety);
            join.ShouldBe("ABC");
        }
    }
}

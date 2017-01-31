using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabelFeaturesDomena
{
    /// <summary>
    /// Reprezentuje słownik etykiet, które nadamy obrazom.
    /// </summary>
    public class SłownikEtykiet
    {
        /// <summary>
        /// Lista etykiet posortowana rosnąco według indeksu etykiety.
        /// </summary>
        public IEnumerable<string> Etykiety => from e in _etykiety orderby e.Value select e.Key;
        Dictionary<string, int> _etykiety = new Dictionary<string, int>();
        Dictionary<int, string> _indeksy = new Dictionary<int, string>();

        /// <summary>
        /// Dodaj etykietę o podanej nazwie i indeksie.
        /// </summary>
        /// <param name="etykieta"></param>
        /// <param name="index"></param>
        public void Dodaj(string etykieta, int index)
        {
            if (_etykiety.ContainsKey(etykieta)) throw new InvalidOperationException("Mapa etykiet zawiera już podaną etykietę: " + etykieta);
            if (_indeksy.ContainsKey(index)) throw new InvalidOperationException("Mapa etykiet zawiera już podany indeks: " + index);
            _etykiety.Add(etykieta, index);
            _indeksy.Add(index, etykieta);
        }

        /// <summary>
        /// Zamień etykietę na tablicę binarną.
        /// </summary>
        /// <param name="etykieta"></param>
        /// <returns></returns>
        public IEnumerable<int> LabelToBinaryArray(string etykieta)
        {
            var labelIndex = _etykiety[etykieta]; //Pozycja etykiety w tablicy
            var array01 = new int[_etykiety.Count];
            array01[labelIndex] = 1;
            return array01;
        }

        public static SłownikEtykiet Wczytaj(string fileName)
        {
            var records = File.ReadAllLines(fileName);
            var lables = new SłownikEtykiet();
            for (int i = 0; i < records.Length; i++)
                lables.Dodaj(etykieta: records[i], index: i);
            return lables;
        }

        public static SłownikEtykiet AutoLabel(IEnumerable<string> files)
        {
            var labels = new SłownikEtykiet();
            var uniqLabels = new HashSet<string>();
            foreach (var fileName in files)
            {
                var obraz = new Obraz(fileName);
                uniqLabels.Add(obraz.Etykieta);
            }
            int i = 0;
            foreach (var label in uniqLabels) labels.Dodaj(label, i++);
            return labels;
        }
    }
}

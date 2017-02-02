using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;

namespace LabelFeaturesDomena
{
    /// <summary>
    /// Reprezentuje obraz, któremu chcemy nadać etykietę.
    /// </summary>
    public class Obraz
    {
        public string Etykieta => Path.GetFileNameWithoutExtension(_fileName).Split('_').Last();

        string _fileName;

        public Obraz(string fileName) { _fileName = fileName; }

        public IEnumerable<byte> GetBitmapFeatures(bool color = false)
        {
            var features = new List<byte>();
            var bitmap = new Bitmap(_fileName);
            for (int i = 0; i < bitmap.Width; i++)
            {
                //Kanał R
                for (int j = 0; j < bitmap.Height; j++)
                {
                    var pixel = bitmap.GetPixel(i, j);
                    features.Add(pixel.R);
                    if (color)
                    {
                        features.Add(pixel.G);
                        features.Add(pixel.B);
                    }
                }
            }
            return features;
        }

    }
}

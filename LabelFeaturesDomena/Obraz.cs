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

        public IEnumerable<byte> GetBitmapFeatures()
        {
            var features = new List<byte>();
            var bitmap = new Bitmap(_fileName);
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    var pixel = bitmap.GetPixel(i, j);
                    var color = (pixel.R + pixel.G + pixel.B) / 3; //średnia
                    var colorByte = (byte)color;
                    features.Add(colorByte);
                }
            }
            return features;
        }
    }
}

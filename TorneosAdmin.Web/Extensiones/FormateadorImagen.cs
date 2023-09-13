using SkiaSharp;
using System;
using System.IO;

namespace TorneosAdmin.Web.Extensiones
{
    public class FormateadorImagen
    {
        public static byte[] CambiarTamanio(string fileContents, int maxWidth, int maxHeight)
        {
            using FileStream ms = new FileStream(fileContents, FileMode.Open);
            using SKBitmap sourceBitmap = SKBitmap.Decode(ms);

            int height = Math.Min(maxHeight, sourceBitmap.Height);
            int width = Math.Min(maxWidth, sourceBitmap.Width);

            using SKBitmap scaledBitmap = sourceBitmap.Resize(new SKImageInfo(width, height), SKFilterQuality.High);
            using SKImage scaledImage = SKImage.FromBitmap(scaledBitmap);
            using SKData data = scaledImage.Encode();

            return data.ToArray();
        }

    }
}

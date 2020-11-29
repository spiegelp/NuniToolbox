using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace NuniToolbox.Ui.Converters
{
    /// <summary>
    /// Converts a byte array into a <see cref="BitmapImage" />.
    /// </summary>
    public class ByteArrayToBitmapImageConverter : IValueConverter
    {
        /// <summary>
        /// Creates a new <see cref="ByteArrayToBitmapImageConverter" />.
        /// </summary>
        public ByteArrayToBitmapImageConverter() { }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is byte[] data)
            {
                using MemoryStream ms = new MemoryStream(data);

                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = ms;
                bitmapImage.EndInit();

                return bitmapImage;
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}

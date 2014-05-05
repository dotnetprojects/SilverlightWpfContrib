using System;
using System.IO;

namespace SilverlightContrib.Imaging
{
    /// <summary>
    /// Bitmap File Header class used y BmpDecoder.
    /// </summary>
    public class BmpFileHeader
    {
        /// Original EditableImage and PngEncoder classes courtesy Joe Stegman.
        /// http://blogs.msdn.com/jstegman

        private const int _SIZE = 14;

        /// <summary>
        /// 
        /// </summary>
        public short BitmapType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public short NA1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public short NA2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int OffsetToData { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static BmpFileHeader FillFromStream(Stream stream)
        {
            byte[] buffer = new byte[_SIZE];
            BmpFileHeader header = new BmpFileHeader();

            stream.Read(buffer, 0, _SIZE);

            // Fill
            header.BitmapType = BitConverter.ToInt16(buffer, 0);
            header.Size = BitConverter.ToInt32(buffer, 2);
            header.OffsetToData = BitConverter.ToInt32(buffer, 10);

            // Return results
            return header;
        }
    }
}

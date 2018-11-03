using System;
using System.Drawing;

namespace Generator.Model
{
    [Serializable]
    public class PoreDto
    {
        public Bitmap PoreImage { get; set; }
        public double Area { get; set; }
        public Guid Id { get; set; }
    }
}

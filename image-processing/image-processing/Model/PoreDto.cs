using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace image_processing.Model
{
    [Serializable]
    public class PoreDto
    {
        public Bitmap PoreImage { get; set; }
        public double Area { get; set; }
        public Guid Id { get; set; }
    }
}

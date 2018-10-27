using image_processing.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace image_processing.Model
{
    [Serializable]
    public class PoresKnowledgeBaseDo
    {
        public Dictionary<double[],Guid> PoresDictionary { get; set; }
        public List<PoreDto> PoresData { get; set; }
    }
}

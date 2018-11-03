using System;
using System.Collections.Generic;

namespace Generator.Model
{
    [Serializable]
    public class PoresKnowledgeBaseDo
    {
        public Dictionary<double[],Guid> PoresDictionary { get; set; }
        public List<PoreDto> PoresData { get; set; }
    }
}

using System.Collections.Generic;

namespace IntelMetTask
{
    public class SlabMeasure
    {
        public SlabMeasure(List<Distance> distances)
        {
            Distances = distances;
        }

        public List<Distance> Distances { get; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace IntelMetTask
{
    public static class NoiseRemover
    {
        public static List<Distance> CleanData(List<Distance> data)
        {
            var result = new List<Distance>();
            
            const float decayFactor = 0.1f;
            
            float previousAverageSpeed = 0;
            bool hasNonZeroSpeed = false;

            foreach (var point in data)
            {
                if (point.Speed != 0)
                {
                    hasNonZeroSpeed = true;
                    float alpha = decayFactor / (1 - decayFactor);
                    float averageSpeed = alpha * point.Speed + (1 - alpha) * previousAverageSpeed;
                    previousAverageSpeed = averageSpeed;
                    result.Add(new Distance(averageSpeed, point.Value));
                }
                else if (hasNonZeroSpeed)
                {
                    result.Add(new Distance(previousAverageSpeed, point.Value));
                }
                else
                {
                    result.Add(point);
                }
            }

            return result;
        }
    }
}
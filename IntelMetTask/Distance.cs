using Newtonsoft.Json;

namespace IntelMetTask
{
    public class Distance
    {
        public Distance(float speed, float distance)
        {
            Speed = speed;
            Value = distance;
        }

        public float Speed { get; }
        
        [JsonProperty("Distance")]
        public float Value { get; }
    }
}
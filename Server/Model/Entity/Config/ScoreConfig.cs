using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ETModel
{
    [ConfigAttribute(AppType.AllServer)]
    public partial class ScoreConfigCategory : ACategory<ScoreConfig>
    {
    }

    public class ScoreConfig: IConfig
    {
        public long Id { get; set; }
        public int MinX { get; set; }
        public int MaxX { get; set; }
        public float Y { get; set; }
        public int MinZ { get; set; }
        public int MaxZ { get; set; }
    }
}
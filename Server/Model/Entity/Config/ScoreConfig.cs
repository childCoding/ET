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
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
    }
}
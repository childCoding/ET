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
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
    }
}
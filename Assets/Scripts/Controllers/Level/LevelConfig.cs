using Newtonsoft.Json;

namespace WildGunman.Controllers.Level
{
    public class LevelConfig
    {
        public string Id => _id;
        public int Order => _order;
        public string LocKey => _locKey;
        public string ResourcesPrefabPath => _resourcesPrefabPath;
        public LevelCharactersConfig Characters => _characters;

        [JsonProperty("id")] private string _id;
        [JsonProperty("order")] private int _order;
        [JsonProperty("locKey")] private string _locKey;
        [JsonProperty("resourcesPrefabPath")] private string _resourcesPrefabPath;
        [JsonProperty("characters")] private LevelCharactersConfig _characters;
    }

    public class LevelCharactersConfig
    {
        public float MinSpawnDelaySec => _minSpawnDelaySec;
        public float MaxSpawnDelaySec => _maxSpawnDelaySec;
        public int MaxSpawnCount => _maxSpawnCount;
        public float MinWaitingDelaySec => _minWaitingDelaySec;
        public float MaxWaitingDelaySec => _maxWaitingDelaySec;
        public int BonusPoints => _bonusPoints;

        [JsonProperty("minSpawnDelaySec")] private float _minSpawnDelaySec;
        [JsonProperty("maxSpawnDelaySec")] private float _maxSpawnDelaySec;
        [JsonProperty("maxSpawnCount")] private int _maxSpawnCount;
        [JsonProperty("minWaitingDelaySec")] private float _minWaitingDelaySec;
        [JsonProperty("maxWaitingDelaySec")] private float _maxWaitingDelaySec;
        [JsonProperty("bonusPoints")] private int _bonusPoints;
    }
}
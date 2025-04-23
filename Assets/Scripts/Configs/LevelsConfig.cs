using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "Configs/LevelConfig")]
public class LevelsConfig : ScriptableObject
{
    [SerializeField] private List<LevelConfig> _levelConfigList;

    public List<LevelConfig> LevelConfigList => _levelConfigList;

    public string LEVEL_SAVE_NAME = "level_number";
}

public class LevelConfig
{
    public int CollectableNumber = 5;
}
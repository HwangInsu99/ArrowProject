using System;
using System.Collections.Generic;
using UnityEngine;

public enum ESceneID
{
    Lobby = 0,
    Stage = 1
}

[Serializable]
public class SceneEntry
{
    public ESceneID Id;
    public string SceneName;
}

public class SceneCatalog : MonoBehaviour
{
    [SerializeField] private List<SceneEntry> _scenes = new List<SceneEntry>();

    private readonly Dictionary<ESceneID, string> _idToName = new Dictionary<ESceneID, string>();
    private readonly Dictionary<string, ESceneID> _nameToId = new Dictionary<string, ESceneID>();
    void Awake()
    {
        BuildMaps();
    }

    public void BuildMaps()
    {
        _idToName.Clear();
        _nameToId.Clear();

        for (int i = 0; i < _scenes.Count; i++)
        {
            SceneEntry entry = _scenes[i];
            if (entry == null)
            {
                continue;
            }

            if (string.IsNullOrEmpty(entry.SceneName))
            {
                Debug.LogWarning($"SceneEntry 비어있음 / Id = {entry.Id}");
                continue;
            }

            if (_idToName.ContainsKey(entry.Id))
            {
                Debug.LogWarning($"Id 중복 : {entry.Id}");
                continue;
            }

            if (_nameToId.ContainsKey(entry.SceneName))
            {
                Debug.LogWarning($"이름 중복 : {entry.SceneName}");
                continue;
            }

            _idToName.Add(entry.Id, entry.SceneName);
            _nameToId.Add(entry.SceneName, entry.Id);
        }

        Debug.Log($"SceneList Count = {_scenes.Count}");
        Debug.Log($"Map (Id -> Name) = {_idToName.Count}");
        Debug.Log($"Map (Name -> Id) = {_nameToId.Count}");
    }

    public bool TryGetSceneName(ESceneID id, out string sceneName)
    {

        return _idToName.TryGetValue(id, out sceneName);
    }

    public bool TryGetSceneId(string sceneName, out ESceneID id)
    {
        return _nameToId.TryGetValue(sceneName, out id);
    }

    public List<SceneEntry> GetEntries()
    {
        return _scenes;
    }
}

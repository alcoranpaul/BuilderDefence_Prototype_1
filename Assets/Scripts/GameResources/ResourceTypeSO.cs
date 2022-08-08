using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ResourceType")]
public class ResourceTypeSO : ScriptableObject {
    [SerializeField] private string nameString;
    [SerializeField] private Sprite sprite;
    [SerializeField] private string shortName;
    [SerializeField] private string colorHex;
    public string GetName() {
        return nameString;
    }

    public Sprite GetSprite() {
        return sprite;
    }

    public string GetShortName() {
        return shortName;
    }

    public string GetColorHex() {
        return colorHex;
    }
}

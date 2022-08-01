using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ResourceType")]
public class ResourceTypeSO : ScriptableObject {
    [SerializeField] private string nameString;
    [SerializeField] private Sprite sprite;
    public string GetName() {
        return nameString;
    }

    public Sprite GetSprite() {
        return sprite;
    }

}

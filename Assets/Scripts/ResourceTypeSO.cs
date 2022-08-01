using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ResourceType")]
public class ResourceTypeSO : ScriptableObject {
    [SerializeField] private string nameString;

    public string GetName() {
        return nameString;
    }

}

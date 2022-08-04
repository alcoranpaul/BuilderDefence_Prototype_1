using UnityEngine;

public class ResourceNode : MonoBehaviour {
    public ResourceTypeSO resourceType;
    public int resourceAmount;
    public bool isEmpty = false;

    public void RemoveResourceAmount() {
        resourceAmount--;
        if (resourceAmount <= 0) {
            Destroy(gameObject);
            isEmpty = true;
        }
    }
}

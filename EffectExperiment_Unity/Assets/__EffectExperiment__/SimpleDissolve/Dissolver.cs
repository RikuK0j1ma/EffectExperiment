using UnityEngine;

public class Dissolver : MonoBehaviour
{
    [SerializeField] Material dissolveMaterial;

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log($"Hit {hit.textureCoord}");
            Vector2 uv = hit.textureCoord;
            dissolveMaterial.SetVector("_DissolveCenter", uv);
        }
    }
}

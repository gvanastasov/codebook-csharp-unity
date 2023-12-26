using UnityEngine;

public class MaterialSwapper : MonoBehaviour
{
    public Material[] materials;

    public int startMaterialIdx = 0;

    public void SwapMaterial()
    {
        GetComponent<Renderer>().material = materials[startMaterialIdx];

        startMaterialIdx = (startMaterialIdx + 1) % materials.Length;
    }
}

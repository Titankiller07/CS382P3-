using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [Header("Inscribed")]
    public float rotationsPerSecond = 0.1f;

    [Header("Dynamic")]
    public int levelShown = 0;
    Material mat;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        int currentLevel = Mathf.FloorToInt(Hero.S.shieldLevel);
        if (levelShown != currentLevel)
        {
            levelShown = currentLevel;
            mat.mainTextureOffset = new Vector2(0.2f * levelShown, 0);
        }
        float rZ = -(rotationsPerSecond * Time.time * 360) % 360f;
        transform.rotation = Quaternion.Euler(0, 0, rZ);
    }
}

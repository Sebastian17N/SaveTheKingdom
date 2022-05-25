using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellDataFolder : MonoBehaviour
{
    public SpellScriptableObject SpellScriptableObject;
    public Sprite Sprite;
    public GameObject SpellDataFolderPrefab;

    private void Start()
    {
        CreateSpellDataFolder();
    }
    private void CreateSpellDataFolder()
    {
        SpellDataFolderPrefab = Instantiate(transform.gameObject);
    }
}

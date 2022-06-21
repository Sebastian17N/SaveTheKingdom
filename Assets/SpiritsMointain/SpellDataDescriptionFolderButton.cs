using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellDataDescriptionFolderButton : MonoBehaviour
{
    //public SpellScriptableObject SpellScriptableObject;
    //public SpellCardManager SpellCardManager;
    public SpellScriptableObject[] ScriptableObjects;    
    public Sprite Sprite;

    void Start()
    {
        foreach (SpellScriptableObject scriptableObject in ScriptableObjects)
        {
            CreateSpellDataDescriptionFolder(scriptableObject);
        }
    }
    private void CreateSpellDataDescriptionFolder(SpellScriptableObject spellScriptableObject)
    {

        var SpellFolder = transform.Find("SpellPrefab(Clone)");
        SpellFolder.GetComponentInChildren<Image>().sprite = spellScriptableObject.Sprite;
    }
}
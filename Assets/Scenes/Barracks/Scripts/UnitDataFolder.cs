using Assets.Units.Defenses.Scripts;
using TMPro;
using UnityEngine;

namespace Assets.Scenes.Barracks.Scripts
{
    public class UnitDataFolder : MonoBehaviour
    {
        public UnitScriptableObject UnitScriptableObject;
        public int UnitIndex;

        public void RefreshStatisticsTexts()
        {
            var unit = transform.gameObject;
            var damageObject = unit.transform.Find("Damage");
            damageObject.transform.Find("DamageNumber").GetComponent<TMP_Text>().text =
                    $"Damage: {UnitScriptableObject.AttackDamage}";

            var healthObject = unit.transform.Find("Health");
            healthObject.transform.Find("HealthNumber").GetComponent<TMP_Text>().text =
                    $"Health: {UnitScriptableObject.Health}";

            var levelObject = unit.transform.Find("Level").GetComponent<TMP_Text>().text =
                    $"Level: {UnitScriptableObject.Level}";
        }

    }

}

using Assets.Common.JsonModel;
using System.IO;
using UnityEngine;

namespace Assets.Common
{
	public class JsonLoader
    {
		public static MapsConfigJsonModel LoadConfig(string levelName)
		{
			if (!File.Exists($"Assets/Map/Configs/{levelName}.json"))
				return null;

			var fileData = File.ReadAllText($"Assets/Map/Configs/{levelName}.json");

			return JsonUtility.FromJson<MapsConfigJsonModel>(fileData);
		}
    }
}

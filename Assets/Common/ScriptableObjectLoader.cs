using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Common
{
	public class ScriptableObjectLoader
	{
		public static List<ScriptableObject> LoadAllScriptableObjectsFromFolder(string folderPath)
			=>
				Resources.LoadAll<ScriptableObject>(folderPath)
					.ToList();
	}
}
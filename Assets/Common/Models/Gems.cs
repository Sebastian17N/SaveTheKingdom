using Assets.Common.Enums;

namespace Assets.Common.Models
{
	[System.Serializable]
	public class Gems
	{
		public int Count;
		[System.NonSerialized]
		public Enums.ResourcesTypeEnum TypeEnum;
	}
}

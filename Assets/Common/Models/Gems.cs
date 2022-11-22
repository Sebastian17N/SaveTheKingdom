using Assets.Common.Enums;

namespace Assets.Common.Models
{
	[System.Serializable]
	public class Gems
	{
		public int Amount;
		[System.NonSerialized]
		public ResourcesTypeEnum TypeEnum;
	}
}

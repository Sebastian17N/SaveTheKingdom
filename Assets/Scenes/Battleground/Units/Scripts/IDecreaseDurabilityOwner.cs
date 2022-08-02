namespace Assets.Units.Scripts
{
	internal interface IDecreaseDurabilityOwner
	{
		/// <summary>
		/// Decrease health of unit till her death.
		/// </summary>
		/// <param name="amount">Amount of damage.</param>
		/// <returns>True - if unit still exisits. False - if unit died.</returns>
		bool DecreaseDurability(float amount);
	}
}

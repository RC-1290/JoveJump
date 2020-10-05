// Copyright Code Animo� (Laurens Mathot) � 2020, All rights reserved. Example project for 5th Planet Games
using UnityEngine;

namespace CodeAnimo
{
	public class PlatformJumpCounter : MonoBehaviour
	{
		public void CountJump(Score scoreKeeper)
		{
			++scoreKeeper.PlatformCount;
			Destroy(this);
		}
	}
}
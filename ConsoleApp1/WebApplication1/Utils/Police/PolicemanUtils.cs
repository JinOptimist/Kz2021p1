using System;
using System.Collections.Generic;

namespace WebApplication1.Utils.Police
{
	public static class PolicemanUtils
	{
		public static long SearchRandom(List<long> policeId)
		{
			int randomNumber = new Random().Next(0, policeId.Count);

			return policeId[randomNumber];
		}
	}
}

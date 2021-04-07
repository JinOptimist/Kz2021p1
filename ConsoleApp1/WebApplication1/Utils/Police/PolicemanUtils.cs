using System;

namespace WebApplication1.Utils.Police
{
	public static class PolicemanUtils
	{
		public static long SearchRandom(int policeCount)
		{
			return new Random().Next(1, policeCount);
		}
	}
}

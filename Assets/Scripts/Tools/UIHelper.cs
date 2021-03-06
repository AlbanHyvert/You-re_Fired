﻿namespace Helper
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public static class UIHelper
	{

		public static string FormatIntegerString(float value)
		{
			string ret = "";
			char unitCharacter = '\0';
			float tmpValue = 0.0f;

            if(value >= 1000000000)
            {
                tmpValue = value / 1000000000;
                unitCharacter = 'M';
            }

            else if (value >= 1000000)
			{
				tmpValue = value / 1000000;
				unitCharacter = 'm';
			}
			else if (value >= 1000)
			{
				tmpValue = value / 1000;
				unitCharacter = 'k';
			}
			else
			{
				tmpValue = value;
			}

			if (unitCharacter != '\0')
			{
				tmpValue = (float)Math.Truncate(tmpValue * 10) / 10;
				ret = tmpValue.ToString("#.#") + unitCharacter;
			}
			else
			{
				tmpValue = (float)Math.Truncate(tmpValue * 10) / 10;
				ret = tmpValue.ToString("#.#");
			}

			if (ret == string.Empty)
			{
				ret = "0";
			}
			return ret;
		}

		public static string FormatTimeSpanToString(TimeSpan timeSpan)
		{
			string ret = "";
			if (timeSpan.Days >= 1)
			{
				ret = timeSpan.Days.ToString() + "d ";
			}
			if (timeSpan.Hours >= 1)
			{
				ret += timeSpan.Hours.ToString() + "h ";
			}
			if (timeSpan.Minutes >= 1)
			{
				ret += timeSpan.Minutes.ToString() + "m ";
			}
			ret += timeSpan.Seconds.ToString() + "s";

			return ret;
		}
	}
}
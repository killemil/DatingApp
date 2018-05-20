﻿using System;

namespace DatingApp.Common.Infrastructure.Extensions
{
    public static class DateTimeExtensions
    {
        public static int CalculateAge(this DateTime theDateTime)
        {
            var age = DateTime.Today.Year - theDateTime.Year;

            if (theDateTime.AddYears(age) > DateTime.Today)
            {
                age--;
            }

            return age;
        }
    }
}

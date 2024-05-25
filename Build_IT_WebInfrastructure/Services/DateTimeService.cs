using Build_IT_CommonTools.Interfaces;
using Build_IT_WebApplication.Common.Interfaces;
using System;

namespace Build_IT_WebInfrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
        public DateTime UtcNow => DateTime.UtcNow;
    }
}

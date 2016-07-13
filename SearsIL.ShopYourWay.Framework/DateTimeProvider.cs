using System;

namespace SearsIL.ShopYourWay.Framework
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }

    class DateTimeProvider : IDateTimeProvider

    {
        public DateTime UtcNow => DateTime.UtcNow;    
    }
}

namespace Api.GRRInnovations.TodoManager.Domain.Extensions
{
    public static class DateTimeExtension
    {
        public static double TimeStamp(this DateTime time)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var unixDateTime = (time.ToUniversalTime() - epoch).TotalSeconds;

            return unixDateTime;
        }
    }
}

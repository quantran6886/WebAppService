namespace WebAppService.Areas.Admin._Helper
{
    public class TimeAgo
    {
        public static string GetTime(DateTime dateTime)
        {
            var span = DateTime.Now - dateTime;
            if (span.TotalSeconds < 60)
                return $"{(int)span.TotalSeconds} giây trước";
            if (span.TotalMinutes < 60)
                return $"{(int)span.TotalMinutes} phút trước";
            if (span.TotalHours < 24)
                return $"{(int)span.TotalHours} giờ trước";
            if (span.TotalDays < 30)
                return $"{(int)span.TotalDays} ngày trước";

            return dateTime.ToString("dd/MM/yyyy");
        }
    }
}

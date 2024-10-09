public class TimeService
{
    public string GetCurrentTimePeriod()
    {
        var currentTime = DateTime.Now;
        var hour = currentTime.Hour;

        if (hour >= 6 && hour < 12)
        {
            return $"зараз ранок {hour}";
        }
        else if (hour >= 12 && hour < 18)
        {
            return $"зараз день {hour}";
        }
        else if (hour >= 18 && hour < 24)
        {
            return $"зараз вечір {hour}";
        }
        else
        {
            return $"зараз ніч {hour}";
        }
    }
}

// See https://aka.ms/new-console-template for more information

namespace TimeTester
{
    internal class Program
    {
        public const string DayTimeConnectionString = "DayTime";
        public const string NightTimeConnectionString = "NightTime";
        public const string NightTimeStart = "22:00:00";
        public const string NightTimeEnd = "06:30:00";

        static void Main()
        {
            var nightTimeStart = TimeKeeper.ToTimeSpan(NightTimeStart);
            var nightTimeEnd = TimeKeeper.ToTimeSpan(NightTimeEnd);

            var connection = new ConnectionStringProvider(DayTimeConnectionString, NightTimeConnectionString, nightTimeStart, nightTimeEnd, DateTime.Now.TimeOfDay);

            var which = connection.GetConnectionString();

            Console.WriteLine($"Connection String is '{which}' at {DateTime.Now}");
        }
        
    }

    public class TimeKeeper
    {
        public static TimeSpan ToTimeSpan(string nightTimeStart)
        {
            string[] timeParts = nightTimeStart.Split(new[] {':'}, 3);
            return new TimeSpan(Convert.ToInt32(timeParts[0]),
                Convert.ToInt32(timeParts[1]),
                Convert.ToInt32(timeParts[2]));
        }
    }

    public class ConnectionStringProvider
    {
        public TimeSpan CurrentTime { get; }
        private readonly string _dayTimeConnectionString;
        private readonly string _nightTimeConnectionString;
        private readonly TimeSpan _nightTimeStart;
        private readonly TimeSpan _nightTimeEnd;
        private readonly TimeSpan _currentTime;

        public ConnectionStringProvider(string dayTimeConnectionString,
            string nightTimeConnectionString,
            TimeSpan nightTimeStart,
            TimeSpan nightTimeEnd,
            TimeSpan currentTime)
        {
            CurrentTime = currentTime;
            _dayTimeConnectionString = dayTimeConnectionString;
            _nightTimeConnectionString = nightTimeConnectionString;
            _nightTimeStart = nightTimeStart;
            _nightTimeEnd = nightTimeEnd;
            _currentTime = currentTime;
        }

        public string GetConnectionString()
        {
            return IsNightTime() ? _nightTimeConnectionString : _dayTimeConnectionString;
        }

        private bool IsNightTime()
        {
            var currentTime = _currentTime; // DateTime.Now.TimeOfDay;
            var dssNightTimeStart = _nightTimeStart;
            var dssNightTimeEnd = _nightTimeEnd;
            var isNightTime = ((currentTime.Hours > 12) && (currentTime.CompareTo(dssNightTimeStart) >= 0)) ||
                              ((currentTime.Hours < 12) && (currentTime.CompareTo(dssNightTimeEnd) <= 0));
            return isNightTime;
        }
    }
}
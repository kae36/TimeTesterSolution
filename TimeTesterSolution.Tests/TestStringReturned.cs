using TimeTester;

namespace TimeTesterSolution.Tests
{
    [Trait("Test","")]
    public class TestStringReturned
    {
        public readonly string DayTimeConnectionString = "DayTime";
        public readonly string NightTimeConnectionString = "NightTime";
        public readonly TimeSpan NightTimeStart = TimeKeeper.ToTimeSpan("22:00:00");
        public readonly TimeSpan NightTimeEnd = TimeKeeper.ToTimeSpan("06:30:00");

        [Theory]
        [InlineData("21:59:59", "DayTime")]
        [InlineData("22:00:00", "NightTime")]
        [InlineData("22:00:01", "NightTime")]
        [InlineData("23:00:00", "NightTime")]
        [InlineData("00:00:00", "NightTime")]
        [InlineData("01:00:00", "NightTime")]
        [InlineData("02:00:00", "NightTime")]
        [InlineData("03:00:00", "NightTime")]
        [InlineData("04:00:00", "NightTime")]
        [InlineData("05:00:00", "NightTime")]
        [InlineData("06:00:00", "NightTime")]
        [InlineData("06:29:59", "NightTime")]
        [InlineData("06:30:00", "NightTime")] // Should return DayTime?
        [InlineData("06:30:01", "DayTime")]
        [InlineData("07:00:00", "DayTime")]
        [InlineData("08:00:00", "DayTime")]
        [InlineData("09:00:00", "DayTime")]
        [InlineData("10:00:00", "DayTime")]
        [InlineData("11:00:00", "DayTime")]
        [InlineData("12:00:00", "DayTime")]
        [InlineData("13:00:00", "DayTime")]
        [InlineData("14:00:00", "DayTime")]
        [InlineData("15:00:00", "DayTime")]
        [InlineData("16:00:00", "DayTime")]
        [InlineData("17:00:00", "DayTime")]
        [InlineData("18:00:00", "DayTime")]
        [InlineData("19:00:00", "DayTime")]
        [InlineData("20:00:00", "DayTime")]
        [InlineData("21:00:00", "DayTime")]
        public void StringReturnedMatchesExpected(string time, string expected)
        {
            // Arrange
            var now = TimeKeeper.ToTimeSpan(time);
            var csp = new ConnectionStringProvider(DayTimeConnectionString, NightTimeConnectionString, NightTimeStart,
                NightTimeEnd, now);
            // Act
            var value = csp.GetConnectionString();
            // Assert
            Assert.Equal(value, expected);
        }

        [Fact]
        [Trait("Category", "Unit Test")]
        public void ThisTestShouldAlwaysFail()
        {
            var succeed = false;  // set to true to make this test succeed
            var time = "06:30:00";
            var expected = (succeed) ? "NightTime": "daytime";
            var now = TimeKeeper.ToTimeSpan(time);
            var csp = new ConnectionStringProvider(DayTimeConnectionString, NightTimeConnectionString, NightTimeStart,
                NightTimeEnd, now);
            // Act
            var value = csp.GetConnectionString();
            // Assert
            Assert.Equal(value, expected);
        }

        [Fact]
        [Trait("Category", "Unit Test")]
        public void ThisTestShouldAlwaysSucceed()
        {
            var time = "06:30:00";
            var expected = "NightTime";
            var now = TimeKeeper.ToTimeSpan(time);
            var csp = new ConnectionStringProvider(DayTimeConnectionString, NightTimeConnectionString, NightTimeStart,
                NightTimeEnd, now);
            // Act
            var value = csp.GetConnectionString();
            // Assert
            Assert.Equal(value, expected);
        }
    }
}

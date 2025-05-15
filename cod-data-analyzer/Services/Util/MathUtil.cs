namespace cod_data_analyzer.Services.Util
{
    public class MathUtil
    {
        public static float Average(float numerator, float demondenominator)
            => demondenominator == 0 ? 0 : numerator / (demondenominator == 0 ? 1 : demondenominator);
        public static int SecondsElapsedInt(DateTime startDate, DateTime endDate)
            => (int)SecondsElapsedLong(startDate, endDate);
        public static long SecondsElapsedLong(DateTime startDate, DateTime endDate)
           => (new DateTimeOffset(endDate).ToUnixTimeSeconds() - new DateTimeOffset(startDate).ToUnixTimeSeconds());
    }
}

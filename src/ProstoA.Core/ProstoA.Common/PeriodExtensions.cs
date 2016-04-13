namespace ProstoA {
    public static class PeriodExtensions {
        public static Period PreviousYear(this Period period, int count = 1) {
            return new Period(period.Start?.AddYears(-count), period.Expiration?.AddYears(-count));
        }
    }
}
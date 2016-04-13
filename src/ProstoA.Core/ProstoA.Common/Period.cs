using System;

namespace ProstoA {
    public struct Period {
        public Period(DateTimeOffset? start, DateTimeOffset? expiration) {
            Start = start;
            Expiration = expiration;
        }

        public static Period Empty = new Period();

        public DateTimeOffset? Start { get; }

        public DateTimeOffset? Expiration { get; }

        public string Display => "период с " + Start?.ToString("dd.MM.yy") + " по " + Expiration?.ToString("dd.MM.yy");

        public bool Contains(DateTimeOffset date) {
            return (Start == null || date >= Start) && (Expiration == null || date <= Expiration);
        }

        public static Period Pasre(string from = null, string to = null) {
            DateTimeOffset fromData;
            DateTimeOffset toData;

            return new Period(
                DateTimeOffset.TryParse(from, out fromData) ? (DateTimeOffset?)fromData : null,
                DateTimeOffset.TryParse(to, out toData) ? (DateTimeOffset?)toData : null
            );
        }
    }
}
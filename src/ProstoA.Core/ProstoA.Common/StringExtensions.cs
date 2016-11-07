using System.Linq;

namespace ProstoA {
    public static class StringExtensions {
        public static string ReplaceTokens(this string text, object tokens) {
            return tokens
                .ConvertToDictionary()
                .Aggregate(text, (current, token) => current.Replace($"{{{token.Key}}}", token.Value.ToString()));
        }
    }
}
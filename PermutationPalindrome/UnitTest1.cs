using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NUnit.Framework;
using Shouldly;

namespace Tests
{
    public class Tests
    {
        [TestCase("civic", true)]
        [TestCase("ivicc", true)]
        [TestCase("civil", false)]
        [TestCase("livci", false)]
        [TestCase("redder", true)]
        [TestCase("rreedd", true)]
        [TestCase("I did, did I?", true)]
        [TestCase("Don't nod.", true)]
        [TestCase("Red rum, sir, is murder", true)]
        [TestCase("Step on no pets", true)]
        [TestCase("Step on no pet", true)]
        [TestCase("Step on no dog", false)]
        [TestCase("ΝΙΨΟΝ ΑΝΟΜΗΜΑΤΑ ΜΗ ΜΟΝΑΝ ΟΨΙΝ", true)]
        [TestCase("ΝΙΨΟΝ ΑΝΟΜΗΜΑΤΑ ΜΗ ΜΝΑΝ ΟΨΙΝ", false)]
        public void IsAnyPermutationPalindrome(string input, bool expectedResult)
        {
            TestForPalindrome(input).ShouldBe(expectedResult);
            IsPalindrome(input).ShouldBe(expectedResult);
        }

        public bool IsPalindrome(string input)
        {
            var sanitized = Sanitize(input);

            var st = new HashSet<char>();

            foreach (var c in sanitized)
            {
                if (st.Contains(c)) st.Remove(c);
                else st.Add(c);
            }

            return st.Count <= 1;
        }

        private static bool TestForPalindrome(string input)
        {
            var sanitized = Sanitize(input);

            // How many unpaired letters do we have
            var letterCount = sanitized
                .GroupBy(x => x)
                .Select(x => x.Count())
                .Count(x => x % 2 == 1);

            // For an odd length palindrome, should only have 1 odd letter, otherwise should have 0
            return (sanitized.Length % 2 == 1) ? letterCount == 1 : letterCount == 0;
        }

        private static string Sanitize(string input)
        {
            return Regex.Replace(input, "[^\\p{L}]+", "").ToLowerInvariant();
        }
    }
}

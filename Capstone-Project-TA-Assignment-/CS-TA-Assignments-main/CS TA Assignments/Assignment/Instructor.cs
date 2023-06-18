using System;
using System.Collections.Generic;
using System.Text;

namespace cwu.cs.TaAssignments
{
    class Instructor
    {
        public string lastName = string.Empty;
        public string firstName = string.Empty;
        public string email = string.Empty;

        private Instructor() { }


        private static Dictionary<string, Instructor> cache;

        public static Instructor ByName(string fullName)
        {
            if (cache == null) InitCache();

            if (string.IsNullOrEmpty(fullName)) return new Instructor();
            if (!cache.ContainsKey(fullName)) return new Instructor();

            return cache[fullName];
        }

        private static void InitCache()
        {
            cache = new Dictionary<string, Instructor>();

            cache.Add("Salter,Rosemary", new Instructor()
            {
                firstName = "Rosemary",
                lastName = "Dr. Salter",
                email = "Rosemary.Salter@cwu.edu"
            });

            cache.Add("Abdul-Wahid,Sarah", new Instructor()
            {
                firstName = "Sarah",
                lastName = "Ms. Abdul-Wahid",
                email = "Sarah.Abdul-Wahid@cwu.edu"
            });

            cache.Add("Harrison,Tatiana F", new Instructor()
            {
                firstName = "Tatiana",
                lastName = "Ms. Harrison",
                email = "Tatiana.Harrison@cwu.edu"
            });

            cache.Add("Hueffed,Joseph Dominic", new Instructor()
            {
                firstName = "Joseph",
                lastName = "Mr. Hueffed",
                email = "Joseph.Hueffed@cwu.edu"
            });

            cache.Add("Dovhalets,Dmytro", new Instructor()
            {
                firstName = "Dmytro",
                lastName = "Mr. Dovhalets",
                email = "Dmytro.Dovhalets@cwu.edu"
            });

            cache.Add("Leitert,Arne", new Instructor()
            {
                firstName = "Arne",
                lastName = "Dr. Leitert",
                email = "arne.leitert@cwu.edu"
            });

            cache.Add("McKinley III,Douglas Edouard", new Instructor()
            {
                firstName = "Douglas",
                lastName = "Mr. McKinley",
                email = "douglas.mckinley2@cwu.edu"
            });

            cache.Add("White,Nathan Andrew", new Instructor()
            {
                firstName = "Nathan",
                lastName = "Dr. White",
                email = "nathan.white@cwu.edu"
            });

            cache.Add("Andonie,Razvan", new Instructor()
            {
                firstName = "Razvan",
                lastName = "Dr. Andonie",
                email = "Razvan.Andonie@cwu.edu"
            });

            cache.Add("Yepdjio Nkouanga,Hermann", new Instructor()
            {
                firstName = "Hermann",
                lastName = "Mr. Yepdjio Nkouanga",
                email = "Hermann.YepdjioNkouanga@cwu.edu"
            });

            cache.Add("Vajda,Szilard", new Instructor()
            {
                firstName = "Szilard",
                lastName = "Dr. Vajda",
                email = "Szilard.Vajda@cwu.edu"
            });

            cache.Add("Davendra,Donald David", new Instructor()
            {
                firstName = "Donald",
                lastName = "Dr. Davendra",
                email = "Donald.Davendra@cwu.edu"
            });

            cache.Add("Kovalerchuk,Boris", new Instructor()
            {
                firstName = "Boris",
                lastName = "Dr. Kovalerchuk",
                email = "Boris.Kovalerchuk@cwu.edu"
            });
        }
    }
}

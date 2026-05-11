using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Lab4.Models
{
    internal class Performer
    {
        private string _firstName;
        private string _lastName;
        private static readonly Regex NameRegex = new Regex(@"^[A-ZА-ЯҐЄІЇ][a-zа-яґєії']{1,49}$");
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (!NameRegex.IsMatch(value))
                    throw new ArgumentException("Ім'я повинно починатися з великої літери та містити від 2 до 50 символів.");
                _firstName = value;
            }
        }
        public string LastName
        {
            get => _lastName;
            set
            {
                if (!NameRegex.IsMatch(value))
                    throw new ArgumentException("Прізвище повинно починатися з великої літери та містить від 2 до 50 символів.");
                _lastName = value;
            }
        }
    }
}

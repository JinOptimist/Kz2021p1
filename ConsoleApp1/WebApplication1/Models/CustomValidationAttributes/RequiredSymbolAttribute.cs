using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.CustomValidationAttributes
{
    public class RequiredSymbolAttribute : ValidationAttribute
    {
        private string _requiredSymbol;

        public RequiredSymbolAttribute(string requiredSymbol)
        {
            _requiredSymbol = requiredSymbol;
        }

        public override string FormatErrorMessage(string name)
        {
            return ErrorMessage ?? $"Обязательно используй символ '{_requiredSymbol}'";
            //return ErrorMessage != null 
            //    ? ErrorMessage
            //    : "Обязательно введи пробел";
        }

        public override bool IsValid(object value)
        {
            var str = value as string;
            if (str == null)
            {
                return false;
            }
            
            return str.IndexOf(_requiredSymbol) > -1;
        }
    }
}

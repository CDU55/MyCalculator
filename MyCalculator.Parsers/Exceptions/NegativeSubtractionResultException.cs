﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MyCalculator.Parsers.Exceptions
{
    public class NegativeSubtractionResultException : Exception
    {
        public NegativeSubtractionResultException(string message) : base(message)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace si2.bll.Helpers.MultiLingualSupport
{
    public class UndefinedValueForLanguageException: Exception
    {
        public UndefinedValueForLanguageException(string message): base(message)
        {

        }
    }
}

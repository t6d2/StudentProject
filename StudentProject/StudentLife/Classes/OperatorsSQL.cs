using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentLife.Classes
{
    public class OperatorsSQL
    {
        public string MoreLessOperator { get; }
        public string NullOperator { get; }
        public string OrAndOperator { get; }

        public OperatorsSQL(string moreLessOperator, string nullOperator, string orAndOperator)
        {
            MoreLessOperator = moreLessOperator;
            NullOperator = nullOperator;
            OrAndOperator = orAndOperator;
        }
    }
}

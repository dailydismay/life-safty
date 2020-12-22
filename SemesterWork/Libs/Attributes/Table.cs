using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SemesterWork.Libs.Attributes
{
    public class Table : Attribute
    {
        public string TableName { get; set; }

        public Table(string tableName)
        {
            TableName = tableName;
        }
    }
}

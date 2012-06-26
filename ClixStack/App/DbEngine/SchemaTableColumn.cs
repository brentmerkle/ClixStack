using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppFramework
{
    public class SchemaTableColumn
    {
        public SchemaActionType SchemaActionType { get; set; }
        public int SchemaTableColumnOrder { get; set; }
        public string SchemaTableColumnName { get; set; }
        public bool IsRequired { get; set; }
        public int Length { get; set; }
        public int Precision { get; set; }
        public int Scale { get; set; }
        public SchemaTableColumnType SchemaTableColumnType { get; set; }
    }
}

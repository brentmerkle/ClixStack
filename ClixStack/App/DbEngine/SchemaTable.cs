using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppFramework
{
    class SchemaTable
    {
        public SchemaTable()
        {
            SchemaTableColumns = new List<SchemaTableColumn>();
        }
        public SchemaActionType SchemaActionType { get; set; }
        public string SchemaTableName { get; set; }
        public List<SchemaTableColumn> SchemaTableColumns { get; set; }
        public bool IsCreated { get; set; }
    }
}

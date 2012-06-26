using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace AppFramework
{
    public class DbEngine
    {
        // Database 3.0
        public DbEngine(string ConnectionString)
        {
            this._ConnectionString = ConnectionString;
            _Parameters = new ParameterClass(this);
            _Data = new DataBindingClass(this);
            _Schema = new SchemaDesignClass(this);
            _Parameters.Add.ReturnParameter(AppSettings.Exceptions.RETURN_CODE);
        }

        #region | Properties |

        private SqlTransaction _Transaction;
        private SqlConnection _Connection;
        private string _ConnectionString;
        private SqlCommand _Command = new SqlCommand();
        private SqlDataReader _Reader;
        private string _Procedure;
        private bool _IsProcedure = true;
        //private ListItemCollection _ListItems;
        private DataTable _DataTable;

        public string Procedure
        {
            get { return this._Procedure; }
            set { this._Procedure = value; }
        }

        #endregion

        #region | Parameters |

        private ParameterClass _Parameters;
        public ParameterClass Parameters
        {
            get { return _Parameters; }
            set { _Parameters = value; }
        }
        public class ParameterClass
        {
            public ParameterClass(DbEngine Parent)
            {
                this.Parent = Parent;
                this._Add = new AddClass(Parent);
                this._Set = new SetClass(Parent);
            }
            private DbEngine Parent;

            #region | Add |

            private AddClass _Add;

            public AddClass Add
            {
                get { return _Add; }
                set { _Add = value; }
            }

            public class AddClass
            {
                public AddClass(DbEngine Parent)
                { this.Parent = Parent; }

                private DbEngine Parent;

                public void Varchar(string ParameterName, string ParameterValue)
                {
                    try
                    { Parent._Command.Parameters.Add("@" + ParameterName, SqlDbType.VarChar, 8000).Value = ParameterValue; }
                    catch (Exception ex)
                    { throw ex; }
                }

                public void Text(string ParameterName, string ParameterValue)
                {
                    try
                    { Parent._Command.Parameters.Add("@" + ParameterName, SqlDbType.Text).Value = ParameterValue; }
                    catch (Exception ex)
                    { throw ex; }
                }

                public void Int(string ParameterName, int ParameterValue)
                {
                    try
                    { Parent._Command.Parameters.Add("@" + ParameterName, SqlDbType.Int).Value = ParameterValue; }
                    catch (Exception ex)
                    { throw ex; }
                }

                public void Float(string ParameterName, Double ParameterValue)
                {
                    try
                    { Parent._Command.Parameters.Add("@" + ParameterName, SqlDbType.Float).Value = ParameterValue; }
                    catch (Exception ex)
                    { throw ex; }
                }

                public void BigInt(string ParameterName, Int32 ParameterValue)
                {
                    try
                    { Parent._Command.Parameters.Add("@" + ParameterName, SqlDbType.Int).Value = ParameterValue; }
                    catch (Exception ex)
                    { throw ex; }
                }

                public void TinyInt(string ParameterName, int ParameterValue)
                {
                    try
                    { Parent._Command.Parameters.Add("@" + ParameterName, SqlDbType.TinyInt).Value = ParameterValue; }
                    catch (Exception ex)
                    { throw ex; }
                }

                public void DateTime(string ParameterName, DateTime ParameterValue)
                {
                    try
                    { Parent._Command.Parameters.Add("@" + ParameterName, SqlDbType.DateTime).Value = ParameterValue; }
                    catch (Exception ex)
                    { throw ex; }
                }

                public void Bit(string ParameterName, bool ParameterValue)
                {
                    try
                    { Parent._Command.Parameters.Add("@" + ParameterName, SqlDbType.Bit).Value = ParameterValue; }
                    catch (Exception ex)
                    { throw ex; }
                }

                public void VarcharByLength(string ParameterName, string ParameterValue, int VarcharLength)
                {
                    try
                    { Parent._Command.Parameters.Add("@" + ParameterName, SqlDbType.VarChar, VarcharLength).Value = ParameterValue; }
                    catch (Exception ex)
                    { throw ex; }
                }

                public void CharByLength(string ParameterName, string ParameterValue, int CharLength)
                {
                    try
                    { Parent._Command.Parameters.Add("@" + ParameterName, SqlDbType.Char, CharLength).Value = ParameterValue; }
                    catch (Exception ex)
                    { throw ex; }
                }

                public void Money(string ParameterName, double ParameterValue)
                {
                    try
                    { Parent._Command.Parameters.Add("@" + ParameterName, SqlDbType.Money).Value = ParameterValue; }
                    catch (Exception ex)
                    { throw ex; }
                }

                public void Decimal(string ParameterName, double ParameterValue, byte Scale, byte Precision)
                {
                    try
                    {
                        SqlParameter Decimal_Parameter = new SqlParameter();
                        Decimal_Parameter.ParameterName = "@" + ParameterName;
                        Decimal_Parameter.SqlDbType = SqlDbType.Decimal;
                        Decimal_Parameter.Direction = ParameterDirection.Input;
                        Decimal_Parameter.Precision = Precision;
                        Decimal_Parameter.Scale = Scale;
                        Decimal_Parameter.Value = ParameterValue;
                        Parent._Command.Parameters.Add(Decimal_Parameter);

                    }
                    catch (Exception ex)
                    { throw ex; }
                }

                public void Uniqueidentifier(string ParameterName, Guid ParameterValue)
                {
                    try
                    { Parent._Command.Parameters.Add("@" + ParameterName, SqlDbType.UniqueIdentifier).Value = ParameterValue; }
                    catch (Exception ex)
                    { throw ex; }
                }

                public void ReturnParameter(string ParameterName)
                {
                    try
                    {
                        SqlParameter ReturnParameter = new SqlParameter("@" + ParameterName, DbType.Int32);
                        ReturnParameter.Direction = ParameterDirection.ReturnValue;
                        Parent._Command.Parameters.Add(ReturnParameter);
                    }
                    catch (Exception ex)
                    { throw ex; }
                }

            }

            #endregion

            #region | Set |

            private SetClass _Set;

            public SetClass Set
            {
                get { return _Set; }
                set { _Set = value; }
            }

            public class SetClass
            {
                public SetClass(DbEngine Parent)
                { this.Parent = Parent; }

                private DbEngine Parent;

                public void SetVarchar(string ParameterName, string ParameterValue)
                {
                    try
                    { Parent._Command.Parameters["@" + ParameterName].Value = ParameterValue; }
                    catch (Exception ex)
                    { throw ex; }
                }

                public void VarcharResize(string ParameterName, string ParameterValue, Int32 NewVarcharLength)
                {
                    try
                    {
                        foreach (SqlParameter pram in Parent._Command.Parameters)
                        {
                            if (pram.ParameterName == "@" + ParameterName)
                            {
                                pram.Value = ParameterValue;
                                return;
                            }
                        }
                        Parent._Command.Parameters.Add("@" + ParameterName, SqlDbType.VarChar, 8000).Value = ParameterValue;
                    }
                    catch (Exception ex)
                    { throw ex; }
                }

                public void Text(string ParameterName, string ParameterValue)
                {
                    try
                    {
                        Parent._Command.Parameters["@" + ParameterName].Value = ParameterValue;
                    }
                    catch (Exception ex)
                    { throw ex; }
                }

                public void Int(string ParameterName, int ParameterValue)
                {
                    try
                    {
                        Parent._Command.Parameters["@" + ParameterName].Value = ParameterValue;
                    }
                    catch (Exception ex)
                    { throw ex; }
                }

                public void Float(string ParameterName, Double ParameterValue)
                {
                    try
                    { Parent._Command.Parameters["@" + ParameterName].Value = ParameterValue; }
                    catch (Exception ex)
                    { throw ex; }
                }

                public void BigInt(string ParameterName, int ParameterValue)
                {
                    try
                    { Parent._Command.Parameters["@" + ParameterName].Value = ParameterValue; }
                    catch (Exception ex)
                    { throw ex; }
                }

                public void TinyInt(string ParameterName, int ParameterValue)
                {
                    try
                    { Parent._Command.Parameters["@" + ParameterName].Value = ParameterValue; }
                    catch (Exception ex)
                    { throw ex; }
                }

                public void DateTime(string ParameterName, DateTime ParameterValue)
                {
                    try
                    { Parent._Command.Parameters["@" + ParameterName].Value = ParameterValue; }
                    catch (Exception ex)
                    { throw ex; }
                }

                public void ParameterBit(string ParameterName, bool ParameterValue)
                {
                    try
                    { Parent._Command.Parameters["@" + ParameterName].Value = ParameterValue; }
                    catch (Exception ex)
                    { throw ex; }
                }

                public void Char(string ParameterName, string ParameterValue)
                {
                    try
                    { Parent._Command.Parameters["@" + ParameterName].Value = ParameterValue; }
                    catch (Exception ex)
                    { throw ex; }
                }

                public void Money(string ParameterName, double ParameterValue)
                {
                    try
                    { Parent._Command.Parameters["@" + ParameterName].Value = ParameterValue; }
                    catch (Exception ex)
                    { throw ex; }
                }

                public void Decimal(string ParameterName, double ParameterValue)
                {
                    try
                    { Parent._Command.Parameters["@" + ParameterName].Value = ParameterValue; }
                    catch (Exception ex)
                    { throw ex; }
                }

                public void Uniqueidentifier(string ParameterName, Guid ParameterValue)
                {
                    try
                    { Parent._Command.Parameters["@" + ParameterName].Value = ParameterValue; }
                    catch (Exception ex)
                    { throw ex; }
                }

            }

            #endregion

            public void ClearAll()
            {
                try
                { Parent._Command.Parameters.Clear(); }
                catch (Exception ex)
                { throw ex; }
            }

            public int GetReturnParameter(string ParameterName)
            {
                try
                { return Parent._Command.Parameters["@" + ParameterName].Value.ToString().x_ToInt(); }
                catch (Exception ex)
                { throw ex; }
            }

        }

        #endregion

        #region | Transactions |

        private DataBindingClass _Data;
        public DataBindingClass Data
        {
            get { return _Data; }
            set { _Data = value; }
        }

        public class DataBindingClass
        {
            public DataBindingClass(DbEngine Parent)
            {
                this.Parent = Parent;
                this._List = new ListItemsClass(Parent);
                this._Table = new DataTableClass(Parent);
            }
            private DbEngine Parent;

            #region | ListItems |

            private ListItemsClass _List;
            public ListItemsClass List
            {
                get { return _List; }
                set { _List = value; }
            }

            public class ListItemsClass
            {
                public ListItemsClass(DbEngine Parent)
                {
                    this.Parent = Parent;
                }

                private DbEngine Parent;

                public void Add(string Text, string Value)
                {
                    if (Parent._ListItems == null)
                        Parent._ListItems = new ListItemCollection();
                    ListItem Item = new ListItem();
                    Item.Text = Text;
                    Item.Value = Value;
                    Parent._ListItems.Add(Item);
                }

                public ListItemCollection Query()
                {
                    SqlDataReader ReadList = default(SqlDataReader);
                    ListItem Item;
                    if (Parent._ListItems == null)
                        Parent._ListItems = new ListItemCollection();
                    using (Parent._Connection = new SqlConnection())
                    {
                        Parent._Connection.ConnectionString = Parent._ConnectionString;
                        Parent._Connection.Open();
                        Parent._Command.Connection = Parent._Connection;
                        Parent._Command.CommandText = Parent._Procedure;
                        Parent._Command.Connection = Parent._Connection;
                        Parent._Command.CommandType = CommandType.StoredProcedure;
                        ReadList = Parent._Command.ExecuteReader();
                        while (ReadList.Read())
                        {
                            Item = new ListItem();
                            Item.Text = ReadList["text"].ToString();
                            Item.Value = ReadList["value"].ToString();
                            Parent._ListItems.Add(Item);
                        }
                        ReadList.Close();
                        if (!ReadList.IsClosed) ReadList.Close();
                    }
                    return Parent._ListItems;
                }

            }

            #endregion

            #region | DataTables |

            private DataTableClass _Table;
            public DataTableClass Table
            {
                get { return _Table; }
                set { _Table = value; }
            }

            public class DataTableClass
            {
                public DataTableClass(DbEngine Parent) { this.Parent = Parent; }

                private DbEngine Parent;

                public DataTable Query()
                {
                    using (Parent._Connection = new SqlConnection())
                    {
                        Parent._Connection.ConnectionString = Parent._ConnectionString;
                        Parent._Connection.Open();
                        Parent._Command.Connection = Parent._Connection;
                        Parent._Command.Connection = Parent._Connection;
                        Parent._Command.CommandText = Parent._Procedure;
                        Parent._Command.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter Data = new SqlDataAdapter(Parent._Command);
                        Parent._DataTable = new DataTable();
                        Data.Fill(Parent._DataTable);
                    }
                    return Parent._DataTable;
                }

            }

            #endregion

            #region | DataReaders |

            public IEnumerable<IDataRecord> ExecuteReader()
            {
                using (Parent._Connection = new SqlConnection())
                {
                    Parent._Connection.ConnectionString = Parent._ConnectionString;
                    Parent._Connection.Open();
                    Parent._Command.Connection = Parent._Connection;
                    Parent._Command.CommandText = Parent._Procedure;
                    Parent._Command.CommandType = CommandType.StoredProcedure;
                    using (IDataReader dr = Parent._Command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            yield return (IDataRecord)dr;
                        }
                    }
                }
            }

            #endregion

            #region | Procedures |

            public int ExecuteProcedure()
            {
                int RowCount = 0;
                using (Parent._Connection = new SqlConnection())
                {
                    Parent._Connection.ConnectionString = Parent._ConnectionString;
                    Parent._Connection.Open();
                    Parent._Transaction = Parent._Connection.BeginTransaction();
                    Parent._Command.Connection = Parent._Connection;
                    Parent._Command.Transaction = Parent._Transaction;
                    Parent._Command.CommandText = Parent._Procedure;
                    Parent._Command.CommandType = CommandType.StoredProcedure;
                    RowCount = Parent._Command.ExecuteNonQuery();
                    Parent._Transaction.Commit();
                }
                return RowCount;
            }

            #endregion
        }

        private SchemaDesignClass _Schema;
        public SchemaDesignClass Schema
        {
            get { return _Schema; }
            set { _Schema = value; }
        }

        public class SchemaDesignClass
        {
            public SchemaDesignClass(DbEngine Parent)
            {
                this.Parent = Parent;
            }
            private DbEngine Parent;

            public void AddTable(SchemaTable SchemaTable)
            {
                SchemaTable.SchemaActionType = SchemaActionType.Create;
                SchemaTable.SchemaTableColumns.Select(s => { s.SchemaActionType = SchemaActionType.Create; return s; }).ToList();
            }

            public SchemaTable GetTable(SchemaTable SchemaTable)
            {
                return null;
            }

            private void TableTransactionManager(SchemaTable SchemaTable)
            {

            }

            private List<SchemaTable> GetTableSchema()
            {
                List<SchemaTable> DbSchema = new List<SchemaTable>();
                SchemaTable SchemaTable;
                SchemaTableColumn SchemaTableColumn;
                Parent._Connection.ConnectionString = Parent._ConnectionString;
                Parent._Connection.Open();
                Parent._Command.Connection = Parent._Connection;
                Parent._Command.CommandText = "select [TABLE_NAME] as [Table],[COLUMN_NAME] as [Column], CASE WHEN [IS_NULLABLE] = 'YES' THEN 1 ELSE 0 END as [Required], ISNULL(CHARACTER_MAXIMUM_LENGTH,0) as [Length],ISNULL(NUMERIC_PRECISION,0) as [Precision],ISNULL(NUMERIC_SCALE,0) as [Scale], [DATA_TYPE] as [Type], [ORDINAL_POSITION] as [Position] from information_schema.columns where substring(table_name,1,3) = 'tbl' order by table_name, ordinal_position";
                Parent._Command.CommandType = CommandType.Text;
                using (IDataReader dr = Parent._Command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        if (SchemaTable != null)
                        {
                            if (dr.GetString(dr.GetOrdinal("Table")) == SchemaTable.SchemaTableName)
                            {
                                // Add Column
                                SchemaTableColumn = new SchemaTableColumn();
                                SchemaTableColumn.IsRequired = dr.GetBoolean(dr.GetOrdinal("Required"));
                                SchemaTableColumn.Length = dr.GetInt32(dr.GetOrdinal("Length"));
                                SchemaTableColumn.Precision = dr.GetInt32(dr.GetOrdinal("Precision"));
                                SchemaTableColumn.Scale = dr.GetInt32(dr.GetOrdinal("Scale"));
                                SchemaTableColumn.SchemaActionType = SchemaActionType.None;
                                SchemaTableColumn.SchemaTableColumnName = dr.GetString(dr.GetOrdinal("Column"));
                                SchemaTableColumn.SchemaTableColumnOrder = dr.GetInt32(dr.GetOrdinal("Position"));
                                if (dr.GetString(dr.GetOrdinal("Type")) == "int")
                                {
                                    if (dr.GetInt32(dr.GetOrdinal("Position")) == 1)
                                    {
                                        SchemaTableColumn.SchemaTableColumnType = SchemaTableColumnType.SequentialIdentity;
                                    }
                                    else
                                    {
                                        SchemaTableColumn.SchemaTableColumnType = SchemaTableColumnType.Integer;
                                    }
                                }
                                else if (dr.GetString(dr.GetOrdinal("Type")) == "varchar")
                                {
                                    SchemaTableColumn.SchemaTableColumnType = SchemaTableColumnType.VarChar;
                                }
                                else if (dr.GetString(dr.GetOrdinal("Type")) == "uniqueidentifier")
                                {
                                    SchemaTableColumn.SchemaTableColumnType = SchemaTableColumnType.UniqueIdentifier;
                                }
                                else if (dr.GetString(dr.GetOrdinal("Type")) == "bit")
                                {
                                    SchemaTableColumn.SchemaTableColumnType = SchemaTableColumnType.Bit;
                                }
                                else if (dr.GetString(dr.GetOrdinal("Type")) == "decimal")
                                {
                                    SchemaTableColumn.SchemaTableColumnType = SchemaTableColumnType.Decimal;
                                }
                                else if (dr.GetString(dr.GetOrdinal("Type")) == "varbinary")
                                {
                                    SchemaTableColumn.SchemaTableColumnType = SchemaTableColumnType.VarBinary;
                                }
                                else if (dr.GetString(dr.GetOrdinal("Type")) == "money")
                                {
                                    SchemaTableColumn.SchemaTableColumnType = SchemaTableColumnType.Money;
                                }
                                SchemaTable.SchemaTableColumns.Add(SchemaTableColumn);
                            }
                            else
                            {
                                // New Table
                            }
                        }
                        SchemaTable = new SchemaTable();
                        SchemaTable.IsCreated = true;
                        SchemaTable.SchemaActionType = SchemaActionType.None;
                        //SchemaTable.
                        yield return (IDataRecord)dr;
                    }
                }
            }
        }



        #endregion
    }
}
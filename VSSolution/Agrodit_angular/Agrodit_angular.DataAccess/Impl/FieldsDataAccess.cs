using Agrodit_angular.DataAccess.Interface;
using Agrodit_angular.Model;
using Agrodit_angular.Utility;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Agrodit_angular.DataAccess.Impl
{
    public class FieldsDataAccess : IFieldsDataAccess
    {
        private MSSqlDatabase MSSqlDatabase { get; set; }
        public FieldsDataAccess(MSSqlDatabase msSqlDatabase)
        {
            MSSqlDatabase = msSqlDatabase;
        }
        public List<FieldsModel> GetAllFields(int page=1,int itemsPerPage=100,List<OrderByModel> orderBy=null)
        {
            var ret = new List<FieldsModel>();
			int offset = (page - 1) * itemsPerPage;
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT  t.* FROM Fields t  Order by t.Id OFFSET @Offset ROWS FETCH NEXT @ItemsPerPage ROWS ONLY";
             if(orderBy!=null && orderBy.Count > 0)
            {
                cmd.CommandText = Helper.ConverOrderListToSQL(cmd.CommandText,orderBy);
            }
			cmd.Parameters.AddWithValue("@Offset", offset);
            cmd.Parameters.AddWithValue("@ItemsPerPage", itemsPerPage);
            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    var t = new FieldsModel()
                    {
                       Id= reader.GetValue<Int64>("Id"),
Name= reader.GetValue<String>("Name"),
CompanyId= reader.GetValue<Int64>("CompanyId"),
Geofence= reader.GetValue<String>("Geofence"),
ThresholdId= reader.IsDBNull(Helper.GetColumnOrder(reader,"ThresholdId")) ? (Int64?)null : reader.GetInt64("ThresholdId"),
                    };

                    ret.Add(t);
                }
            return ret;
        }

        public List<FieldsModel> SearchFields(string searchKey, int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy=null)
        {
            var ret = new List<FieldsModel>();
            int offset = (page - 1) * itemsPerPage;
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT  t.* FROM Fields t  WHERE 1=1 AND  t.Name LIKE CONCAT('%',@SearchKey,'%') OR t.Geofence LIKE CONCAT('%',@SearchKey,'%') Order by t.Id OFFSET @Offset ROWS FETCH NEXT @ItemsPerPage ROWS ONLY";
             if(orderBy!=null && orderBy.Count > 0)
            {
                cmd.CommandText = Helper.ConverOrderListToSQL(cmd.CommandText,orderBy);
            }
            cmd.Parameters.AddWithValue("@SearchKey", searchKey);
            cmd.Parameters.AddWithValue("@Offset", offset);
            cmd.Parameters.AddWithValue("@ItemsPerPage", itemsPerPage);
            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    var t = new FieldsModel()
                    {
                       Id= reader.GetValue<Int64>("Id"),
Name= reader.GetValue<String>("Name"),
CompanyId= reader.GetValue<Int64>("CompanyId"),
Geofence= reader.GetValue<String>("Geofence"),
ThresholdId= reader.IsDBNull(Helper.GetColumnOrder(reader,"ThresholdId")) ? (Int64?)null : reader.GetInt64("ThresholdId"),
                    };

                    ret.Add(t);
                }
            return ret;
        }

        public  int GetAllTotalRecordFields()
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT Count(*) as TotalRecord FROM Fields t";
            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                { 
                    return reader.GetInt32("TotalRecord");
                }
            return 0;
        }
        public int GetSearchTotalRecordFields(string searchKey)
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT Count(*) as TotalRecord FROM Fields t  WHERE 1=1 AND  t.Name LIKE CONCAT('%',@SearchKey,'%') OR t.Geofence LIKE CONCAT('%',@SearchKey,'%')";
            cmd.Parameters.AddWithValue("@SearchKey", searchKey);
            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                { 
                    return reader.GetInt32("TotalRecord");
                }
            return 0;
        }

		

        public FieldsModel GetFieldsByID(int id)
        {

            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT  t.* FROM Fields t  WHERE t.Id= @Id Order by t.Id OFFSET 0 ROWS FETCH NEXT 1 ROWS ONLY";
			cmd.Parameters.AddWithValue("@id", id);

            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    return new FieldsModel()
                    {
                        Id= reader.GetValue<Int64>("Id"),
Name= reader.GetValue<String>("Name"),
CompanyId= reader.GetValue<Int64>("CompanyId"),
Geofence= reader.GetValue<String>("Geofence"),
ThresholdId= reader.IsDBNull(Helper.GetColumnOrder(reader,"ThresholdId")) ? (Int64?)null : reader.GetInt64("ThresholdId"),
                    };
                }
            return null;
        }

         public List<FieldsModel> FilterFields(List<FilterModel> filterBy,string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy)
        {
            var ret = new List<FieldsModel>();
            int offset = (page - 1) * itemsPerPage;
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT  t.* FROM Fields t  {filterColumns} Order by t.Id OFFSET @Offset ROWS FETCH NEXT @ItemsPerPage ROWS ONLY";
            if(filterBy!=null && filterBy.Count > 0)
            {
                var whereClause = string.Empty;
                int paramCount=0;
                foreach (var r in filterBy)
                {
                    if (!string.IsNullOrEmpty(r.ColumnName))
                    {
                    paramCount++;
                        if (!string.IsNullOrEmpty(whereClause))
                        {
                            whereClause=whereClause + " " + andOr + " ";
                        }
                        whereClause = whereClause + "t." + r.ColumnName + " "+UtilityCommon.ConvertFilterToSQLString(r.ColumnCondition) + " @" + r.ColumnName+paramCount;
                        cmd.Parameters.AddWithValue("@"+ r.ColumnName+paramCount, r.ColumnValue);
                    }
                }
                whereClause = whereClause.Trim();
                cmd.CommandText = cmd.CommandText.Replace("{filterColumns}", "Where " + whereClause);
            }
            else
            {
                cmd.CommandText = cmd.CommandText.Replace("{filterColumns}", "");
            }
            if (orderBy != null && orderBy.Count > 0)
            {
                cmd.CommandText = Helper.ConverOrderListToSQL(cmd.CommandText, orderBy);
            }
            cmd.Parameters.AddWithValue("@Offset", offset);
            cmd.Parameters.AddWithValue("@ItemsPerPage", itemsPerPage);
            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                { 
                var t = new FieldsModel()
                    {
                       Id= reader.GetValue<Int64>("Id"),
Name= reader.GetValue<String>("Name"),
CompanyId= reader.GetValue<Int64>("CompanyId"),
Geofence= reader.GetValue<String>("Geofence"),
ThresholdId= reader.IsDBNull(Helper.GetColumnOrder(reader,"ThresholdId")) ? (Int64?)null : reader.GetInt64("ThresholdId"),
                    };

                    ret.Add(t);
                }
            return ret;
        }

       public int GetFilterTotalRecordFields(List<FilterModel> filterBy,string andOr)
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT Count(*) as TotalRecord FROM Fields t {filterColumns}";
            if (filterBy != null && filterBy.Count > 0)
            {
            int paramCount=0;
                var whereClause = string.Empty;
                foreach (var r in filterBy)
                {
                    if (!string.IsNullOrEmpty(r.ColumnName))
                    {paramCount++;
                        if (!string.IsNullOrEmpty(whereClause))
                        {
                            whereClause = whereClause + " " + andOr + " ";
                        }
                        whereClause = whereClause + "t." + r.ColumnName + " "+UtilityCommon.ConvertFilterToSQLString(r.ColumnCondition) + " @" + r.ColumnName+paramCount;
                        cmd.Parameters.AddWithValue("@" + r.ColumnName+paramCount, r.ColumnValue);
                    }
                }
                whereClause = whereClause.Trim();
                cmd.CommandText = cmd.CommandText.Replace("{filterColumns}", "Where " + whereClause);
            }
            else
            {
                cmd.CommandText = cmd.CommandText.Replace("{filterColumns}", "");
            }
            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    return reader.GetInt32("TotalRecord");
                }
            return 0;
        }

        public bool UpdateFields(FieldsModel model)
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            SqlTransaction transaction = this.MSSqlDatabase.Connection.BeginTransaction("");
            cmd.Transaction = transaction;
            cmd.CommandText = @"UPDATE Fields SET Id=@Id,Name=@Name,CompanyId=@CompanyId,Geofence=@Geofence,ThresholdId=@ThresholdId WHERE Id = @Id;";
            cmd.Parameters.AddWithValue("@Id", Helper.GetNullableParameter(model.Id));
cmd.Parameters.AddWithValue("@Name", Helper.GetNullableParameter(model.Name));
cmd.Parameters.AddWithValue("@CompanyId", Helper.GetNullableParameter(model.CompanyId));
cmd.Parameters.AddWithValue("@Geofence", Helper.GetNullableParameter(model.Geofence));
cmd.Parameters.AddWithValue("@ThresholdId", Helper.GetNullableParameter(model.ThresholdId));
            var recs = cmd.ExecuteNonQuery();
            if (recs > 0)
            {  
            transaction.Commit();
                return true;
            }
            return false;
        }

        public long AddFields(FieldsModel model)
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            SqlTransaction transaction = this.MSSqlDatabase.Connection.BeginTransaction("");
            cmd.Transaction = transaction;
            cmd.CommandText = @"INSERT INTO Fields (Id,Name,CompanyId,Geofence,ThresholdId) VALUES (@Id,@Name,@CompanyId,@Geofence,@ThresholdId);SELECT SCOPE_IDENTITY();";
            cmd.Parameters.AddWithValue("@Id", Helper.GetNullableParameter(model.Id));
cmd.Parameters.AddWithValue("@Name", Helper.GetNullableParameter(model.Name));
cmd.Parameters.AddWithValue("@CompanyId", Helper.GetNullableParameter(model.CompanyId));
cmd.Parameters.AddWithValue("@Geofence", Helper.GetNullableParameter(model.Geofence));
cmd.Parameters.AddWithValue("@ThresholdId", Helper.GetNullableParameter(model.ThresholdId));
              var recs = cmd.ExecuteScalar();
            if (recs!=null)
            {
                transaction.Commit();
                long.TryParse(recs.ToString(), out long result);
                return result>0? result : 1;
            }
            return -1;
          
        }

        public bool DeleteFields(int id)
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            SqlTransaction transaction = this.MSSqlDatabase.Connection.BeginTransaction("");
            cmd.Transaction = transaction;
            cmd.CommandText = @"DELETE FROM Fields Where Id=@Id";
			cmd.Parameters.AddWithValue("@id", id);
            var recs = cmd.ExecuteNonQuery();
            if (recs > 0)
            { 
                transaction.Commit();
                return true;
            }
            return false;
        }
        public bool DeleteMultipleFields(List<DeleteMultipleModel> deleteParam,string andOr)
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            SqlTransaction transaction = this.MSSqlDatabase.Connection.BeginTransaction("");
            cmd.Transaction = transaction;
            cmd.CommandText = @"DELETE FROM Fields Where";
            int count = 0;
            foreach (var r in deleteParam)
            {
                if (count == 0)
                {
                    cmd.CommandText = cmd.CommandText + " " + r.ColumnName + "=@" + r.ColumnName;
                }
                else
                {
                    cmd.CommandText = cmd.CommandText + " "+andOr+" " + r.ColumnName + "=@" + r.ColumnName;
                }
                cmd.Parameters.AddWithValue("@" + r.ColumnName, r.ColumnValue);
                count++;
            }

            var recs = cmd.ExecuteNonQuery();
            if (recs > 0)
            {
                transaction.Commit();
                return true;
            }
            return false;
        }
        
    }
}


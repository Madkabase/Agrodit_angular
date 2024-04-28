using Agrodit_angular.DataAccess.Interface;
using Agrodit_angular.Model;
using Agrodit_angular.Utility;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Agrodit_angular.DataAccess.Impl
{
    public class Spatial_Ref_SysDataAccess : ISpatial_Ref_SysDataAccess
    {
        private MSSqlDatabase MSSqlDatabase { get; set; }
        public Spatial_Ref_SysDataAccess(MSSqlDatabase msSqlDatabase)
        {
            MSSqlDatabase = msSqlDatabase;
        }
        public List<Spatial_Ref_SysModel> GetAllSpatial_Ref_Sys(int page=1,int itemsPerPage=100,List<OrderByModel> orderBy=null)
        {
            var ret = new List<Spatial_Ref_SysModel>();
			int offset = (page - 1) * itemsPerPage;
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT  t.* FROM spatial_ref_sys t  Order by t.srid OFFSET @Offset ROWS FETCH NEXT @ItemsPerPage ROWS ONLY";
             if(orderBy!=null && orderBy.Count > 0)
            {
                cmd.CommandText = Helper.ConverOrderListToSQL(cmd.CommandText,orderBy);
            }
			cmd.Parameters.AddWithValue("@Offset", offset);
            cmd.Parameters.AddWithValue("@ItemsPerPage", itemsPerPage);
            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    var t = new Spatial_Ref_SysModel()
                    {
                       srid= reader.GetValue<Int64>("srid"),
auth_name= reader.GetValue<String>("auth_name"),
auth_srid= reader.IsDBNull(Helper.GetColumnOrder(reader,"auth_srid")) ? (Int64?)null : reader.GetInt64("auth_srid"),
srtext= reader.GetValue<String>("srtext"),
proj4text= reader.GetValue<String>("proj4text"),
                    };

                    ret.Add(t);
                }
            return ret;
        }

        public List<Spatial_Ref_SysModel> SearchSpatial_Ref_Sys(string searchKey, int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy=null)
        {
            var ret = new List<Spatial_Ref_SysModel>();
            int offset = (page - 1) * itemsPerPage;
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT  t.* FROM spatial_ref_sys t  WHERE 1=1 AND  t.auth_name LIKE CONCAT('%',@SearchKey,'%') OR t.srtext LIKE CONCAT('%',@SearchKey,'%') OR t.proj4text LIKE CONCAT('%',@SearchKey,'%') Order by t.srid OFFSET @Offset ROWS FETCH NEXT @ItemsPerPage ROWS ONLY";
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
                    var t = new Spatial_Ref_SysModel()
                    {
                       srid= reader.GetValue<Int64>("srid"),
auth_name= reader.GetValue<String>("auth_name"),
auth_srid= reader.IsDBNull(Helper.GetColumnOrder(reader,"auth_srid")) ? (Int64?)null : reader.GetInt64("auth_srid"),
srtext= reader.GetValue<String>("srtext"),
proj4text= reader.GetValue<String>("proj4text"),
                    };

                    ret.Add(t);
                }
            return ret;
        }

        public  int GetAllTotalRecordSpatial_Ref_Sys()
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT Count(*) as TotalRecord FROM spatial_ref_sys t";
            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                { 
                    return reader.GetInt32("TotalRecord");
                }
            return 0;
        }
        public int GetSearchTotalRecordSpatial_Ref_Sys(string searchKey)
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT Count(*) as TotalRecord FROM spatial_ref_sys t  WHERE 1=1 AND  t.auth_name LIKE CONCAT('%',@SearchKey,'%') OR t.srtext LIKE CONCAT('%',@SearchKey,'%') OR t.proj4text LIKE CONCAT('%',@SearchKey,'%')";
            cmd.Parameters.AddWithValue("@SearchKey", searchKey);
            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                { 
                    return reader.GetInt32("TotalRecord");
                }
            return 0;
        }

		

        public Spatial_Ref_SysModel GetSpatial_Ref_SysByID(int id)
        {

            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT  t.* FROM spatial_ref_sys t  WHERE t.srid= @srid Order by t.srid OFFSET 0 ROWS FETCH NEXT 1 ROWS ONLY";
			cmd.Parameters.AddWithValue("@id", id);

            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    return new Spatial_Ref_SysModel()
                    {
                        srid= reader.GetValue<Int64>("srid"),
auth_name= reader.GetValue<String>("auth_name"),
auth_srid= reader.IsDBNull(Helper.GetColumnOrder(reader,"auth_srid")) ? (Int64?)null : reader.GetInt64("auth_srid"),
srtext= reader.GetValue<String>("srtext"),
proj4text= reader.GetValue<String>("proj4text"),
                    };
                }
            return null;
        }

         public List<Spatial_Ref_SysModel> FilterSpatial_Ref_Sys(List<FilterModel> filterBy,string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy)
        {
            var ret = new List<Spatial_Ref_SysModel>();
            int offset = (page - 1) * itemsPerPage;
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT  t.* FROM spatial_ref_sys t  {filterColumns} Order by t.srid OFFSET @Offset ROWS FETCH NEXT @ItemsPerPage ROWS ONLY";
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
                var t = new Spatial_Ref_SysModel()
                    {
                       srid= reader.GetValue<Int64>("srid"),
auth_name= reader.GetValue<String>("auth_name"),
auth_srid= reader.IsDBNull(Helper.GetColumnOrder(reader,"auth_srid")) ? (Int64?)null : reader.GetInt64("auth_srid"),
srtext= reader.GetValue<String>("srtext"),
proj4text= reader.GetValue<String>("proj4text"),
                    };

                    ret.Add(t);
                }
            return ret;
        }

       public int GetFilterTotalRecordSpatial_Ref_Sys(List<FilterModel> filterBy,string andOr)
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT Count(*) as TotalRecord FROM spatial_ref_sys t {filterColumns}";
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

        public bool UpdateSpatial_Ref_Sys(Spatial_Ref_SysModel model)
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            SqlTransaction transaction = this.MSSqlDatabase.Connection.BeginTransaction("");
            cmd.Transaction = transaction;
            cmd.CommandText = @"UPDATE spatial_ref_sys SET srid=@srid,auth_name=@auth_name,auth_srid=@auth_srid,srtext=@srtext,proj4text=@proj4text WHERE srid = @srid;";
            cmd.Parameters.AddWithValue("@srid", Helper.GetNullableParameter(model.srid));
cmd.Parameters.AddWithValue("@auth_name", Helper.GetNullableParameter(model.auth_name));
cmd.Parameters.AddWithValue("@auth_srid", Helper.GetNullableParameter(model.auth_srid));
cmd.Parameters.AddWithValue("@srtext", Helper.GetNullableParameter(model.srtext));
cmd.Parameters.AddWithValue("@proj4text", Helper.GetNullableParameter(model.proj4text));
            var recs = cmd.ExecuteNonQuery();
            if (recs > 0)
            {  
            transaction.Commit();
                return true;
            }
            return false;
        }

        public long AddSpatial_Ref_Sys(Spatial_Ref_SysModel model)
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            SqlTransaction transaction = this.MSSqlDatabase.Connection.BeginTransaction("");
            cmd.Transaction = transaction;
            cmd.CommandText = @"INSERT INTO spatial_ref_sys (srid,auth_name,auth_srid,srtext,proj4text) VALUES (@srid,@auth_name,@auth_srid,@srtext,@proj4text);SELECT SCOPE_IDENTITY();";
            cmd.Parameters.AddWithValue("@srid", Helper.GetNullableParameter(model.srid));
cmd.Parameters.AddWithValue("@auth_name", Helper.GetNullableParameter(model.auth_name));
cmd.Parameters.AddWithValue("@auth_srid", Helper.GetNullableParameter(model.auth_srid));
cmd.Parameters.AddWithValue("@srtext", Helper.GetNullableParameter(model.srtext));
cmd.Parameters.AddWithValue("@proj4text", Helper.GetNullableParameter(model.proj4text));
              var recs = cmd.ExecuteScalar();
            if (recs!=null)
            {
                transaction.Commit();
                long.TryParse(recs.ToString(), out long result);
                return result>0? result : 1;
            }
            return -1;
          
        }

        public bool DeleteSpatial_Ref_Sys(int id)
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            SqlTransaction transaction = this.MSSqlDatabase.Connection.BeginTransaction("");
            cmd.Transaction = transaction;
            cmd.CommandText = @"DELETE FROM spatial_ref_sys Where srid=@srid";
			cmd.Parameters.AddWithValue("@id", id);
            var recs = cmd.ExecuteNonQuery();
            if (recs > 0)
            { 
                transaction.Commit();
                return true;
            }
            return false;
        }
        public bool DeleteMultipleSpatial_Ref_Sys(List<DeleteMultipleModel> deleteParam,string andOr)
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            SqlTransaction transaction = this.MSSqlDatabase.Connection.BeginTransaction("");
            cmd.Transaction = transaction;
            cmd.CommandText = @"DELETE FROM Spatial_Ref_Sys Where";
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


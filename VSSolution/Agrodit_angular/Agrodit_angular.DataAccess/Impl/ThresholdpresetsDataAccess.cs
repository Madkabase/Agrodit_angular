using Agrodit_angular.DataAccess.Interface;
using Agrodit_angular.Model;
using Agrodit_angular.Utility;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Agrodit_angular.DataAccess.Impl
{
    public class ThresholdpresetsDataAccess : IThresholdpresetsDataAccess
    {
        private MSSqlDatabase MSSqlDatabase { get; set; }
        public ThresholdpresetsDataAccess(MSSqlDatabase msSqlDatabase)
        {
            MSSqlDatabase = msSqlDatabase;
        }
        public List<ThresholdpresetsModel> GetAllThresholdpresets(int page=1,int itemsPerPage=100,List<OrderByModel> orderBy=null)
        {
            var ret = new List<ThresholdpresetsModel>();
			int offset = (page - 1) * itemsPerPage;
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT  t.* FROM ThresholdPresets t  Order by t.Id OFFSET @Offset ROWS FETCH NEXT @ItemsPerPage ROWS ONLY";
             if(orderBy!=null && orderBy.Count > 0)
            {
                cmd.CommandText = Helper.ConverOrderListToSQL(cmd.CommandText,orderBy);
            }
			cmd.Parameters.AddWithValue("@Offset", offset);
            cmd.Parameters.AddWithValue("@ItemsPerPage", itemsPerPage);
            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    var t = new ThresholdpresetsModel()
                    {
                       Id= reader.GetValue<Int64>("Id"),
Name= reader.GetValue<String>("Name"),
CompanyId= reader.GetValue<Int64>("CompanyId"),
Moisture1Min= reader.GetValue<Int64>("Moisture1Min"),
Moisture1Max= reader.GetValue<Int64>("Moisture1Max"),
Moisture2Min= reader.GetValue<Int64>("Moisture2Min"),
Moisture2Max= reader.GetValue<Int64>("Moisture2Max"),
Temperature1Min= reader.GetValue<Double>("Temperature1Min"),
Temperature1Max= reader.GetValue<Double>("Temperature1Max"),
Temperature2Max= reader.GetValue<Double>("Temperature2Max"),
Temperature2Min= reader.GetValue<Double>("Temperature2Min"),
                    };

                    ret.Add(t);
                }
            return ret;
        }

        public List<ThresholdpresetsModel> SearchThresholdpresets(string searchKey, int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy=null)
        {
            var ret = new List<ThresholdpresetsModel>();
            int offset = (page - 1) * itemsPerPage;
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT  t.* FROM ThresholdPresets t  WHERE 1=1 AND  t.Name LIKE CONCAT('%',@SearchKey,'%') Order by t.Id OFFSET @Offset ROWS FETCH NEXT @ItemsPerPage ROWS ONLY";
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
                    var t = new ThresholdpresetsModel()
                    {
                       Id= reader.GetValue<Int64>("Id"),
Name= reader.GetValue<String>("Name"),
CompanyId= reader.GetValue<Int64>("CompanyId"),
Moisture1Min= reader.GetValue<Int64>("Moisture1Min"),
Moisture1Max= reader.GetValue<Int64>("Moisture1Max"),
Moisture2Min= reader.GetValue<Int64>("Moisture2Min"),
Moisture2Max= reader.GetValue<Int64>("Moisture2Max"),
Temperature1Min= reader.GetValue<Double>("Temperature1Min"),
Temperature1Max= reader.GetValue<Double>("Temperature1Max"),
Temperature2Max= reader.GetValue<Double>("Temperature2Max"),
Temperature2Min= reader.GetValue<Double>("Temperature2Min"),
                    };

                    ret.Add(t);
                }
            return ret;
        }

        public  int GetAllTotalRecordThresholdpresets()
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT Count(*) as TotalRecord FROM ThresholdPresets t";
            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                { 
                    return reader.GetInt32("TotalRecord");
                }
            return 0;
        }
        public int GetSearchTotalRecordThresholdpresets(string searchKey)
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT Count(*) as TotalRecord FROM ThresholdPresets t  WHERE 1=1 AND  t.Name LIKE CONCAT('%',@SearchKey,'%')";
            cmd.Parameters.AddWithValue("@SearchKey", searchKey);
            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                { 
                    return reader.GetInt32("TotalRecord");
                }
            return 0;
        }

		

        public ThresholdpresetsModel GetThresholdpresetsByID(int id)
        {

            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT  t.* FROM ThresholdPresets t  WHERE t.Id= @Id Order by t.Id OFFSET 0 ROWS FETCH NEXT 1 ROWS ONLY";
			cmd.Parameters.AddWithValue("@id", id);

            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    return new ThresholdpresetsModel()
                    {
                        Id= reader.GetValue<Int64>("Id"),
Name= reader.GetValue<String>("Name"),
CompanyId= reader.GetValue<Int64>("CompanyId"),
Moisture1Min= reader.GetValue<Int64>("Moisture1Min"),
Moisture1Max= reader.GetValue<Int64>("Moisture1Max"),
Moisture2Min= reader.GetValue<Int64>("Moisture2Min"),
Moisture2Max= reader.GetValue<Int64>("Moisture2Max"),
Temperature1Min= reader.GetValue<Double>("Temperature1Min"),
Temperature1Max= reader.GetValue<Double>("Temperature1Max"),
Temperature2Max= reader.GetValue<Double>("Temperature2Max"),
Temperature2Min= reader.GetValue<Double>("Temperature2Min"),
                    };
                }
            return null;
        }

         public List<ThresholdpresetsModel> FilterThresholdpresets(List<FilterModel> filterBy,string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy)
        {
            var ret = new List<ThresholdpresetsModel>();
            int offset = (page - 1) * itemsPerPage;
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT  t.* FROM ThresholdPresets t  {filterColumns} Order by t.Id OFFSET @Offset ROWS FETCH NEXT @ItemsPerPage ROWS ONLY";
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
                var t = new ThresholdpresetsModel()
                    {
                       Id= reader.GetValue<Int64>("Id"),
Name= reader.GetValue<String>("Name"),
CompanyId= reader.GetValue<Int64>("CompanyId"),
Moisture1Min= reader.GetValue<Int64>("Moisture1Min"),
Moisture1Max= reader.GetValue<Int64>("Moisture1Max"),
Moisture2Min= reader.GetValue<Int64>("Moisture2Min"),
Moisture2Max= reader.GetValue<Int64>("Moisture2Max"),
Temperature1Min= reader.GetValue<Double>("Temperature1Min"),
Temperature1Max= reader.GetValue<Double>("Temperature1Max"),
Temperature2Max= reader.GetValue<Double>("Temperature2Max"),
Temperature2Min= reader.GetValue<Double>("Temperature2Min"),
                    };

                    ret.Add(t);
                }
            return ret;
        }

       public int GetFilterTotalRecordThresholdpresets(List<FilterModel> filterBy,string andOr)
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT Count(*) as TotalRecord FROM ThresholdPresets t {filterColumns}";
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

        public bool UpdateThresholdpresets(ThresholdpresetsModel model)
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            SqlTransaction transaction = this.MSSqlDatabase.Connection.BeginTransaction("");
            cmd.Transaction = transaction;
            cmd.CommandText = @"UPDATE ThresholdPresets SET Id=@Id,Name=@Name,CompanyId=@CompanyId,Moisture1Min=@Moisture1Min,Moisture1Max=@Moisture1Max,Moisture2Min=@Moisture2Min,Moisture2Max=@Moisture2Max,Temperature1Min=@Temperature1Min,Temperature1Max=@Temperature1Max,Temperature2Max=@Temperature2Max,Temperature2Min=@Temperature2Min WHERE Id = @Id;";
            cmd.Parameters.AddWithValue("@Id", Helper.GetNullableParameter(model.Id));
cmd.Parameters.AddWithValue("@Name", Helper.GetNullableParameter(model.Name));
cmd.Parameters.AddWithValue("@CompanyId", Helper.GetNullableParameter(model.CompanyId));
cmd.Parameters.AddWithValue("@Moisture1Min", Helper.GetNullableParameter(model.Moisture1Min));
cmd.Parameters.AddWithValue("@Moisture1Max", Helper.GetNullableParameter(model.Moisture1Max));
cmd.Parameters.AddWithValue("@Moisture2Min", Helper.GetNullableParameter(model.Moisture2Min));
cmd.Parameters.AddWithValue("@Moisture2Max", Helper.GetNullableParameter(model.Moisture2Max));
cmd.Parameters.AddWithValue("@Temperature1Min", Helper.GetNullableParameter(model.Temperature1Min));
cmd.Parameters.AddWithValue("@Temperature1Max", Helper.GetNullableParameter(model.Temperature1Max));
cmd.Parameters.AddWithValue("@Temperature2Max", Helper.GetNullableParameter(model.Temperature2Max));
cmd.Parameters.AddWithValue("@Temperature2Min", Helper.GetNullableParameter(model.Temperature2Min));
            var recs = cmd.ExecuteNonQuery();
            if (recs > 0)
            {  
            transaction.Commit();
                return true;
            }
            return false;
        }

        public long AddThresholdpresets(ThresholdpresetsModel model)
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            SqlTransaction transaction = this.MSSqlDatabase.Connection.BeginTransaction("");
            cmd.Transaction = transaction;
            cmd.CommandText = @"INSERT INTO ThresholdPresets (Id,Name,CompanyId,Moisture1Min,Moisture1Max,Moisture2Min,Moisture2Max,Temperature1Min,Temperature1Max,Temperature2Max,Temperature2Min) VALUES (@Id,@Name,@CompanyId,@Moisture1Min,@Moisture1Max,@Moisture2Min,@Moisture2Max,@Temperature1Min,@Temperature1Max,@Temperature2Max,@Temperature2Min);SELECT SCOPE_IDENTITY();";
            cmd.Parameters.AddWithValue("@Id", Helper.GetNullableParameter(model.Id));
cmd.Parameters.AddWithValue("@Name", Helper.GetNullableParameter(model.Name));
cmd.Parameters.AddWithValue("@CompanyId", Helper.GetNullableParameter(model.CompanyId));
cmd.Parameters.AddWithValue("@Moisture1Min", Helper.GetNullableParameter(model.Moisture1Min));
cmd.Parameters.AddWithValue("@Moisture1Max", Helper.GetNullableParameter(model.Moisture1Max));
cmd.Parameters.AddWithValue("@Moisture2Min", Helper.GetNullableParameter(model.Moisture2Min));
cmd.Parameters.AddWithValue("@Moisture2Max", Helper.GetNullableParameter(model.Moisture2Max));
cmd.Parameters.AddWithValue("@Temperature1Min", Helper.GetNullableParameter(model.Temperature1Min));
cmd.Parameters.AddWithValue("@Temperature1Max", Helper.GetNullableParameter(model.Temperature1Max));
cmd.Parameters.AddWithValue("@Temperature2Max", Helper.GetNullableParameter(model.Temperature2Max));
cmd.Parameters.AddWithValue("@Temperature2Min", Helper.GetNullableParameter(model.Temperature2Min));
              var recs = cmd.ExecuteScalar();
            if (recs!=null)
            {
                transaction.Commit();
                long.TryParse(recs.ToString(), out long result);
                return result>0? result : 1;
            }
            return -1;
          
        }

        public bool DeleteThresholdpresets(int id)
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            SqlTransaction transaction = this.MSSqlDatabase.Connection.BeginTransaction("");
            cmd.Transaction = transaction;
            cmd.CommandText = @"DELETE FROM ThresholdPresets Where Id=@Id";
			cmd.Parameters.AddWithValue("@id", id);
            var recs = cmd.ExecuteNonQuery();
            if (recs > 0)
            { 
                transaction.Commit();
                return true;
            }
            return false;
        }
        public bool DeleteMultipleThresholdpresets(List<DeleteMultipleModel> deleteParam,string andOr)
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            SqlTransaction transaction = this.MSSqlDatabase.Connection.BeginTransaction("");
            cmd.Transaction = transaction;
            cmd.CommandText = @"DELETE FROM Thresholdpresets Where";
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


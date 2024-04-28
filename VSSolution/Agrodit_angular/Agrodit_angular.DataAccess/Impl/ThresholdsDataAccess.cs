using Agrodit_angular.DataAccess.Interface;
using Agrodit_angular.Model;
using Agrodit_angular.Utility;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Agrodit_angular.DataAccess.Impl
{
    public class ThresholdsDataAccess : IThresholdsDataAccess
    {
        private MSSqlDatabase MSSqlDatabase { get; set; }
        public ThresholdsDataAccess(MSSqlDatabase msSqlDatabase)
        {
            MSSqlDatabase = msSqlDatabase;
        }
        public List<ThresholdsModel> GetAllThresholds(int page=1,int itemsPerPage=100,List<OrderByModel> orderBy=null)
        {
            var ret = new List<ThresholdsModel>();
			int offset = (page - 1) * itemsPerPage;
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT  t.* FROM Thresholds t  Order by t.Id OFFSET @Offset ROWS FETCH NEXT @ItemsPerPage ROWS ONLY";
             if(orderBy!=null && orderBy.Count > 0)
            {
                cmd.CommandText = Helper.ConverOrderListToSQL(cmd.CommandText,orderBy);
            }
			cmd.Parameters.AddWithValue("@Offset", offset);
            cmd.Parameters.AddWithValue("@ItemsPerPage", itemsPerPage);
            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    var t = new ThresholdsModel()
                    {
                       Id= reader.GetValue<Int64>("Id"),
Moisture1Min= reader.GetValue<Int64>("Moisture1Min"),
Moisture1Max= reader.GetValue<Int64>("Moisture1Max"),
Moisture2Min= reader.GetValue<Int64>("Moisture2Min"),
Moisture2Max= reader.GetValue<Int64>("Moisture2Max"),
Temperature1Min= reader.GetValue<Double>("Temperature1Min"),
Temperature1Max= reader.GetValue<Double>("Temperature1Max"),
MainSensor= reader.GetValue<Int64>("MainSensor"),
Temperature2Max= reader.GetValue<Double>("Temperature2Max"),
Temperature2Min= reader.GetValue<Double>("Temperature2Min"),
Salinity1Max= reader.GetValue<Double>("Salinity1Max"),
Salinity1Min= reader.GetValue<Double>("Salinity1Min"),
Salinity2Max= reader.GetValue<Double>("Salinity2Max"),
Salinity2Min= reader.GetValue<Double>("Salinity2Min"),
                    };

                    ret.Add(t);
                }
            return ret;
        }

        public List<ThresholdsModel> SearchThresholds(string searchKey, int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy=null)
        {
            var ret = new List<ThresholdsModel>();
            int offset = (page - 1) * itemsPerPage;
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT  t.* FROM Thresholds t  WHERE 1=1 AND  Order by t.Id OFFSET @Offset ROWS FETCH NEXT @ItemsPerPage ROWS ONLY";
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
                    var t = new ThresholdsModel()
                    {
                       Id= reader.GetValue<Int64>("Id"),
Moisture1Min= reader.GetValue<Int64>("Moisture1Min"),
Moisture1Max= reader.GetValue<Int64>("Moisture1Max"),
Moisture2Min= reader.GetValue<Int64>("Moisture2Min"),
Moisture2Max= reader.GetValue<Int64>("Moisture2Max"),
Temperature1Min= reader.GetValue<Double>("Temperature1Min"),
Temperature1Max= reader.GetValue<Double>("Temperature1Max"),
MainSensor= reader.GetValue<Int64>("MainSensor"),
Temperature2Max= reader.GetValue<Double>("Temperature2Max"),
Temperature2Min= reader.GetValue<Double>("Temperature2Min"),
Salinity1Max= reader.GetValue<Double>("Salinity1Max"),
Salinity1Min= reader.GetValue<Double>("Salinity1Min"),
Salinity2Max= reader.GetValue<Double>("Salinity2Max"),
Salinity2Min= reader.GetValue<Double>("Salinity2Min"),
                    };

                    ret.Add(t);
                }
            return ret;
        }

        public  int GetAllTotalRecordThresholds()
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT Count(*) as TotalRecord FROM Thresholds t";
            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                { 
                    return reader.GetInt32("TotalRecord");
                }
            return 0;
        }
        public int GetSearchTotalRecordThresholds(string searchKey)
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT Count(*) as TotalRecord FROM Thresholds t  WHERE 1=1 AND";
            cmd.Parameters.AddWithValue("@SearchKey", searchKey);
            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                { 
                    return reader.GetInt32("TotalRecord");
                }
            return 0;
        }

		

        public ThresholdsModel GetThresholdsByID(int id)
        {

            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT  t.* FROM Thresholds t  WHERE t.Id= @Id Order by t.Id OFFSET 0 ROWS FETCH NEXT 1 ROWS ONLY";
			cmd.Parameters.AddWithValue("@id", id);

            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    return new ThresholdsModel()
                    {
                        Id= reader.GetValue<Int64>("Id"),
Moisture1Min= reader.GetValue<Int64>("Moisture1Min"),
Moisture1Max= reader.GetValue<Int64>("Moisture1Max"),
Moisture2Min= reader.GetValue<Int64>("Moisture2Min"),
Moisture2Max= reader.GetValue<Int64>("Moisture2Max"),
Temperature1Min= reader.GetValue<Double>("Temperature1Min"),
Temperature1Max= reader.GetValue<Double>("Temperature1Max"),
MainSensor= reader.GetValue<Int64>("MainSensor"),
Temperature2Max= reader.GetValue<Double>("Temperature2Max"),
Temperature2Min= reader.GetValue<Double>("Temperature2Min"),
Salinity1Max= reader.GetValue<Double>("Salinity1Max"),
Salinity1Min= reader.GetValue<Double>("Salinity1Min"),
Salinity2Max= reader.GetValue<Double>("Salinity2Max"),
Salinity2Min= reader.GetValue<Double>("Salinity2Min"),
                    };
                }
            return null;
        }

         public List<ThresholdsModel> FilterThresholds(List<FilterModel> filterBy,string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy)
        {
            var ret = new List<ThresholdsModel>();
            int offset = (page - 1) * itemsPerPage;
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT  t.* FROM Thresholds t  {filterColumns} Order by t.Id OFFSET @Offset ROWS FETCH NEXT @ItemsPerPage ROWS ONLY";
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
                var t = new ThresholdsModel()
                    {
                       Id= reader.GetValue<Int64>("Id"),
Moisture1Min= reader.GetValue<Int64>("Moisture1Min"),
Moisture1Max= reader.GetValue<Int64>("Moisture1Max"),
Moisture2Min= reader.GetValue<Int64>("Moisture2Min"),
Moisture2Max= reader.GetValue<Int64>("Moisture2Max"),
Temperature1Min= reader.GetValue<Double>("Temperature1Min"),
Temperature1Max= reader.GetValue<Double>("Temperature1Max"),
MainSensor= reader.GetValue<Int64>("MainSensor"),
Temperature2Max= reader.GetValue<Double>("Temperature2Max"),
Temperature2Min= reader.GetValue<Double>("Temperature2Min"),
Salinity1Max= reader.GetValue<Double>("Salinity1Max"),
Salinity1Min= reader.GetValue<Double>("Salinity1Min"),
Salinity2Max= reader.GetValue<Double>("Salinity2Max"),
Salinity2Min= reader.GetValue<Double>("Salinity2Min"),
                    };

                    ret.Add(t);
                }
            return ret;
        }

       public int GetFilterTotalRecordThresholds(List<FilterModel> filterBy,string andOr)
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT Count(*) as TotalRecord FROM Thresholds t {filterColumns}";
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

        public bool UpdateThresholds(ThresholdsModel model)
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            SqlTransaction transaction = this.MSSqlDatabase.Connection.BeginTransaction("");
            cmd.Transaction = transaction;
            cmd.CommandText = @"UPDATE Thresholds SET Id=@Id,Moisture1Min=@Moisture1Min,Moisture1Max=@Moisture1Max,Moisture2Min=@Moisture2Min,Moisture2Max=@Moisture2Max,Temperature1Min=@Temperature1Min,Temperature1Max=@Temperature1Max,MainSensor=@MainSensor,Temperature2Max=@Temperature2Max,Temperature2Min=@Temperature2Min,Salinity1Max=@Salinity1Max,Salinity1Min=@Salinity1Min,Salinity2Max=@Salinity2Max,Salinity2Min=@Salinity2Min WHERE Id = @Id;";
            cmd.Parameters.AddWithValue("@Id", Helper.GetNullableParameter(model.Id));
cmd.Parameters.AddWithValue("@Moisture1Min", Helper.GetNullableParameter(model.Moisture1Min));
cmd.Parameters.AddWithValue("@Moisture1Max", Helper.GetNullableParameter(model.Moisture1Max));
cmd.Parameters.AddWithValue("@Moisture2Min", Helper.GetNullableParameter(model.Moisture2Min));
cmd.Parameters.AddWithValue("@Moisture2Max", Helper.GetNullableParameter(model.Moisture2Max));
cmd.Parameters.AddWithValue("@Temperature1Min", Helper.GetNullableParameter(model.Temperature1Min));
cmd.Parameters.AddWithValue("@Temperature1Max", Helper.GetNullableParameter(model.Temperature1Max));
cmd.Parameters.AddWithValue("@MainSensor", Helper.GetNullableParameter(model.MainSensor));
cmd.Parameters.AddWithValue("@Temperature2Max", Helper.GetNullableParameter(model.Temperature2Max));
cmd.Parameters.AddWithValue("@Temperature2Min", Helper.GetNullableParameter(model.Temperature2Min));
cmd.Parameters.AddWithValue("@Salinity1Max", Helper.GetNullableParameter(model.Salinity1Max));
cmd.Parameters.AddWithValue("@Salinity1Min", Helper.GetNullableParameter(model.Salinity1Min));
cmd.Parameters.AddWithValue("@Salinity2Max", Helper.GetNullableParameter(model.Salinity2Max));
cmd.Parameters.AddWithValue("@Salinity2Min", Helper.GetNullableParameter(model.Salinity2Min));
            var recs = cmd.ExecuteNonQuery();
            if (recs > 0)
            {  
            transaction.Commit();
                return true;
            }
            return false;
        }

        public long AddThresholds(ThresholdsModel model)
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            SqlTransaction transaction = this.MSSqlDatabase.Connection.BeginTransaction("");
            cmd.Transaction = transaction;
            cmd.CommandText = @"INSERT INTO Thresholds (Id,Moisture1Min,Moisture1Max,Moisture2Min,Moisture2Max,Temperature1Min,Temperature1Max,MainSensor,Temperature2Max,Temperature2Min,Salinity1Max,Salinity1Min,Salinity2Max,Salinity2Min) VALUES (@Id,@Moisture1Min,@Moisture1Max,@Moisture2Min,@Moisture2Max,@Temperature1Min,@Temperature1Max,@MainSensor,@Temperature2Max,@Temperature2Min,@Salinity1Max,@Salinity1Min,@Salinity2Max,@Salinity2Min);SELECT SCOPE_IDENTITY();";
            cmd.Parameters.AddWithValue("@Id", Helper.GetNullableParameter(model.Id));
cmd.Parameters.AddWithValue("@Moisture1Min", Helper.GetNullableParameter(model.Moisture1Min));
cmd.Parameters.AddWithValue("@Moisture1Max", Helper.GetNullableParameter(model.Moisture1Max));
cmd.Parameters.AddWithValue("@Moisture2Min", Helper.GetNullableParameter(model.Moisture2Min));
cmd.Parameters.AddWithValue("@Moisture2Max", Helper.GetNullableParameter(model.Moisture2Max));
cmd.Parameters.AddWithValue("@Temperature1Min", Helper.GetNullableParameter(model.Temperature1Min));
cmd.Parameters.AddWithValue("@Temperature1Max", Helper.GetNullableParameter(model.Temperature1Max));
cmd.Parameters.AddWithValue("@MainSensor", Helper.GetNullableParameter(model.MainSensor));
cmd.Parameters.AddWithValue("@Temperature2Max", Helper.GetNullableParameter(model.Temperature2Max));
cmd.Parameters.AddWithValue("@Temperature2Min", Helper.GetNullableParameter(model.Temperature2Min));
cmd.Parameters.AddWithValue("@Salinity1Max", Helper.GetNullableParameter(model.Salinity1Max));
cmd.Parameters.AddWithValue("@Salinity1Min", Helper.GetNullableParameter(model.Salinity1Min));
cmd.Parameters.AddWithValue("@Salinity2Max", Helper.GetNullableParameter(model.Salinity2Max));
cmd.Parameters.AddWithValue("@Salinity2Min", Helper.GetNullableParameter(model.Salinity2Min));
              var recs = cmd.ExecuteScalar();
            if (recs!=null)
            {
                transaction.Commit();
                long.TryParse(recs.ToString(), out long result);
                return result>0? result : 1;
            }
            return -1;
          
        }

        public bool DeleteThresholds(int id)
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            SqlTransaction transaction = this.MSSqlDatabase.Connection.BeginTransaction("");
            cmd.Transaction = transaction;
            cmd.CommandText = @"DELETE FROM Thresholds Where Id=@Id";
			cmd.Parameters.AddWithValue("@id", id);
            var recs = cmd.ExecuteNonQuery();
            if (recs > 0)
            { 
                transaction.Commit();
                return true;
            }
            return false;
        }
        public bool DeleteMultipleThresholds(List<DeleteMultipleModel> deleteParam,string andOr)
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            SqlTransaction transaction = this.MSSqlDatabase.Connection.BeginTransaction("");
            cmd.Transaction = transaction;
            cmd.CommandText = @"DELETE FROM Thresholds Where";
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


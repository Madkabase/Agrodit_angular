using Agrodit_angular.DataAccess.Interface;
using Agrodit_angular.Model;
using Agrodit_angular.Utility;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Agrodit_angular.DataAccess.Impl
{
    public class DevicedataDataAccess : IDevicedataDataAccess
    {
        private MSSqlDatabase MSSqlDatabase { get; set; }
        public DevicedataDataAccess(MSSqlDatabase msSqlDatabase)
        {
            MSSqlDatabase = msSqlDatabase;
        }
        public List<DevicedataModel> GetAllDevicedata(int page=1,int itemsPerPage=100,List<OrderByModel> orderBy=null)
        {
            var ret = new List<DevicedataModel>();
			int offset = (page - 1) * itemsPerPage;
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT  t.* FROM devicedata t  Order by t.Id OFFSET @Offset ROWS FETCH NEXT @ItemsPerPage ROWS ONLY";
             if(orderBy!=null && orderBy.Count > 0)
            {
                cmd.CommandText = Helper.ConverOrderListToSQL(cmd.CommandText,orderBy);
            }
			cmd.Parameters.AddWithValue("@Offset", offset);
            cmd.Parameters.AddWithValue("@ItemsPerPage", itemsPerPage);
            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    var t = new DevicedataModel()
                    {
                       Id= reader.GetValue<Int64>("Id"),
Moisture1= reader.GetValue<Double>("Moisture1"),
Moisture2= reader.GetValue<Double>("Moisture2"),
BatteryLevel= reader.IsDBNull(Helper.GetColumnOrder(reader,"BatteryLevel")) ? (Int64?)null : reader.GetInt64("BatteryLevel"),
Temperature1= reader.GetValue<Double>("Temperature1"),
TimeStamp= reader.GetValue<DateTime>("TimeStamp"),
Temperature2= reader.GetValue<Double>("Temperature2"),
Salinity1= reader.GetValue<Int64>("Salinity1"),
Salinity2= reader.GetValue<Int64>("Salinity2"),
DeviceId= reader.IsDBNull(Helper.GetColumnOrder(reader,"DeviceId")) ? (Int64?)null : reader.GetInt64("DeviceId"),
                    };

                    ret.Add(t);
                }
            return ret;
        }

        public List<DevicedataModel> SearchDevicedata(string searchKey, int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy=null)
        {
            var ret = new List<DevicedataModel>();
            int offset = (page - 1) * itemsPerPage;
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT  t.* FROM devicedata t  WHERE 1=1 AND  Order by t.Id OFFSET @Offset ROWS FETCH NEXT @ItemsPerPage ROWS ONLY";
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
                    var t = new DevicedataModel()
                    {
                       Id= reader.GetValue<Int64>("Id"),
Moisture1= reader.GetValue<Double>("Moisture1"),
Moisture2= reader.GetValue<Double>("Moisture2"),
BatteryLevel= reader.IsDBNull(Helper.GetColumnOrder(reader,"BatteryLevel")) ? (Int64?)null : reader.GetInt64("BatteryLevel"),
Temperature1= reader.GetValue<Double>("Temperature1"),
TimeStamp= reader.GetValue<DateTime>("TimeStamp"),
Temperature2= reader.GetValue<Double>("Temperature2"),
Salinity1= reader.GetValue<Int64>("Salinity1"),
Salinity2= reader.GetValue<Int64>("Salinity2"),
DeviceId= reader.IsDBNull(Helper.GetColumnOrder(reader,"DeviceId")) ? (Int64?)null : reader.GetInt64("DeviceId"),
                    };

                    ret.Add(t);
                }
            return ret;
        }

        public  int GetAllTotalRecordDevicedata()
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT Count(*) as TotalRecord FROM devicedata t";
            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                { 
                    return reader.GetInt32("TotalRecord");
                }
            return 0;
        }
        public int GetSearchTotalRecordDevicedata(string searchKey)
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT Count(*) as TotalRecord FROM devicedata t  WHERE 1=1 AND";
            cmd.Parameters.AddWithValue("@SearchKey", searchKey);
            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                { 
                    return reader.GetInt32("TotalRecord");
                }
            return 0;
        }

		

        public DevicedataModel GetDevicedataByID(int id)
        {

            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT  t.* FROM devicedata t  WHERE t.Id= @Id Order by t.Id OFFSET 0 ROWS FETCH NEXT 1 ROWS ONLY";
			cmd.Parameters.AddWithValue("@id", id);

            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    return new DevicedataModel()
                    {
                        Id= reader.GetValue<Int64>("Id"),
Moisture1= reader.GetValue<Double>("Moisture1"),
Moisture2= reader.GetValue<Double>("Moisture2"),
BatteryLevel= reader.IsDBNull(Helper.GetColumnOrder(reader,"BatteryLevel")) ? (Int64?)null : reader.GetInt64("BatteryLevel"),
Temperature1= reader.GetValue<Double>("Temperature1"),
TimeStamp= reader.GetValue<DateTime>("TimeStamp"),
Temperature2= reader.GetValue<Double>("Temperature2"),
Salinity1= reader.GetValue<Int64>("Salinity1"),
Salinity2= reader.GetValue<Int64>("Salinity2"),
DeviceId= reader.IsDBNull(Helper.GetColumnOrder(reader,"DeviceId")) ? (Int64?)null : reader.GetInt64("DeviceId"),
                    };
                }
            return null;
        }

         public List<DevicedataModel> FilterDevicedata(List<FilterModel> filterBy,string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy)
        {
            var ret = new List<DevicedataModel>();
            int offset = (page - 1) * itemsPerPage;
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT  t.* FROM devicedata t  {filterColumns} Order by t.Id OFFSET @Offset ROWS FETCH NEXT @ItemsPerPage ROWS ONLY";
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
                var t = new DevicedataModel()
                    {
                       Id= reader.GetValue<Int64>("Id"),
Moisture1= reader.GetValue<Double>("Moisture1"),
Moisture2= reader.GetValue<Double>("Moisture2"),
BatteryLevel= reader.IsDBNull(Helper.GetColumnOrder(reader,"BatteryLevel")) ? (Int64?)null : reader.GetInt64("BatteryLevel"),
Temperature1= reader.GetValue<Double>("Temperature1"),
TimeStamp= reader.GetValue<DateTime>("TimeStamp"),
Temperature2= reader.GetValue<Double>("Temperature2"),
Salinity1= reader.GetValue<Int64>("Salinity1"),
Salinity2= reader.GetValue<Int64>("Salinity2"),
DeviceId= reader.IsDBNull(Helper.GetColumnOrder(reader,"DeviceId")) ? (Int64?)null : reader.GetInt64("DeviceId"),
                    };

                    ret.Add(t);
                }
            return ret;
        }

       public int GetFilterTotalRecordDevicedata(List<FilterModel> filterBy,string andOr)
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT Count(*) as TotalRecord FROM devicedata t {filterColumns}";
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

        public bool UpdateDevicedata(DevicedataModel model)
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            SqlTransaction transaction = this.MSSqlDatabase.Connection.BeginTransaction("");
            cmd.Transaction = transaction;
            cmd.CommandText = @"UPDATE devicedata SET Id=@Id,Moisture1=@Moisture1,Moisture2=@Moisture2,BatteryLevel=@BatteryLevel,Temperature1=@Temperature1,TimeStamp=@TimeStamp,Temperature2=@Temperature2,Salinity1=@Salinity1,Salinity2=@Salinity2,DeviceId=@DeviceId WHERE Id = @Id;";
            cmd.Parameters.AddWithValue("@Id", Helper.GetNullableParameter(model.Id));
cmd.Parameters.AddWithValue("@Moisture1", Helper.GetNullableParameter(model.Moisture1));
cmd.Parameters.AddWithValue("@Moisture2", Helper.GetNullableParameter(model.Moisture2));
cmd.Parameters.AddWithValue("@BatteryLevel", Helper.GetNullableParameter(model.BatteryLevel));
cmd.Parameters.AddWithValue("@Temperature1", Helper.GetNullableParameter(model.Temperature1));
cmd.Parameters.AddWithValue("@TimeStamp", Helper.GetNullableParameter(model.TimeStamp));
cmd.Parameters.AddWithValue("@Temperature2", Helper.GetNullableParameter(model.Temperature2));
cmd.Parameters.AddWithValue("@Salinity1", Helper.GetNullableParameter(model.Salinity1));
cmd.Parameters.AddWithValue("@Salinity2", Helper.GetNullableParameter(model.Salinity2));
cmd.Parameters.AddWithValue("@DeviceId", Helper.GetNullableParameter(model.DeviceId));
            var recs = cmd.ExecuteNonQuery();
            if (recs > 0)
            {  
            transaction.Commit();
                return true;
            }
            return false;
        }

        public long AddDevicedata(DevicedataModel model)
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            SqlTransaction transaction = this.MSSqlDatabase.Connection.BeginTransaction("");
            cmd.Transaction = transaction;
            cmd.CommandText = @"INSERT INTO devicedata (Id,Moisture1,Moisture2,BatteryLevel,Temperature1,TimeStamp,Temperature2,Salinity1,Salinity2,DeviceId) VALUES (@Id,@Moisture1,@Moisture2,@BatteryLevel,@Temperature1,@TimeStamp,@Temperature2,@Salinity1,@Salinity2,@DeviceId);SELECT SCOPE_IDENTITY();";
            cmd.Parameters.AddWithValue("@Id", Helper.GetNullableParameter(model.Id));
cmd.Parameters.AddWithValue("@Moisture1", Helper.GetNullableParameter(model.Moisture1));
cmd.Parameters.AddWithValue("@Moisture2", Helper.GetNullableParameter(model.Moisture2));
cmd.Parameters.AddWithValue("@BatteryLevel", Helper.GetNullableParameter(model.BatteryLevel));
cmd.Parameters.AddWithValue("@Temperature1", Helper.GetNullableParameter(model.Temperature1));
cmd.Parameters.AddWithValue("@TimeStamp", Helper.GetNullableParameter(model.TimeStamp));
cmd.Parameters.AddWithValue("@Temperature2", Helper.GetNullableParameter(model.Temperature2));
cmd.Parameters.AddWithValue("@Salinity1", Helper.GetNullableParameter(model.Salinity1));
cmd.Parameters.AddWithValue("@Salinity2", Helper.GetNullableParameter(model.Salinity2));
cmd.Parameters.AddWithValue("@DeviceId", Helper.GetNullableParameter(model.DeviceId));
              var recs = cmd.ExecuteScalar();
            if (recs!=null)
            {
                transaction.Commit();
                long.TryParse(recs.ToString(), out long result);
                return result>0? result : 1;
            }
            return -1;
          
        }

        public bool DeleteDevicedata(int id)
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            SqlTransaction transaction = this.MSSqlDatabase.Connection.BeginTransaction("");
            cmd.Transaction = transaction;
            cmd.CommandText = @"DELETE FROM devicedata Where Id=@Id";
			cmd.Parameters.AddWithValue("@id", id);
            var recs = cmd.ExecuteNonQuery();
            if (recs > 0)
            { 
                transaction.Commit();
                return true;
            }
            return false;
        }
        public bool DeleteMultipleDevicedata(List<DeleteMultipleModel> deleteParam,string andOr)
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            SqlTransaction transaction = this.MSSqlDatabase.Connection.BeginTransaction("");
            cmd.Transaction = transaction;
            cmd.CommandText = @"DELETE FROM Devicedata Where";
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


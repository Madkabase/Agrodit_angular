using Agrodit_angular.DataAccess.Interface;
using Agrodit_angular.Model;
using Agrodit_angular.Utility;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Agrodit_angular.DataAccess.Impl
{
    public class DevicesDataAccess : IDevicesDataAccess
    {
        private MSSqlDatabase MSSqlDatabase { get; set; }
        public DevicesDataAccess(MSSqlDatabase msSqlDatabase)
        {
            MSSqlDatabase = msSqlDatabase;
        }
        public List<DevicesModel> GetAllDevices(int page=1,int itemsPerPage=100,List<OrderByModel> orderBy=null)
        {
            var ret = new List<DevicesModel>();
			int offset = (page - 1) * itemsPerPage;
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT  t.* FROM devices t  Order by t.FieldId OFFSET @Offset ROWS FETCH NEXT @ItemsPerPage ROWS ONLY";
             if(orderBy!=null && orderBy.Count > 0)
            {
                cmd.CommandText = Helper.ConverOrderListToSQL(cmd.CommandText,orderBy);
            }
			cmd.Parameters.AddWithValue("@Offset", offset);
            cmd.Parameters.AddWithValue("@ItemsPerPage", itemsPerPage);
            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    var t = new DevicesModel()
                    {
                       Name= reader.GetValue<String>("Name"),
DevEUI= reader.GetValue<String>("DevEUI"),
JoinEUI= reader.GetValue<String>("JoinEUI"),
AppKey= reader.GetValue<String>("AppKey"),
FieldId= reader.GetValue<Int64>("FieldId"),
CalibrationMoisture1Max= reader.IsDBNull(Helper.GetColumnOrder(reader,"CalibrationMoisture1Max")) ? (Int64?)null : reader.GetInt64("CalibrationMoisture1Max"),
CalibrationMoisture1Min= reader.IsDBNull(Helper.GetColumnOrder(reader,"CalibrationMoisture1Min")) ? (Int64?)null : reader.GetInt64("CalibrationMoisture1Min"),
CalibrationMoisture2Max= reader.IsDBNull(Helper.GetColumnOrder(reader,"CalibrationMoisture2Max")) ? (Int64?)null : reader.GetInt64("CalibrationMoisture2Max"),
CalibrationMoisture2Min= reader.IsDBNull(Helper.GetColumnOrder(reader,"CalibrationMoisture2Min")) ? (Int64?)null : reader.GetInt64("CalibrationMoisture2Min"),
CalibrationSalinity1Max= reader.IsDBNull(Helper.GetColumnOrder(reader,"CalibrationSalinity1Max")) ? (Int64?)null : reader.GetInt64("CalibrationSalinity1Max"),
CalibrationSalinity1Min= reader.IsDBNull(Helper.GetColumnOrder(reader,"CalibrationSalinity1Min")) ? (Int64?)null : reader.GetInt64("CalibrationSalinity1Min"),
CalibrationSalinity2Max= reader.IsDBNull(Helper.GetColumnOrder(reader,"CalibrationSalinity2Max")) ? (Int64?)null : reader.GetInt64("CalibrationSalinity2Max"),
CalibrationSalinity2Min= reader.IsDBNull(Helper.GetColumnOrder(reader,"CalibrationSalinity2Min")) ? (Int64?)null : reader.GetInt64("CalibrationSalinity2Min"),
Location= reader.IsDBNull(Helper.GetColumnOrder(reader,"Location")) ? (Double?)null : reader.GetDouble("Location"),
Id= reader.GetValue<Int64>("Id"),
Status= reader.GetValue<String>("Status"),
CompanyId= reader.IsDBNull(Helper.GetColumnOrder(reader,"CompanyId")) ? (Int64?)null : reader.GetInt64("CompanyId"),
                    };

                    ret.Add(t);
                }
            return ret;
        }

        public List<DevicesModel> SearchDevices(string searchKey, int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy=null)
        {
            var ret = new List<DevicesModel>();
            int offset = (page - 1) * itemsPerPage;
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT  t.* FROM devices t  WHERE 1=1 AND  t.Name LIKE CONCAT('%',@SearchKey,'%') OR t.DevEUI LIKE CONCAT('%',@SearchKey,'%') OR t.JoinEUI LIKE CONCAT('%',@SearchKey,'%') OR t.AppKey LIKE CONCAT('%',@SearchKey,'%') OR t.Status LIKE CONCAT('%',@SearchKey,'%') Order by t.FieldId OFFSET @Offset ROWS FETCH NEXT @ItemsPerPage ROWS ONLY";
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
                    var t = new DevicesModel()
                    {
                       Name= reader.GetValue<String>("Name"),
DevEUI= reader.GetValue<String>("DevEUI"),
JoinEUI= reader.GetValue<String>("JoinEUI"),
AppKey= reader.GetValue<String>("AppKey"),
FieldId= reader.GetValue<Int64>("FieldId"),
CalibrationMoisture1Max= reader.IsDBNull(Helper.GetColumnOrder(reader,"CalibrationMoisture1Max")) ? (Int64?)null : reader.GetInt64("CalibrationMoisture1Max"),
CalibrationMoisture1Min= reader.IsDBNull(Helper.GetColumnOrder(reader,"CalibrationMoisture1Min")) ? (Int64?)null : reader.GetInt64("CalibrationMoisture1Min"),
CalibrationMoisture2Max= reader.IsDBNull(Helper.GetColumnOrder(reader,"CalibrationMoisture2Max")) ? (Int64?)null : reader.GetInt64("CalibrationMoisture2Max"),
CalibrationMoisture2Min= reader.IsDBNull(Helper.GetColumnOrder(reader,"CalibrationMoisture2Min")) ? (Int64?)null : reader.GetInt64("CalibrationMoisture2Min"),
CalibrationSalinity1Max= reader.IsDBNull(Helper.GetColumnOrder(reader,"CalibrationSalinity1Max")) ? (Int64?)null : reader.GetInt64("CalibrationSalinity1Max"),
CalibrationSalinity1Min= reader.IsDBNull(Helper.GetColumnOrder(reader,"CalibrationSalinity1Min")) ? (Int64?)null : reader.GetInt64("CalibrationSalinity1Min"),
CalibrationSalinity2Max= reader.IsDBNull(Helper.GetColumnOrder(reader,"CalibrationSalinity2Max")) ? (Int64?)null : reader.GetInt64("CalibrationSalinity2Max"),
CalibrationSalinity2Min= reader.IsDBNull(Helper.GetColumnOrder(reader,"CalibrationSalinity2Min")) ? (Int64?)null : reader.GetInt64("CalibrationSalinity2Min"),
Location= reader.IsDBNull(Helper.GetColumnOrder(reader,"Location")) ? (Double?)null : reader.GetDouble("Location"),
Id= reader.GetValue<Int64>("Id"),
Status= reader.GetValue<String>("Status"),
CompanyId= reader.IsDBNull(Helper.GetColumnOrder(reader,"CompanyId")) ? (Int64?)null : reader.GetInt64("CompanyId"),
                    };

                    ret.Add(t);
                }
            return ret;
        }

        public  int GetAllTotalRecordDevices()
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT Count(*) as TotalRecord FROM devices t";
            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                { 
                    return reader.GetInt32("TotalRecord");
                }
            return 0;
        }
        public int GetSearchTotalRecordDevices(string searchKey)
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT Count(*) as TotalRecord FROM devices t  WHERE 1=1 AND  t.Name LIKE CONCAT('%',@SearchKey,'%') OR t.DevEUI LIKE CONCAT('%',@SearchKey,'%') OR t.JoinEUI LIKE CONCAT('%',@SearchKey,'%') OR t.AppKey LIKE CONCAT('%',@SearchKey,'%') OR t.Status LIKE CONCAT('%',@SearchKey,'%')";
            cmd.Parameters.AddWithValue("@SearchKey", searchKey);
            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                { 
                    return reader.GetInt32("TotalRecord");
                }
            return 0;
        }

		

        public DevicesModel GetDevicesByID(int id)
        {

            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT  t.* FROM devices t  WHERE t.FieldId= @FieldId Order by t.FieldId OFFSET 0 ROWS FETCH NEXT 1 ROWS ONLY";
			cmd.Parameters.AddWithValue("@id", id);

            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    return new DevicesModel()
                    {
                        Name= reader.GetValue<String>("Name"),
DevEUI= reader.GetValue<String>("DevEUI"),
JoinEUI= reader.GetValue<String>("JoinEUI"),
AppKey= reader.GetValue<String>("AppKey"),
FieldId= reader.GetValue<Int64>("FieldId"),
CalibrationMoisture1Max= reader.IsDBNull(Helper.GetColumnOrder(reader,"CalibrationMoisture1Max")) ? (Int64?)null : reader.GetInt64("CalibrationMoisture1Max"),
CalibrationMoisture1Min= reader.IsDBNull(Helper.GetColumnOrder(reader,"CalibrationMoisture1Min")) ? (Int64?)null : reader.GetInt64("CalibrationMoisture1Min"),
CalibrationMoisture2Max= reader.IsDBNull(Helper.GetColumnOrder(reader,"CalibrationMoisture2Max")) ? (Int64?)null : reader.GetInt64("CalibrationMoisture2Max"),
CalibrationMoisture2Min= reader.IsDBNull(Helper.GetColumnOrder(reader,"CalibrationMoisture2Min")) ? (Int64?)null : reader.GetInt64("CalibrationMoisture2Min"),
CalibrationSalinity1Max= reader.IsDBNull(Helper.GetColumnOrder(reader,"CalibrationSalinity1Max")) ? (Int64?)null : reader.GetInt64("CalibrationSalinity1Max"),
CalibrationSalinity1Min= reader.IsDBNull(Helper.GetColumnOrder(reader,"CalibrationSalinity1Min")) ? (Int64?)null : reader.GetInt64("CalibrationSalinity1Min"),
CalibrationSalinity2Max= reader.IsDBNull(Helper.GetColumnOrder(reader,"CalibrationSalinity2Max")) ? (Int64?)null : reader.GetInt64("CalibrationSalinity2Max"),
CalibrationSalinity2Min= reader.IsDBNull(Helper.GetColumnOrder(reader,"CalibrationSalinity2Min")) ? (Int64?)null : reader.GetInt64("CalibrationSalinity2Min"),
Location= reader.IsDBNull(Helper.GetColumnOrder(reader,"Location")) ? (Double?)null : reader.GetDouble("Location"),
Id= reader.GetValue<Int64>("Id"),
Status= reader.GetValue<String>("Status"),
CompanyId= reader.IsDBNull(Helper.GetColumnOrder(reader,"CompanyId")) ? (Int64?)null : reader.GetInt64("CompanyId"),
                    };
                }
            return null;
        }

         public List<DevicesModel> FilterDevices(List<FilterModel> filterBy,string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy)
        {
            var ret = new List<DevicesModel>();
            int offset = (page - 1) * itemsPerPage;
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT  t.* FROM devices t  {filterColumns} Order by t.FieldId OFFSET @Offset ROWS FETCH NEXT @ItemsPerPage ROWS ONLY";
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
                var t = new DevicesModel()
                    {
                       Name= reader.GetValue<String>("Name"),
DevEUI= reader.GetValue<String>("DevEUI"),
JoinEUI= reader.GetValue<String>("JoinEUI"),
AppKey= reader.GetValue<String>("AppKey"),
FieldId= reader.GetValue<Int64>("FieldId"),
CalibrationMoisture1Max= reader.IsDBNull(Helper.GetColumnOrder(reader,"CalibrationMoisture1Max")) ? (Int64?)null : reader.GetInt64("CalibrationMoisture1Max"),
CalibrationMoisture1Min= reader.IsDBNull(Helper.GetColumnOrder(reader,"CalibrationMoisture1Min")) ? (Int64?)null : reader.GetInt64("CalibrationMoisture1Min"),
CalibrationMoisture2Max= reader.IsDBNull(Helper.GetColumnOrder(reader,"CalibrationMoisture2Max")) ? (Int64?)null : reader.GetInt64("CalibrationMoisture2Max"),
CalibrationMoisture2Min= reader.IsDBNull(Helper.GetColumnOrder(reader,"CalibrationMoisture2Min")) ? (Int64?)null : reader.GetInt64("CalibrationMoisture2Min"),
CalibrationSalinity1Max= reader.IsDBNull(Helper.GetColumnOrder(reader,"CalibrationSalinity1Max")) ? (Int64?)null : reader.GetInt64("CalibrationSalinity1Max"),
CalibrationSalinity1Min= reader.IsDBNull(Helper.GetColumnOrder(reader,"CalibrationSalinity1Min")) ? (Int64?)null : reader.GetInt64("CalibrationSalinity1Min"),
CalibrationSalinity2Max= reader.IsDBNull(Helper.GetColumnOrder(reader,"CalibrationSalinity2Max")) ? (Int64?)null : reader.GetInt64("CalibrationSalinity2Max"),
CalibrationSalinity2Min= reader.IsDBNull(Helper.GetColumnOrder(reader,"CalibrationSalinity2Min")) ? (Int64?)null : reader.GetInt64("CalibrationSalinity2Min"),
Location= reader.IsDBNull(Helper.GetColumnOrder(reader,"Location")) ? (Double?)null : reader.GetDouble("Location"),
Id= reader.GetValue<Int64>("Id"),
Status= reader.GetValue<String>("Status"),
CompanyId= reader.IsDBNull(Helper.GetColumnOrder(reader,"CompanyId")) ? (Int64?)null : reader.GetInt64("CompanyId"),
                    };

                    ret.Add(t);
                }
            return ret;
        }

       public int GetFilterTotalRecordDevices(List<FilterModel> filterBy,string andOr)
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT Count(*) as TotalRecord FROM devices t {filterColumns}";
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

        public bool UpdateDevices(DevicesModel model)
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            SqlTransaction transaction = this.MSSqlDatabase.Connection.BeginTransaction("");
            cmd.Transaction = transaction;
            cmd.CommandText = @"UPDATE devices SET Name=@Name,DevEUI=@DevEUI,JoinEUI=@JoinEUI,AppKey=@AppKey,FieldId=@FieldId,CalibrationMoisture1Max=@CalibrationMoisture1Max,CalibrationMoisture1Min=@CalibrationMoisture1Min,CalibrationMoisture2Max=@CalibrationMoisture2Max,CalibrationMoisture2Min=@CalibrationMoisture2Min,CalibrationSalinity1Max=@CalibrationSalinity1Max,CalibrationSalinity1Min=@CalibrationSalinity1Min,CalibrationSalinity2Max=@CalibrationSalinity2Max,CalibrationSalinity2Min=@CalibrationSalinity2Min,Location=@Location,Id=@Id,Status=@Status,CompanyId=@CompanyId WHERE FieldId = @FieldId;";
            cmd.Parameters.AddWithValue("@Name", Helper.GetNullableParameter(model.Name));
cmd.Parameters.AddWithValue("@DevEUI", Helper.GetNullableParameter(model.DevEUI));
cmd.Parameters.AddWithValue("@JoinEUI", Helper.GetNullableParameter(model.JoinEUI));
cmd.Parameters.AddWithValue("@AppKey", Helper.GetNullableParameter(model.AppKey));
cmd.Parameters.AddWithValue("@FieldId", Helper.GetNullableParameter(model.FieldId));
cmd.Parameters.AddWithValue("@CalibrationMoisture1Max", Helper.GetNullableParameter(model.CalibrationMoisture1Max));
cmd.Parameters.AddWithValue("@CalibrationMoisture1Min", Helper.GetNullableParameter(model.CalibrationMoisture1Min));
cmd.Parameters.AddWithValue("@CalibrationMoisture2Max", Helper.GetNullableParameter(model.CalibrationMoisture2Max));
cmd.Parameters.AddWithValue("@CalibrationMoisture2Min", Helper.GetNullableParameter(model.CalibrationMoisture2Min));
cmd.Parameters.AddWithValue("@CalibrationSalinity1Max", Helper.GetNullableParameter(model.CalibrationSalinity1Max));
cmd.Parameters.AddWithValue("@CalibrationSalinity1Min", Helper.GetNullableParameter(model.CalibrationSalinity1Min));
cmd.Parameters.AddWithValue("@CalibrationSalinity2Max", Helper.GetNullableParameter(model.CalibrationSalinity2Max));
cmd.Parameters.AddWithValue("@CalibrationSalinity2Min", Helper.GetNullableParameter(model.CalibrationSalinity2Min));
cmd.Parameters.AddWithValue("@Location", Helper.GetNullableParameter(model.Location));
cmd.Parameters.AddWithValue("@Id", Helper.GetNullableParameter(model.Id));
cmd.Parameters.AddWithValue("@Status", Helper.GetNullableParameter(model.Status));
cmd.Parameters.AddWithValue("@CompanyId", Helper.GetNullableParameter(model.CompanyId));
            var recs = cmd.ExecuteNonQuery();
            if (recs > 0)
            {  
            transaction.Commit();
                return true;
            }
            return false;
        }

        public long AddDevices(DevicesModel model)
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            SqlTransaction transaction = this.MSSqlDatabase.Connection.BeginTransaction("");
            cmd.Transaction = transaction;
            cmd.CommandText = @"INSERT INTO devices (Name,DevEUI,JoinEUI,AppKey,FieldId,CalibrationMoisture1Max,CalibrationMoisture1Min,CalibrationMoisture2Max,CalibrationMoisture2Min,CalibrationSalinity1Max,CalibrationSalinity1Min,CalibrationSalinity2Max,CalibrationSalinity2Min,Location,Id,Status,CompanyId) VALUES (@Name,@DevEUI,@JoinEUI,@AppKey,@FieldId,@CalibrationMoisture1Max,@CalibrationMoisture1Min,@CalibrationMoisture2Max,@CalibrationMoisture2Min,@CalibrationSalinity1Max,@CalibrationSalinity1Min,@CalibrationSalinity2Max,@CalibrationSalinity2Min,@Location,@Id,@Status,@CompanyId);SELECT SCOPE_IDENTITY();";
            cmd.Parameters.AddWithValue("@Name", Helper.GetNullableParameter(model.Name));
cmd.Parameters.AddWithValue("@DevEUI", Helper.GetNullableParameter(model.DevEUI));
cmd.Parameters.AddWithValue("@JoinEUI", Helper.GetNullableParameter(model.JoinEUI));
cmd.Parameters.AddWithValue("@AppKey", Helper.GetNullableParameter(model.AppKey));
cmd.Parameters.AddWithValue("@FieldId", Helper.GetNullableParameter(model.FieldId));
cmd.Parameters.AddWithValue("@CalibrationMoisture1Max", Helper.GetNullableParameter(model.CalibrationMoisture1Max));
cmd.Parameters.AddWithValue("@CalibrationMoisture1Min", Helper.GetNullableParameter(model.CalibrationMoisture1Min));
cmd.Parameters.AddWithValue("@CalibrationMoisture2Max", Helper.GetNullableParameter(model.CalibrationMoisture2Max));
cmd.Parameters.AddWithValue("@CalibrationMoisture2Min", Helper.GetNullableParameter(model.CalibrationMoisture2Min));
cmd.Parameters.AddWithValue("@CalibrationSalinity1Max", Helper.GetNullableParameter(model.CalibrationSalinity1Max));
cmd.Parameters.AddWithValue("@CalibrationSalinity1Min", Helper.GetNullableParameter(model.CalibrationSalinity1Min));
cmd.Parameters.AddWithValue("@CalibrationSalinity2Max", Helper.GetNullableParameter(model.CalibrationSalinity2Max));
cmd.Parameters.AddWithValue("@CalibrationSalinity2Min", Helper.GetNullableParameter(model.CalibrationSalinity2Min));
cmd.Parameters.AddWithValue("@Location", Helper.GetNullableParameter(model.Location));
cmd.Parameters.AddWithValue("@Id", Helper.GetNullableParameter(model.Id));
cmd.Parameters.AddWithValue("@Status", Helper.GetNullableParameter(model.Status));
cmd.Parameters.AddWithValue("@CompanyId", Helper.GetNullableParameter(model.CompanyId));
              var recs = cmd.ExecuteScalar();
            if (recs!=null)
            {
                transaction.Commit();
                long.TryParse(recs.ToString(), out long result);
                return result>0? result : 1;
            }
            return -1;
          
        }

        public bool DeleteDevices(int id)
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            SqlTransaction transaction = this.MSSqlDatabase.Connection.BeginTransaction("");
            cmd.Transaction = transaction;
            cmd.CommandText = @"DELETE FROM devices Where FieldId=@FieldId";
			cmd.Parameters.AddWithValue("@id", id);
            var recs = cmd.ExecuteNonQuery();
            if (recs > 0)
            { 
                transaction.Commit();
                return true;
            }
            return false;
        }
        public bool DeleteMultipleDevices(List<DeleteMultipleModel> deleteParam,string andOr)
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            SqlTransaction transaction = this.MSSqlDatabase.Connection.BeginTransaction("");
            cmd.Transaction = transaction;
            cmd.CommandText = @"DELETE FROM Devices Where";
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


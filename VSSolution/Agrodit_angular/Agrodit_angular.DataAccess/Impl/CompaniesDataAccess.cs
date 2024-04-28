using Agrodit_angular.DataAccess.Interface;
using Agrodit_angular.Model;
using Agrodit_angular.Utility;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Agrodit_angular.DataAccess.Impl
{
    public class CompaniesDataAccess : ICompaniesDataAccess
    {
        private MSSqlDatabase MSSqlDatabase { get; set; }
        public CompaniesDataAccess(MSSqlDatabase msSqlDatabase)
        {
            MSSqlDatabase = msSqlDatabase;
        }
        public List<CompaniesModel> GetAllCompanies(int page=1,int itemsPerPage=100,List<OrderByModel> orderBy=null)
        {
            var ret = new List<CompaniesModel>();
			int offset = (page - 1) * itemsPerPage;
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT  t.* FROM Companies t  Order by t.Id OFFSET @Offset ROWS FETCH NEXT @ItemsPerPage ROWS ONLY";
             if(orderBy!=null && orderBy.Count > 0)
            {
                cmd.CommandText = Helper.ConverOrderListToSQL(cmd.CommandText,orderBy);
            }
			cmd.Parameters.AddWithValue("@Offset", offset);
            cmd.Parameters.AddWithValue("@ItemsPerPage", itemsPerPage);
            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    var t = new CompaniesModel()
                    {
                       Id= reader.GetValue<Int64>("Id"),
Name= reader.GetValue<String>("Name"),
OwnerId= reader.GetValue<Int64>("OwnerId"),
AppId= reader.GetValue<String>("AppId"),
AppName= reader.GetValue<String>("AppName"),
MaxDevices= reader.GetValue<Int64>("MaxDevices"),
CompanyType= reader.GetValue<String>("CompanyType"),
                    };

                    ret.Add(t);
                }
            return ret;
        }

        public List<CompaniesModel> SearchCompanies(string searchKey, int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy=null)
        {
            var ret = new List<CompaniesModel>();
            int offset = (page - 1) * itemsPerPage;
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT  t.* FROM Companies t  WHERE 1=1 AND  t.Name LIKE CONCAT('%',@SearchKey,'%') OR t.AppId LIKE CONCAT('%',@SearchKey,'%') OR t.AppName LIKE CONCAT('%',@SearchKey,'%') OR t.CompanyType LIKE CONCAT('%',@SearchKey,'%') Order by t.Id OFFSET @Offset ROWS FETCH NEXT @ItemsPerPage ROWS ONLY";
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
                    var t = new CompaniesModel()
                    {
                       Id= reader.GetValue<Int64>("Id"),
Name= reader.GetValue<String>("Name"),
OwnerId= reader.GetValue<Int64>("OwnerId"),
AppId= reader.GetValue<String>("AppId"),
AppName= reader.GetValue<String>("AppName"),
MaxDevices= reader.GetValue<Int64>("MaxDevices"),
CompanyType= reader.GetValue<String>("CompanyType"),
                    };

                    ret.Add(t);
                }
            return ret;
        }

        public  int GetAllTotalRecordCompanies()
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT Count(*) as TotalRecord FROM Companies t";
            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                { 
                    return reader.GetInt32("TotalRecord");
                }
            return 0;
        }
        public int GetSearchTotalRecordCompanies(string searchKey)
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT Count(*) as TotalRecord FROM Companies t  WHERE 1=1 AND  t.Name LIKE CONCAT('%',@SearchKey,'%') OR t.AppId LIKE CONCAT('%',@SearchKey,'%') OR t.AppName LIKE CONCAT('%',@SearchKey,'%') OR t.CompanyType LIKE CONCAT('%',@SearchKey,'%')";
            cmd.Parameters.AddWithValue("@SearchKey", searchKey);
            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                { 
                    return reader.GetInt32("TotalRecord");
                }
            return 0;
        }

		

        public CompaniesModel GetCompaniesByID(int id)
        {

            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT  t.* FROM Companies t  WHERE t.Id= @Id Order by t.Id OFFSET 0 ROWS FETCH NEXT 1 ROWS ONLY";
			cmd.Parameters.AddWithValue("@id", id);

            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    return new CompaniesModel()
                    {
                        Id= reader.GetValue<Int64>("Id"),
Name= reader.GetValue<String>("Name"),
OwnerId= reader.GetValue<Int64>("OwnerId"),
AppId= reader.GetValue<String>("AppId"),
AppName= reader.GetValue<String>("AppName"),
MaxDevices= reader.GetValue<Int64>("MaxDevices"),
CompanyType= reader.GetValue<String>("CompanyType"),
                    };
                }
            return null;
        }

         public List<CompaniesModel> FilterCompanies(List<FilterModel> filterBy,string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy)
        {
            var ret = new List<CompaniesModel>();
            int offset = (page - 1) * itemsPerPage;
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT  t.* FROM Companies t  {filterColumns} Order by t.Id OFFSET @Offset ROWS FETCH NEXT @ItemsPerPage ROWS ONLY";
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
                var t = new CompaniesModel()
                    {
                       Id= reader.GetValue<Int64>("Id"),
Name= reader.GetValue<String>("Name"),
OwnerId= reader.GetValue<Int64>("OwnerId"),
AppId= reader.GetValue<String>("AppId"),
AppName= reader.GetValue<String>("AppName"),
MaxDevices= reader.GetValue<Int64>("MaxDevices"),
CompanyType= reader.GetValue<String>("CompanyType"),
                    };

                    ret.Add(t);
                }
            return ret;
        }

       public int GetFilterTotalRecordCompanies(List<FilterModel> filterBy,string andOr)
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT Count(*) as TotalRecord FROM Companies t {filterColumns}";
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

        public bool UpdateCompanies(CompaniesModel model)
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            SqlTransaction transaction = this.MSSqlDatabase.Connection.BeginTransaction("");
            cmd.Transaction = transaction;
            cmd.CommandText = @"UPDATE Companies SET Id=@Id,Name=@Name,OwnerId=@OwnerId,AppId=@AppId,AppName=@AppName,MaxDevices=@MaxDevices,CompanyType=@CompanyType WHERE Id = @Id;";
            cmd.Parameters.AddWithValue("@Id", Helper.GetNullableParameter(model.Id));
cmd.Parameters.AddWithValue("@Name", Helper.GetNullableParameter(model.Name));
cmd.Parameters.AddWithValue("@OwnerId", Helper.GetNullableParameter(model.OwnerId));
cmd.Parameters.AddWithValue("@AppId", Helper.GetNullableParameter(model.AppId));
cmd.Parameters.AddWithValue("@AppName", Helper.GetNullableParameter(model.AppName));
cmd.Parameters.AddWithValue("@MaxDevices", Helper.GetNullableParameter(model.MaxDevices));
cmd.Parameters.AddWithValue("@CompanyType", Helper.GetNullableParameter(model.CompanyType));
            var recs = cmd.ExecuteNonQuery();
            if (recs > 0)
            {  
            transaction.Commit();
                return true;
            }
            return false;
        }

        public long AddCompanies(CompaniesModel model)
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            SqlTransaction transaction = this.MSSqlDatabase.Connection.BeginTransaction("");
            cmd.Transaction = transaction;
            cmd.CommandText = @"INSERT INTO Companies (Id,Name,OwnerId,AppId,AppName,MaxDevices,CompanyType) VALUES (@Id,@Name,@OwnerId,@AppId,@AppName,@MaxDevices,@CompanyType);SELECT SCOPE_IDENTITY();";
            cmd.Parameters.AddWithValue("@Id", Helper.GetNullableParameter(model.Id));
cmd.Parameters.AddWithValue("@Name", Helper.GetNullableParameter(model.Name));
cmd.Parameters.AddWithValue("@OwnerId", Helper.GetNullableParameter(model.OwnerId));
cmd.Parameters.AddWithValue("@AppId", Helper.GetNullableParameter(model.AppId));
cmd.Parameters.AddWithValue("@AppName", Helper.GetNullableParameter(model.AppName));
cmd.Parameters.AddWithValue("@MaxDevices", Helper.GetNullableParameter(model.MaxDevices));
cmd.Parameters.AddWithValue("@CompanyType", Helper.GetNullableParameter(model.CompanyType));
              var recs = cmd.ExecuteScalar();
            if (recs!=null)
            {
                transaction.Commit();
                long.TryParse(recs.ToString(), out long result);
                return result>0? result : 1;
            }
            return -1;
          
        }

        public bool DeleteCompanies(int id)
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            SqlTransaction transaction = this.MSSqlDatabase.Connection.BeginTransaction("");
            cmd.Transaction = transaction;
            cmd.CommandText = @"DELETE FROM Companies Where Id=@Id";
			cmd.Parameters.AddWithValue("@id", id);
            var recs = cmd.ExecuteNonQuery();
            if (recs > 0)
            { 
                transaction.Commit();
                return true;
            }
            return false;
        }
        public bool DeleteMultipleCompanies(List<DeleteMultipleModel> deleteParam,string andOr)
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            SqlTransaction transaction = this.MSSqlDatabase.Connection.BeginTransaction("");
            cmd.Transaction = transaction;
            cmd.CommandText = @"DELETE FROM Companies Where";
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


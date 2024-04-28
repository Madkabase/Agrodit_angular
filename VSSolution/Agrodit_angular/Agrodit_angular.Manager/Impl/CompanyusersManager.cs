using Agrodit_angular.DataAccess.Interface;
using Agrodit_angular.Manager.Interface;
using Agrodit_angular.Model;
using Agrodit_angular.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agrodit_angular.Manager.Impl
{
    public class CompanyusersManager : ICompanyusersManager
    {
        private readonly ICompanyusersDataAccess DataAccess = null;
        public CompanyusersManager(ICompanyusersDataAccess dataAccess)
        {
            DataAccess = dataAccess;
        }

        public APIResponse GetCompanyusers(int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy=null)
        {
            var result = DataAccess.GetAllCompanyusers(page,itemsPerPage,orderBy);
            if (result != null && result.Count > 0)
            {   
                var totalRecords = DataAccess.GetAllTotalRecordCompanyusers();
                var response = new { records = result, pageNumber = page, pageSize = itemsPerPage, totalRecords = totalRecords };
                return new APIResponse(ResponseCode.SUCCESS, "Record Found", response);
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "No Record Found");
            }
        }

        public APIResponse SearchCompanyusers(string searchKey,int page=1,int itemsPerPage=100,List<OrderByModel> orderBy=null)
        {
            var result = DataAccess.SearchCompanyusers(searchKey,page,itemsPerPage,orderBy);
            if (result != null && result.Count > 0)
            { 
                var totalRecords = DataAccess.GetSearchTotalRecordCompanyusers(searchKey);
                var response = new { records = result, pageNumber = page, pageSize = itemsPerPage, totalRecords = totalRecords };
                return new APIResponse(ResponseCode.SUCCESS, "Record Found", response);
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "No Record Found");
            }
        }

        public APIResponse GetCompanyusersByID(int id)
        {
            var result = DataAccess.GetCompanyusersByID(id);
            if (result != null)
            {
                return new APIResponse(ResponseCode.SUCCESS, "Record Found", result);
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "No Record Found");
            }
        }

        public APIResponse UpdateCompanyusers(int id, CompanyusersModel model)
        {
			model.Id= id;
           
            var result = DataAccess.UpdateCompanyusers(model);
            if (result)
            {
                return new APIResponse(ResponseCode.SUCCESS, "Record Updated");
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "Record Not Updated");
            }
        }

        public APIResponse AddCompanyusers(CompanyusersModel model)
        {
            var result = DataAccess.AddCompanyusers(model);
            if (result > 0)
            {
                return new APIResponse(ResponseCode.SUCCESS, "Record Created", result);
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "Record Not Created");
            }
        }

        

		public APIResponse DeleteCompanyusers(int id)
        {
            var result = DataAccess.DeleteCompanyusers(id);
            if (result)
            {
                return new APIResponse(ResponseCode.SUCCESS, "Record Deleted");
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "Record Not Deleted");
            }
        }

        public APIResponse DeleteMultipleCompanyusers(List<DeleteMultipleModel> deleteParam,string andOr)
        {
            var result = DataAccess.DeleteMultipleCompanyusers(deleteParam,andOr);
            if (result)
            {
                return new APIResponse(ResponseCode.SUCCESS, "Record Deleted");
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "Record Not Deleted");
            }
        }

        public APIResponse FilterCompanyusers(List<FilterModel> filterModels, string andOr, int page = 1, int itemsPerPage = 100, List<OrderByModel> orderBy = null)
        {
            var result = DataAccess.FilterCompanyusers(filterModels, andOr,page, itemsPerPage, orderBy);
            if (result != null && result.Count > 0)
            {
                var totalRecords = DataAccess.GetFilterTotalRecordCompanyusers(filterModels,andOr);
                var response = new { records = result, pageNumber = page, pageSize = itemsPerPage, totalRecords = totalRecords };
                return new APIResponse(ResponseCode.SUCCESS, "Record Found", response);
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "No Record Found");
            }
        }
    }
}


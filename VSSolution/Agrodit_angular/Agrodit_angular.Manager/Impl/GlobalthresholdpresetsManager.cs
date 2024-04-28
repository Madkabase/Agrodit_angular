using Agrodit_angular.DataAccess.Interface;
using Agrodit_angular.Manager.Interface;
using Agrodit_angular.Model;
using Agrodit_angular.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agrodit_angular.Manager.Impl
{
    public class GlobalthresholdpresetsManager : IGlobalthresholdpresetsManager
    {
        private readonly IGlobalthresholdpresetsDataAccess DataAccess = null;
        public GlobalthresholdpresetsManager(IGlobalthresholdpresetsDataAccess dataAccess)
        {
            DataAccess = dataAccess;
        }

        public APIResponse GetGlobalthresholdpresets(int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy=null)
        {
            var result = DataAccess.GetAllGlobalthresholdpresets(page,itemsPerPage,orderBy);
            if (result != null && result.Count > 0)
            {   
                var totalRecords = DataAccess.GetAllTotalRecordGlobalthresholdpresets();
                var response = new { records = result, pageNumber = page, pageSize = itemsPerPage, totalRecords = totalRecords };
                return new APIResponse(ResponseCode.SUCCESS, "Record Found", response);
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "No Record Found");
            }
        }

        public APIResponse SearchGlobalthresholdpresets(string searchKey,int page=1,int itemsPerPage=100,List<OrderByModel> orderBy=null)
        {
            var result = DataAccess.SearchGlobalthresholdpresets(searchKey,page,itemsPerPage,orderBy);
            if (result != null && result.Count > 0)
            { 
                var totalRecords = DataAccess.GetSearchTotalRecordGlobalthresholdpresets(searchKey);
                var response = new { records = result, pageNumber = page, pageSize = itemsPerPage, totalRecords = totalRecords };
                return new APIResponse(ResponseCode.SUCCESS, "Record Found", response);
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "No Record Found");
            }
        }

        public APIResponse GetGlobalthresholdpresetsByID(int id)
        {
            var result = DataAccess.GetGlobalthresholdpresetsByID(id);
            if (result != null)
            {
                return new APIResponse(ResponseCode.SUCCESS, "Record Found", result);
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "No Record Found");
            }
        }

        public APIResponse UpdateGlobalthresholdpresets(int id, GlobalthresholdpresetsModel model)
        {
			model.Id= id;
           
            var result = DataAccess.UpdateGlobalthresholdpresets(model);
            if (result)
            {
                return new APIResponse(ResponseCode.SUCCESS, "Record Updated");
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "Record Not Updated");
            }
        }

        public APIResponse AddGlobalthresholdpresets(GlobalthresholdpresetsModel model)
        {
            var result = DataAccess.AddGlobalthresholdpresets(model);
            if (result > 0)
            {
                return new APIResponse(ResponseCode.SUCCESS, "Record Created", result);
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "Record Not Created");
            }
        }

        

		public APIResponse DeleteGlobalthresholdpresets(int id)
        {
            var result = DataAccess.DeleteGlobalthresholdpresets(id);
            if (result)
            {
                return new APIResponse(ResponseCode.SUCCESS, "Record Deleted");
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "Record Not Deleted");
            }
        }

        public APIResponse DeleteMultipleGlobalthresholdpresets(List<DeleteMultipleModel> deleteParam,string andOr)
        {
            var result = DataAccess.DeleteMultipleGlobalthresholdpresets(deleteParam,andOr);
            if (result)
            {
                return new APIResponse(ResponseCode.SUCCESS, "Record Deleted");
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "Record Not Deleted");
            }
        }

        public APIResponse FilterGlobalthresholdpresets(List<FilterModel> filterModels, string andOr, int page = 1, int itemsPerPage = 100, List<OrderByModel> orderBy = null)
        {
            var result = DataAccess.FilterGlobalthresholdpresets(filterModels, andOr,page, itemsPerPage, orderBy);
            if (result != null && result.Count > 0)
            {
                var totalRecords = DataAccess.GetFilterTotalRecordGlobalthresholdpresets(filterModels,andOr);
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


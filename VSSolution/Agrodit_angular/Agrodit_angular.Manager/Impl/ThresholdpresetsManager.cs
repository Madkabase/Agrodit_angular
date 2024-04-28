using Agrodit_angular.DataAccess.Interface;
using Agrodit_angular.Manager.Interface;
using Agrodit_angular.Model;
using Agrodit_angular.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agrodit_angular.Manager.Impl
{
    public class ThresholdpresetsManager : IThresholdpresetsManager
    {
        private readonly IThresholdpresetsDataAccess DataAccess = null;
        public ThresholdpresetsManager(IThresholdpresetsDataAccess dataAccess)
        {
            DataAccess = dataAccess;
        }

        public APIResponse GetThresholdpresets(int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy=null)
        {
            var result = DataAccess.GetAllThresholdpresets(page,itemsPerPage,orderBy);
            if (result != null && result.Count > 0)
            {   
                var totalRecords = DataAccess.GetAllTotalRecordThresholdpresets();
                var response = new { records = result, pageNumber = page, pageSize = itemsPerPage, totalRecords = totalRecords };
                return new APIResponse(ResponseCode.SUCCESS, "Record Found", response);
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "No Record Found");
            }
        }

        public APIResponse SearchThresholdpresets(string searchKey,int page=1,int itemsPerPage=100,List<OrderByModel> orderBy=null)
        {
            var result = DataAccess.SearchThresholdpresets(searchKey,page,itemsPerPage,orderBy);
            if (result != null && result.Count > 0)
            { 
                var totalRecords = DataAccess.GetSearchTotalRecordThresholdpresets(searchKey);
                var response = new { records = result, pageNumber = page, pageSize = itemsPerPage, totalRecords = totalRecords };
                return new APIResponse(ResponseCode.SUCCESS, "Record Found", response);
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "No Record Found");
            }
        }

        public APIResponse GetThresholdpresetsByID(int id)
        {
            var result = DataAccess.GetThresholdpresetsByID(id);
            if (result != null)
            {
                return new APIResponse(ResponseCode.SUCCESS, "Record Found", result);
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "No Record Found");
            }
        }

        public APIResponse UpdateThresholdpresets(int id, ThresholdpresetsModel model)
        {
			model.Id= id;
           
            var result = DataAccess.UpdateThresholdpresets(model);
            if (result)
            {
                return new APIResponse(ResponseCode.SUCCESS, "Record Updated");
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "Record Not Updated");
            }
        }

        public APIResponse AddThresholdpresets(ThresholdpresetsModel model)
        {
            var result = DataAccess.AddThresholdpresets(model);
            if (result > 0)
            {
                return new APIResponse(ResponseCode.SUCCESS, "Record Created", result);
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "Record Not Created");
            }
        }

        

		public APIResponse DeleteThresholdpresets(int id)
        {
            var result = DataAccess.DeleteThresholdpresets(id);
            if (result)
            {
                return new APIResponse(ResponseCode.SUCCESS, "Record Deleted");
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "Record Not Deleted");
            }
        }

        public APIResponse DeleteMultipleThresholdpresets(List<DeleteMultipleModel> deleteParam,string andOr)
        {
            var result = DataAccess.DeleteMultipleThresholdpresets(deleteParam,andOr);
            if (result)
            {
                return new APIResponse(ResponseCode.SUCCESS, "Record Deleted");
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "Record Not Deleted");
            }
        }

        public APIResponse FilterThresholdpresets(List<FilterModel> filterModels, string andOr, int page = 1, int itemsPerPage = 100, List<OrderByModel> orderBy = null)
        {
            var result = DataAccess.FilterThresholdpresets(filterModels, andOr,page, itemsPerPage, orderBy);
            if (result != null && result.Count > 0)
            {
                var totalRecords = DataAccess.GetFilterTotalRecordThresholdpresets(filterModels,andOr);
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


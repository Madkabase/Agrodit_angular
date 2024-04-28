using Agrodit_angular.DataAccess.Interface;
using Agrodit_angular.Manager.Interface;
using Agrodit_angular.Model;
using Agrodit_angular.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agrodit_angular.Manager.Impl
{
    public class Spatial_Ref_SysManager : ISpatial_Ref_SysManager
    {
        private readonly ISpatial_Ref_SysDataAccess DataAccess = null;
        public Spatial_Ref_SysManager(ISpatial_Ref_SysDataAccess dataAccess)
        {
            DataAccess = dataAccess;
        }

        public APIResponse GetSpatial_Ref_Sys(int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy=null)
        {
            var result = DataAccess.GetAllSpatial_Ref_Sys(page,itemsPerPage,orderBy);
            if (result != null && result.Count > 0)
            {   
                var totalRecords = DataAccess.GetAllTotalRecordSpatial_Ref_Sys();
                var response = new { records = result, pageNumber = page, pageSize = itemsPerPage, totalRecords = totalRecords };
                return new APIResponse(ResponseCode.SUCCESS, "Record Found", response);
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "No Record Found");
            }
        }

        public APIResponse SearchSpatial_Ref_Sys(string searchKey,int page=1,int itemsPerPage=100,List<OrderByModel> orderBy=null)
        {
            var result = DataAccess.SearchSpatial_Ref_Sys(searchKey,page,itemsPerPage,orderBy);
            if (result != null && result.Count > 0)
            { 
                var totalRecords = DataAccess.GetSearchTotalRecordSpatial_Ref_Sys(searchKey);
                var response = new { records = result, pageNumber = page, pageSize = itemsPerPage, totalRecords = totalRecords };
                return new APIResponse(ResponseCode.SUCCESS, "Record Found", response);
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "No Record Found");
            }
        }

        public APIResponse GetSpatial_Ref_SysByID(int id)
        {
            var result = DataAccess.GetSpatial_Ref_SysByID(id);
            if (result != null)
            {
                return new APIResponse(ResponseCode.SUCCESS, "Record Found", result);
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "No Record Found");
            }
        }

        public APIResponse UpdateSpatial_Ref_Sys(int id, Spatial_Ref_SysModel model)
        {
			model.srid= id;
           
            var result = DataAccess.UpdateSpatial_Ref_Sys(model);
            if (result)
            {
                return new APIResponse(ResponseCode.SUCCESS, "Record Updated");
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "Record Not Updated");
            }
        }

        public APIResponse AddSpatial_Ref_Sys(Spatial_Ref_SysModel model)
        {
            var result = DataAccess.AddSpatial_Ref_Sys(model);
            if (result > 0)
            {
                return new APIResponse(ResponseCode.SUCCESS, "Record Created", result);
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "Record Not Created");
            }
        }

        

		public APIResponse DeleteSpatial_Ref_Sys(int id)
        {
            var result = DataAccess.DeleteSpatial_Ref_Sys(id);
            if (result)
            {
                return new APIResponse(ResponseCode.SUCCESS, "Record Deleted");
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "Record Not Deleted");
            }
        }

        public APIResponse DeleteMultipleSpatial_Ref_Sys(List<DeleteMultipleModel> deleteParam,string andOr)
        {
            var result = DataAccess.DeleteMultipleSpatial_Ref_Sys(deleteParam,andOr);
            if (result)
            {
                return new APIResponse(ResponseCode.SUCCESS, "Record Deleted");
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "Record Not Deleted");
            }
        }

        public APIResponse FilterSpatial_Ref_Sys(List<FilterModel> filterModels, string andOr, int page = 1, int itemsPerPage = 100, List<OrderByModel> orderBy = null)
        {
            var result = DataAccess.FilterSpatial_Ref_Sys(filterModels, andOr,page, itemsPerPage, orderBy);
            if (result != null && result.Count > 0)
            {
                var totalRecords = DataAccess.GetFilterTotalRecordSpatial_Ref_Sys(filterModels,andOr);
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


using Agrodit_angular.Model;
using Agrodit_angular.Utility;
using System;
using System.Collections.Generic;

namespace Agrodit_angular.DataAccess.Interface
{
    public interface ISpatial_Ref_SysDataAccess
    {
        List<Spatial_Ref_SysModel> GetAllSpatial_Ref_Sys(int page, int itemsPerPage,List<OrderByModel> orderBy);
        List<Spatial_Ref_SysModel> SearchSpatial_Ref_Sys(string searchKey,int page, int itemsPerPage,List<OrderByModel> orderBy);
        List<Spatial_Ref_SysModel> FilterSpatial_Ref_Sys(List<FilterModel> filterModels,string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy);
        Spatial_Ref_SysModel GetSpatial_Ref_SysByID(int id);
        bool UpdateSpatial_Ref_Sys(Spatial_Ref_SysModel model);
        int GetAllTotalRecordSpatial_Ref_Sys();
        int GetSearchTotalRecordSpatial_Ref_Sys(string searchKey);
        int GetFilterTotalRecordSpatial_Ref_Sys(List<FilterModel> filterBy,string andOr);
        long AddSpatial_Ref_Sys(Spatial_Ref_SysModel model);
        bool DeleteSpatial_Ref_Sys(int id);
        bool DeleteMultipleSpatial_Ref_Sys(List<DeleteMultipleModel> deleteParam,string andOr);
        
        
    }
}


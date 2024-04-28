using Agrodit_angular.Model;
using Agrodit_angular.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agrodit_angular.Manager.Interface
{
    public interface ISpatial_Ref_SysManager
    {
        APIResponse GetSpatial_Ref_Sys(int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse SearchSpatial_Ref_Sys(string searchKey, int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse FilterSpatial_Ref_Sys(List<FilterModel> filterModels, string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy);
        APIResponse GetSpatial_Ref_SysByID(int id);
        APIResponse UpdateSpatial_Ref_Sys(int id,Spatial_Ref_SysModel model);
        APIResponse AddSpatial_Ref_Sys(Spatial_Ref_SysModel model);
		APIResponse DeleteSpatial_Ref_Sys(int id);
        APIResponse DeleteMultipleSpatial_Ref_Sys(List<DeleteMultipleModel> deleteParam,string andOr);
    }
}


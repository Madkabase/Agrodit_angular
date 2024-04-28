using Agrodit_angular.Model;
using Agrodit_angular.Utility;
using System;
using System.Collections.Generic;

namespace Agrodit_angular.DataAccess.Interface
{
    public interface IGlobalthresholdpresetsDataAccess
    {
        List<GlobalthresholdpresetsModel> GetAllGlobalthresholdpresets(int page, int itemsPerPage,List<OrderByModel> orderBy);
        List<GlobalthresholdpresetsModel> SearchGlobalthresholdpresets(string searchKey,int page, int itemsPerPage,List<OrderByModel> orderBy);
        List<GlobalthresholdpresetsModel> FilterGlobalthresholdpresets(List<FilterModel> filterModels,string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy);
        GlobalthresholdpresetsModel GetGlobalthresholdpresetsByID(int id);
        bool UpdateGlobalthresholdpresets(GlobalthresholdpresetsModel model);
        int GetAllTotalRecordGlobalthresholdpresets();
        int GetSearchTotalRecordGlobalthresholdpresets(string searchKey);
        int GetFilterTotalRecordGlobalthresholdpresets(List<FilterModel> filterBy,string andOr);
        long AddGlobalthresholdpresets(GlobalthresholdpresetsModel model);
        bool DeleteGlobalthresholdpresets(int id);
        bool DeleteMultipleGlobalthresholdpresets(List<DeleteMultipleModel> deleteParam,string andOr);
        
        
    }
}


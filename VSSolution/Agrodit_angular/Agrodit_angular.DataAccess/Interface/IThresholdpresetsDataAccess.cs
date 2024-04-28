using Agrodit_angular.Model;
using Agrodit_angular.Utility;
using System;
using System.Collections.Generic;

namespace Agrodit_angular.DataAccess.Interface
{
    public interface IThresholdpresetsDataAccess
    {
        List<ThresholdpresetsModel> GetAllThresholdpresets(int page, int itemsPerPage,List<OrderByModel> orderBy);
        List<ThresholdpresetsModel> SearchThresholdpresets(string searchKey,int page, int itemsPerPage,List<OrderByModel> orderBy);
        List<ThresholdpresetsModel> FilterThresholdpresets(List<FilterModel> filterModels,string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy);
        ThresholdpresetsModel GetThresholdpresetsByID(int id);
        bool UpdateThresholdpresets(ThresholdpresetsModel model);
        int GetAllTotalRecordThresholdpresets();
        int GetSearchTotalRecordThresholdpresets(string searchKey);
        int GetFilterTotalRecordThresholdpresets(List<FilterModel> filterBy,string andOr);
        long AddThresholdpresets(ThresholdpresetsModel model);
        bool DeleteThresholdpresets(int id);
        bool DeleteMultipleThresholdpresets(List<DeleteMultipleModel> deleteParam,string andOr);
        
        
    }
}


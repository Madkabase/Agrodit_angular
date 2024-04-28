using Agrodit_angular.Model;
using Agrodit_angular.Utility;
using System;
using System.Collections.Generic;

namespace Agrodit_angular.DataAccess.Interface
{
    public interface IThresholdsDataAccess
    {
        List<ThresholdsModel> GetAllThresholds(int page, int itemsPerPage,List<OrderByModel> orderBy);
        List<ThresholdsModel> SearchThresholds(string searchKey,int page, int itemsPerPage,List<OrderByModel> orderBy);
        List<ThresholdsModel> FilterThresholds(List<FilterModel> filterModels,string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy);
        ThresholdsModel GetThresholdsByID(int id);
        bool UpdateThresholds(ThresholdsModel model);
        int GetAllTotalRecordThresholds();
        int GetSearchTotalRecordThresholds(string searchKey);
        int GetFilterTotalRecordThresholds(List<FilterModel> filterBy,string andOr);
        long AddThresholds(ThresholdsModel model);
        bool DeleteThresholds(int id);
        bool DeleteMultipleThresholds(List<DeleteMultipleModel> deleteParam,string andOr);
        
        
    }
}


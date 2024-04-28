using Agrodit_angular.Model;
using Agrodit_angular.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agrodit_angular.Manager.Interface
{
    public interface IThresholdsManager
    {
        APIResponse GetThresholds(int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse SearchThresholds(string searchKey, int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse FilterThresholds(List<FilterModel> filterModels, string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy);
        APIResponse GetThresholdsByID(int id);
        APIResponse UpdateThresholds(int id,ThresholdsModel model);
        APIResponse AddThresholds(ThresholdsModel model);
		APIResponse DeleteThresholds(int id);
        APIResponse DeleteMultipleThresholds(List<DeleteMultipleModel> deleteParam,string andOr);
    }
}


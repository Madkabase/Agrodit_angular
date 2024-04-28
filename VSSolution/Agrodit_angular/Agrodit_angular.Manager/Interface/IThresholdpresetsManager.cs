using Agrodit_angular.Model;
using Agrodit_angular.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agrodit_angular.Manager.Interface
{
    public interface IThresholdpresetsManager
    {
        APIResponse GetThresholdpresets(int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse SearchThresholdpresets(string searchKey, int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse FilterThresholdpresets(List<FilterModel> filterModels, string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy);
        APIResponse GetThresholdpresetsByID(int id);
        APIResponse UpdateThresholdpresets(int id,ThresholdpresetsModel model);
        APIResponse AddThresholdpresets(ThresholdpresetsModel model);
		APIResponse DeleteThresholdpresets(int id);
        APIResponse DeleteMultipleThresholdpresets(List<DeleteMultipleModel> deleteParam,string andOr);
    }
}


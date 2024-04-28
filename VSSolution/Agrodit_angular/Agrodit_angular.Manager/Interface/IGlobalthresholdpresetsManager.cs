using Agrodit_angular.Model;
using Agrodit_angular.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agrodit_angular.Manager.Interface
{
    public interface IGlobalthresholdpresetsManager
    {
        APIResponse GetGlobalthresholdpresets(int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse SearchGlobalthresholdpresets(string searchKey, int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse FilterGlobalthresholdpresets(List<FilterModel> filterModels, string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy);
        APIResponse GetGlobalthresholdpresetsByID(int id);
        APIResponse UpdateGlobalthresholdpresets(int id,GlobalthresholdpresetsModel model);
        APIResponse AddGlobalthresholdpresets(GlobalthresholdpresetsModel model);
		APIResponse DeleteGlobalthresholdpresets(int id);
        APIResponse DeleteMultipleGlobalthresholdpresets(List<DeleteMultipleModel> deleteParam,string andOr);
    }
}


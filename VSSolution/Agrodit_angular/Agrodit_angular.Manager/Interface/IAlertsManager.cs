using Agrodit_angular.Model;
using Agrodit_angular.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agrodit_angular.Manager.Interface
{
    public interface IAlertsManager
    {
        APIResponse GetAlerts(int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse SearchAlerts(string searchKey, int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse FilterAlerts(List<FilterModel> filterModels, string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy);
        APIResponse GetAlertsByID(int id);
        APIResponse UpdateAlerts(int id,AlertsModel model);
        APIResponse AddAlerts(AlertsModel model);
		APIResponse DeleteAlerts(int id);
        APIResponse DeleteMultipleAlerts(List<DeleteMultipleModel> deleteParam,string andOr);
    }
}


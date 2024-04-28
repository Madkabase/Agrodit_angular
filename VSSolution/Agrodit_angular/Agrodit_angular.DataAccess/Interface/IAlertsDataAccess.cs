using Agrodit_angular.Model;
using Agrodit_angular.Utility;
using System;
using System.Collections.Generic;

namespace Agrodit_angular.DataAccess.Interface
{
    public interface IAlertsDataAccess
    {
        List<AlertsModel> GetAllAlerts(int page, int itemsPerPage,List<OrderByModel> orderBy);
        List<AlertsModel> SearchAlerts(string searchKey,int page, int itemsPerPage,List<OrderByModel> orderBy);
        List<AlertsModel> FilterAlerts(List<FilterModel> filterModels,string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy);
        AlertsModel GetAlertsByID(int id);
        bool UpdateAlerts(AlertsModel model);
        int GetAllTotalRecordAlerts();
        int GetSearchTotalRecordAlerts(string searchKey);
        int GetFilterTotalRecordAlerts(List<FilterModel> filterBy,string andOr);
        long AddAlerts(AlertsModel model);
        bool DeleteAlerts(int id);
        bool DeleteMultipleAlerts(List<DeleteMultipleModel> deleteParam,string andOr);
        
        
    }
}


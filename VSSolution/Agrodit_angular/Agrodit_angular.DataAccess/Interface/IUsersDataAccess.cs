using Agrodit_angular.Model;
using Agrodit_angular.Utility;
using System;
using System.Collections.Generic;

namespace Agrodit_angular.DataAccess.Interface
{
    public interface IUsersDataAccess
    {
        List<UsersModel> GetAllUsers(int page, int itemsPerPage,List<OrderByModel> orderBy);
        List<UsersModel> SearchUsers(string searchKey,int page, int itemsPerPage,List<OrderByModel> orderBy);
        List<UsersModel> FilterUsers(List<FilterModel> filterModels,string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy);
        UsersModel GetUsersByID(int id);
        bool UpdateUsers(UsersModel model);
        int GetAllTotalRecordUsers();
        int GetSearchTotalRecordUsers(string searchKey);
        int GetFilterTotalRecordUsers(List<FilterModel> filterBy,string andOr);
        long AddUsers(UsersModel model);
        bool DeleteUsers(int id);
        bool DeleteMultipleUsers(List<DeleteMultipleModel> deleteParam,string andOr);
        
        
    }
}


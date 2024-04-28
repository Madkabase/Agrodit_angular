using Agrodit_angular.Model;
using Agrodit_angular.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agrodit_angular.Manager.Interface
{
    public interface IUsersManager
    {
        APIResponse GetUsers(int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse SearchUsers(string searchKey, int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse FilterUsers(List<FilterModel> filterModels, string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy);
        APIResponse GetUsersByID(int id);
        APIResponse UpdateUsers(int id,UsersModel model);
        APIResponse AddUsers(UsersModel model);
		APIResponse DeleteUsers(int id);
        APIResponse DeleteMultipleUsers(List<DeleteMultipleModel> deleteParam,string andOr);
    }
}


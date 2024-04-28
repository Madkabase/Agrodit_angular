using Agrodit_angular.Model;
using Agrodit_angular.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agrodit_angular.Manager.Interface
{
    public interface ICompanyusersManager
    {
        APIResponse GetCompanyusers(int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse SearchCompanyusers(string searchKey, int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse FilterCompanyusers(List<FilterModel> filterModels, string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy);
        APIResponse GetCompanyusersByID(int id);
        APIResponse UpdateCompanyusers(int id,CompanyusersModel model);
        APIResponse AddCompanyusers(CompanyusersModel model);
		APIResponse DeleteCompanyusers(int id);
        APIResponse DeleteMultipleCompanyusers(List<DeleteMultipleModel> deleteParam,string andOr);
    }
}


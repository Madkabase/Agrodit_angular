using Agrodit_angular.Model;
using Agrodit_angular.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agrodit_angular.Manager.Interface
{
    public interface ICompaniesManager
    {
        APIResponse GetCompanies(int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse SearchCompanies(string searchKey, int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse FilterCompanies(List<FilterModel> filterModels, string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy);
        APIResponse GetCompaniesByID(int id);
        APIResponse UpdateCompanies(int id,CompaniesModel model);
        APIResponse AddCompanies(CompaniesModel model);
		APIResponse DeleteCompanies(int id);
        APIResponse DeleteMultipleCompanies(List<DeleteMultipleModel> deleteParam,string andOr);
    }
}


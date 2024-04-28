using Microsoft.AspNetCore.Http;
using Agrodit_angular.Model;
using Agrodit_angular.Utility;
using System.Collections.Generic;

namespace Agrodit_angular.Manager.Interface
{
    public interface IUploadManager
    {
        APIResponse UploadImages(List<IFormFile> images);
    }
}


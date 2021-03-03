namespace StudentsSocialMedia.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public interface ICategoriesService
    {
        IEnumerable<T> GetAll<T>();

        IEnumerable<SelectListItem> GetAllAsSelectListItems();
    }
}

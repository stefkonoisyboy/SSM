﻿namespace StudentsSocialMedia.Web.ViewModels.Images
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using StudentsSocialMedia.Data.Models;
    using StudentsSocialMedia.Services.Mapping;

    public class AllImagesByIdViewModel : IMapFrom<Image>
    {
        public string Id { get; set; }

        public string Extension { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}

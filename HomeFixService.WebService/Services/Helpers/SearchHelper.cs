﻿using HomeFixService.WebService.Models.EntityFramework;
using HomeFixService.WebService.Models.Enums;
using System.Collections.Generic;

namespace HomeFixService.WebService.Services.Helpers
{
    interface SearchHelper
    {
        List<Users> GetUsersBySearchCriteria(
            string searchTerm,
            float currentLatitude,
            float currentLongitude,
            int pageSize,
            int pageNumber
        );

        List<Users> GetUsersByProfessionCriteria(
            ProfessionsEnum profession,
            string searchTerm,
            float currentLatitude,
            float currentLongitude,
            int pageSize,
            int pageNumber
        );

        List<Users> GetUsersByCountryCityCriteria(
            string countryName,
            string cityName,
            string searchTerm,
            int pageSize,
            int pageNumber
        );

        List<string> GetCountryList(
            string searchTerm
        );

        List<string> GetCityList(
            string countryName,
            string searchTerm
        );
    }
}

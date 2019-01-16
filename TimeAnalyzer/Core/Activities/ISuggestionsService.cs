using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeAnalyzer.Domain.Models;

namespace TimeAnalyzer.Core.Activities
{
    public interface ISuggestionsService
    {
        Task<Dictionary<ActivityType, List<Activity>>> GetSuggestions(string userName, string date);
    }
}

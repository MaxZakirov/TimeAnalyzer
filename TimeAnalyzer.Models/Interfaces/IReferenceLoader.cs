using System.Collections.Generic;
using System.Threading.Tasks;
using TimeAnalyzer.Domain.Models;

namespace TimeAnalyzer.Domain.Interfaces
{
    public interface IReferenceLoader
    {
        Task LoadForTimeReports(IEnumerable<TimeReport> timeReports);
    }
}

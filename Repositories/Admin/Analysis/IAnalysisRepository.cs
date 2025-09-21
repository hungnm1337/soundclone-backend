using Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Admin.Analysis
{
    public interface IAnalysisRepository
    {
        public Task<List<MonthlyCount>> GetUserRegistrationCountByMonth();

        public Task<List<MonthlyCount>> GetTrackUploadCountByMonth();

        public Task<List<MonthlyCount>> GetInvoiceCountByMonth();

        public Task<List<MonthlyAmount>> GetTotalInvoiceAmountByMonth();


    }
}

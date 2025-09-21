using Data.DTOs;
using Repositories.Admin.Analysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Admin.Analysis
{
    public class AnalysisService : IAnalysisService
    {
        private readonly IAnalysisRepository _analysisRepository;
        public AnalysisService(IAnalysisRepository analysisRepository) 
        {
            _analysisRepository = analysisRepository;
        }
        public async Task<List<MonthlyCount>> GetInvoiceCountByMonth()
        {
            return await _analysisRepository.GetInvoiceCountByMonth();
        }

        public async Task<List<MonthlyAmount>> GetTotalInvoiceAmountByMonth()
        {
            return await _analysisRepository.GetTotalInvoiceAmountByMonth();
        }

        public async Task<List<MonthlyCount>> GetTrackUploadCountByMonth()
        {
            return await _analysisRepository.GetTrackUploadCountByMonth();
        }

        public async Task<List<MonthlyCount>> GetUserRegistrationCountByMonth()
        {
            return await _analysisRepository.GetUserRegistrationCountByMonth();
        }
    }
}

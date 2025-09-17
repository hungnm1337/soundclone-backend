using Data.DTOs;
using Repositories.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Report
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;

        public ReportService(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }
        public async Task<bool> CreateSystemReport(SystemReportDTO reportSystemDTO)
        {
            return await _reportRepository.CreateSystemReport(reportSystemDTO);
        }

        public async Task<List<SystemReportDTO>> GetAllSystemReports()
        {
            return await _reportRepository.GetAllSystemReports();
        }

        public async Task<List<SystemReportDTO>> GetAllSystemReportsByUserId(int userId)
        {
            return await _reportRepository.GetAllSystemReportsByUserId(userId);
        }

        public async Task<SystemReportDetailDTO> GetSystemReportById(int id)
        {
            return await GetSystemReportById(id);
        }

        public async Task<bool> ReplySystemReport(ReplySystemReportDTO reportSystemDTO)
        {
            return await _reportRepository.ReplySystemReport(reportSystemDTO);
        }
    }
}

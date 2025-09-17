using Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Report
{
    public interface IReportRepository
    {
        public Task<bool> CreateSystemReport(SystemReportDTO reportSystemDTO);

        public Task<List<SystemReportDTO>> GetAllSystemReports();

        public Task<List<SystemReportDTO>> GetAllSystemReportsByUserId(int userId);


        public Task<SystemReportDetailDTO> GetSystemReportById(int id);

        public Task<bool> ReplySystemReport(ReplySystemReportDTO reportSystemDTO);
    }
}

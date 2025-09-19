using Data.DTOs;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Report
{
    public class ReportRepository : IReportRepository
    {
        private readonly SoundcloneContext _soundcloneContext;

        public ReportRepository(SoundcloneContext soundcloneContext)
        {
            _soundcloneContext = soundcloneContext;
        }
        public async Task<bool> CreateSystemReport(SystemReportDTO reportSystemDTO)
        {
            try
            {
                SystemReport newReport = new SystemReport()
                {
                    Content = reportSystemDTO.Content,
                    UserId = reportSystemDTO.UserId,
                    ReportDate = DateTime.Now,
                };

                _soundcloneContext.SystemReports.Add(newReport);
                await _soundcloneContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<SystemReportDTO>> GetAllSystemReports()
        {
            try
            {
                var reports = await _soundcloneContext.SystemReports.
                    Select(x => new SystemReportDTO
                    {
                        Content = x.Content,
                        isReplied = !x.ReplyContent.IsNullOrEmpty(),
                        SystemReportId = x.SystemReportId,
                        UserId = x.UserId,
                    }).ToListAsync();
                return reports;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<SystemReportDTO>> GetAllSystemReportsByUserId(int userId)
        {
            try
            {
                var reports = await _soundcloneContext.SystemReports
                    .Where(x => x.UserId == userId)
                    .Select(x => new SystemReportDTO
                    {
                        Content = x.Content,
                        isReplied = !x.ReplyContent.IsNullOrEmpty(),
                        SystemReportId = x.SystemReportId,
                        UserId = x.UserId,
                    }).ToListAsync();
                return reports;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<SystemReportDetailDTO> GetSystemReportById(int id)
        {
            try
            {
                var report = await _soundcloneContext.SystemReports.FindAsync(id);
                var result = new SystemReportDetailDTO()
                {
                    ReplyContent = report.ReplyContent,
                    ReplyDate = report.ReplyDate,
                    ReportDate = report.ReportDate,
                    Content = report.Content,
                    UserId = report.UserId,
                    SystemReportId = report.SystemReportId,
                };
                    
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> ReplySystemReport(ReplySystemReportDTO model)
        {
            try
            {
                var report = await _soundcloneContext.SystemReports.FindAsync(model.SystemReportId);
                if (report != null) {
                    report.ReplyDate = DateTime.Now;
                    report.Content = model.ReplyContent;

                    _soundcloneContext.SystemReports.Update(report);
                    await _soundcloneContext.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}

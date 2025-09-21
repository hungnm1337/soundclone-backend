using Data.DTOs;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Admin.Analysis
{
    public class AnalysisRepository : IAnalysisRepository
    {
        private readonly SoundcloneContext _soundcloneContext;
        public AnalysisRepository(SoundcloneContext soundcloneContext)
        {
            _soundcloneContext = soundcloneContext;
        }

        public async Task<List<MonthlyCount>> GetUserRegistrationCountByMonth()
        {
            var query = await _soundcloneContext.Users
                .GroupBy(u => new { u.CreateAt.Year, u.CreateAt.Month })
                .Select(g => new MonthlyCount
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Count = g.Count()
                })
                .OrderBy(x => x.Year)
                .ThenBy(x => x.Month)
                .ToListAsync();

            return query;
        }

        public async Task<List<MonthlyCount>> GetTrackUploadCountByMonth()
        {
            var query = await _soundcloneContext.Tracks
                .GroupBy(t => new { t.UploadDate.Year, t.UploadDate.Month })
                .Select(g => new MonthlyCount
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Count = g.Count()
                })
                .OrderBy(x => x.Year)
                .ThenBy(x => x.Month)
                .ToListAsync();

            return query;
        }

        // Thống kê số lượng invoice theo tháng
        public async Task<List<MonthlyCount>> GetInvoiceCountByMonth()
        {
            var query = await _soundcloneContext.Invoices
                .GroupBy(i => new { i.Date.Year, i.Date.Month })
                .Select(g => new MonthlyCount
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Count = g.Count()
                })
                .OrderBy(x => x.Year)
                .ThenBy(x => x.Month)
                .ToListAsync();

            return query;
        }

        // Thống kê tổng số tiền invoice theo tháng
        public async Task<List<MonthlyAmount>> GetTotalInvoiceAmountByMonth()
        {
            var query = await _soundcloneContext.Invoices
                .GroupBy(i => new { i.Date.Year, i.Date.Month })
                .Select(g => new MonthlyAmount
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    TotalAmount = g.Sum(x => x.TotalAmount)
                })
                .OrderBy(x => x.Year)
                .ThenBy(x => x.Month)
                .ToListAsync();

            return query;
        }
    }

}
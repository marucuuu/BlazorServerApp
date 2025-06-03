using BlazorServerApp.Models;
using BlazorServerApp.Data;
using Microsoft.EntityFrameworkCore;
using QRCoder;
using System.Text.Json;

namespace BlazorServerApp.Services
{
    public class AssetService
    {
        private readonly AppDbContext _context;

        public AssetService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAssetAsync(AssetsQR asset)
        {
            _context.AssetsQRs.Add(asset);
            await _context.SaveChangesAsync();

            var qrPayload = new
            {
                Id = asset.Id,
                EmployeeName = asset.EmployeeName,
                AssetType = asset.AssetType,
                SerialNumber = asset.SerialNumber,
                AssetCondition = asset.AssetCondition,
                Department = asset.Department,
                SubUnit = asset.SubUnit
            };

            string qrContent = JsonSerializer.Serialize(qrPayload);

            using var qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(qrContent, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new PngByteQRCode(qrCodeData);
            var qrCodeBytes = qrCode.GetGraphic(20);

            var fileName = $"wwwroot/qrcodes/asset_{asset.Id}.png";
            await File.WriteAllBytesAsync(fileName, qrCodeBytes);

            asset.QRCodeImagePath = $"/qrcodes/asset_{asset.Id}.png";
            await _context.SaveChangesAsync();
        }

        public async Task<List<AssetsQR>> GetAllAssetsAsync()
        {
            return await _context.AssetsQRs.ToListAsync();
        }

        // <-- Add this method:
        public async Task<AssetsQR?> GetAssetByIdAsync(int id)
        {
            return await _context.AssetsQRs.FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}

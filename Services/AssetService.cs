using BlazorServerApp.Models;
using BlazorServerApp.Data;

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
    }
}
}

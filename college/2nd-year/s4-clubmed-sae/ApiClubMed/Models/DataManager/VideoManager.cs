using ApiClubMed.Models.EntityFramework;
using ApiClubMed.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiClubMed.Models.DataManager
{
    public class VideoManager : IDataRepository<Video>
    {
        readonly ClubMedDBContexts? clubMedDBContext;
        public VideoManager() { }
        public VideoManager(ClubMedDBContexts context)
        {
            clubMedDBContext = context;
        }
        public async Task<ActionResult<IEnumerable<Video>>> GetAllAsync()
        {
            return await clubMedDBContext.Videos
                   .Include("LesResorts")
                   .ToListAsync();
        }
        public async Task<ActionResult<Video>> GetByIdAsync(int id)
        {
            return await clubMedDBContext.Videos
                   .Include("LesResorts")
                   .FirstOrDefaultAsync(u => u.VideoId == id);
        }

        public async Task<ActionResult<Video>> GetByStringAsync(string nom)
        {
            return await clubMedDBContext.Videos
                   .Include("LesResorts")
                   .FirstOrDefaultAsync(u => u.Lien.ToUpper() == nom.ToUpper());
        }
        public async Task AddAsync(Video entity)
        {
            await clubMedDBContext.Videos.AddAsync(entity);
            await clubMedDBContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Video video, Video entity)
        {
            clubMedDBContext.Entry(video).State = EntityState.Modified;
            video.VideoId = entity.VideoId;
            video.Lien = entity.Lien;
            video.LesResorts = video.LesResorts;

            await clubMedDBContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Video entity)
        {
            clubMedDBContext.Videos.Remove(entity);
            await clubMedDBContext.SaveChangesAsync();
        }
    }
}

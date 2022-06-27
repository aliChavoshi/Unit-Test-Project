using System.Collections.Generic;
using System.Linq;

namespace TestNinja.Mocking
{
    public interface IVideoRepository
    {
        IEnumerable<Video> GetUnprocessedVideos();
    }

    public abstract class VideoRepository : IVideoRepository
    {
        private readonly VideoContext _context;

        protected VideoRepository(VideoContext context)
        {
            _context = context;
        }

        public IEnumerable<Video> GetUnprocessedVideos()
        {
            return _context.Videos.Where(x => !x.IsProcessed).ToList();
        }
    }
}
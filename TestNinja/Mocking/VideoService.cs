using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace TestNinja.Mocking
{
    public class VideoService
    {
        private readonly IFileReader _fileReader;
        private readonly IVideoRepository _videoRepository;

        public VideoService(IFileReader fileReader, IVideoRepository videoRepository)
        {
            _fileReader = fileReader;
            _videoRepository = videoRepository;
        }

        public string ReadVideoTitle(string path)
        {
            var str = _fileReader.Read(path); //empty
            var video = JsonConvert.DeserializeObject<Video>(str);
            return video == null ? "Error parsing the video." : video.Title;
        }

        //[] => ""
        //[{},{},{}] => "1,2,3"
        public string GetUnprocessedVideosAsCsv()
        {
            var videos = _videoRepository.GetUnprocessedVideos();

            var videoIds = new List<int>();
            videoIds.AddRange(videos.Select(v => v.Id));

            return string.Join(",", videoIds);
        }
    }

    public class Video
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsProcessed { get; set; }
    }

    public class VideoContext : DbContext
    {
        public DbSet<Video> Videos { get; set; }
    }
}
using System;

namespace SpotifyApp.API.Helpers
{
    public class RankingParams
    {
        private const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int pageSize = 10;
        public int PageSize
        {
            get => pageSize;
            set => pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public int MinYear {get;set;} = 0;
        public int MaxYear {get;set;} = DateTime.Today.Year;
    }
}
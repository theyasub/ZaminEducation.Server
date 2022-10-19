namespace Users.Microservice.Models.Configurations
{
    public class PaginationParams
    {
        private const int _maxSize = 10;
        private int _pageSize;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > _maxSize ? _maxSize : value;
        }

        public int PageIndex { get; set; }
    }
}

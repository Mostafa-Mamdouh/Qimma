namespace PRJ.AuthServer
{
    public class Common
    {
        private readonly IConfiguration configuration;

		public Common(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string GetSiteURL()
        {
            return configuration["SiteURL"].ToString();
		}
		public string GetAuthURL()
		{
			return configuration["AuthURL"].ToString();
		}
	}
}

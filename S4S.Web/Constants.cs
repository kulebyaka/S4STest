namespace S4S.Web
{
	public static class Constants
	{
		public static class Swagger
		{
			public static string EndPoint => $"../swagger/{Version}/swagger.json";
			public static string ApiName => "licenses API";
			public static string Version => "v1";
		}

		public static class Health
		{
			public static string EndPoint => "/health";
		}
	}
}
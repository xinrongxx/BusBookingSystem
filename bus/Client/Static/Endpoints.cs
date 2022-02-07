namespace bus.Client.Static
{
    public static class Endpoints
    {
        private static readonly string Prefix = "api";

        public static readonly string BusesEndpoint = $"{Prefix}/buses";
        public static readonly string FeedbacksEndpoint = $"{Prefix}/feedbacks";
    }

}
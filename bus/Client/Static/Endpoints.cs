namespace bus.Client.Static
{
    public static class Endpoints
    {
        private static readonly string Prefix = "api";

        public static readonly string BusesEndpoint = $"{Prefix}/buses";
        public static readonly string FeedbacksEndpoint = $"{Prefix}/feedbacks";
        public static readonly string CustomersEndpoint = $"{Prefix}/customers";
        public static readonly string SeatsEndpoint = $"{Prefix}/seats";
        public static readonly string ServicesEndpoint = $"{Prefix}/services";
        public static readonly string BookingsEndpoint = $"{Prefix}/bookings";
    }

}
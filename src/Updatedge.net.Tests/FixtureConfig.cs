using AutoFixture;
using NUnit.Framework;
using Udatedge.Common.Models;

namespace Updatedge.net.Tests
{
    [SetUpFixture]
    public class FixtureConfig
    {
        public static IFixture Fixture;

        public const string BaseUrl = "https://localhost/";
        public const string ApiKey = "1234567890";

        public const string UserId1 = "UserId01";
        public const string UserId2 = "UserId02";

        public const string EventId0 = "EventId00";
        public const string EventId1 = "EventId01";

        public const string InviteId1 = "InviteId01";

        public const string OfferId1 = "OfferId01"; 

        public const string Worker1Email = "worker01@email.com";
        public const string Hirer1Email = "hirer01@email.com";
        public const string User1Email = "user01@email.com";

        public const string NotAnEmail = "#thisisnotanemail";

        public static ApiProblemDetails ApiProblemDetails204;
        public static ApiProblemDetails ApiProblemDetails400;
        public static ApiProblemDetails ApiProblemDetails401;
        public static ApiProblemDetails ApiProblemDetails403;
        public static ApiProblemDetails ApiProblemDetails429;
        public static ApiProblemDetails ApiProblemDetails500;

        [OneTimeSetUp]
        public void Init()
        {
            Fixture = new Fixture();

            // create 204 response
            ApiProblemDetails204 = Fixture.Build<ApiProblemDetails>()
                .With(a => a.Status, 204)
                .Create();

            // create 400 response
            ApiProblemDetails400 = Fixture.Build<ApiProblemDetails>()
                .With(a => a.Status, 400)
                .Create();

            // create 401 response
            ApiProblemDetails401 = Fixture.Build<ApiProblemDetails>()
                .With(a => a.Status, 401)
                .Create();

            // create 403 response
            ApiProblemDetails403 = Fixture.Build<ApiProblemDetails>()
                .With(a => a.Status, 403)
                .Create();

            // create 429 response
            ApiProblemDetails429 = Fixture.Build<ApiProblemDetails>()
                .With(a => a.Status, 429)
                .Create();

            // create 500 response
            ApiProblemDetails500 = Fixture.Build<ApiProblemDetails>()
                .With(a => a.Status, 500)
                .Create();
        }

    }

}

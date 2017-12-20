using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using StoryShovel.VsTs;

namespace StoryShovel
{
    public class Program
    {
        public static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        private static Task MainAsync()
        {
            var appsettings = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            var vstsBoard = new VsTsBoard(appsettings["VSTS:PrivateAccessToken"], appsettings["VSTS:TeamBaseAddress"], appsettings["VSTS:ProjectName"]);
            return vstsBoard.AddUserStory("A userstory from console app", "The description of the story from console app");
        }
    }
}

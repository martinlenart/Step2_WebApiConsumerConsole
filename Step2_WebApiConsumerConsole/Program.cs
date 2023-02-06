using Step2_WebApiConsumerConsole.Models;
using Step2_WebApiConsumerConsole.Services;

namespace Step2_WebApiConsumerConsole;
class Program
{
    static async Task Main(string[] args)
    {
        var ServiceUri = "https://localhost:41532";

        var service = new FriendsHttpService(new Uri(ServiceUri));

        Console.WriteLine("Testing GetInfoAsync()");
        var webApi = await service.GetInfoAsync();
        Console.WriteLine($"Title: {webApi?.Title}");
        Console.WriteLine($"Author: {webApi?.Author}");
        Console.WriteLine($"Version: {webApi?.Version}");

        Console.WriteLine("\nTesting GetFriendsAsync()");
        var friends = await service.GetFriendsAsync();
        if (friends == null) throw new Exception("Error testing GetFriendsAsync()");

        Console.WriteLine($"Nr of friends: {friends?.Count()}");
        Console.WriteLine($"First 5 friends:");
        friends?.Take(5).ToList().ForEach(c => Console.WriteLine(c));


        Console.WriteLine("\nTesting GetFriendsAsync(5)");
        friends = await service.GetFriendsAsync(5);
        if (friends == null) throw new Exception("Error testing GetFriendsAsync(5)");

        Console.WriteLine($"Nr of friends: {friends?.Count()}");
        friends?.ToList().ForEach(c => Console.WriteLine(c));


        Console.WriteLine("\nTesting GetFriendAsync(Guid Id)");
        var friend = await service.GetFriendAsync(friends!.First().FriendID);
        if (friend == null) throw new Exception("Error testing GetFriendAsync(Guid Id)");

        Console.WriteLine(friend);


        Console.WriteLine("\nTesting CreateFriendAsync(Friend friend)");
        friend = await service.CreateFriendAsync(Friend.Factory.CreateRandom());
        if (friend == null) throw new Exception("Error testing CreateFriendAsync(Friend friend)");

        Console.WriteLine(friend);


        Console.WriteLine("\nTesting UpdateFriendAsync(Friend friend)");
        friend.FirstName += "_Updated";
        friend.LastName += "_Updated";
        friend = await service.UpdateFriendAsync(friend);
        if (friend == null) throw new Exception("Error testing UpdateFriendAsync(Friend friend)");

        Console.WriteLine(friend);

        Console.WriteLine("\nTesting DeleteFriendAsync(Guid Id)");
        friend = await service.DeleteFriendAsync(friend.FriendID);
        if (friend == null) throw new Exception("Error testing DeleteFriendAsync(Guid Id)");

        Console.WriteLine(friend);


        Console.WriteLine("\nTesting GetQuotesAsync()");
        var quotes = await service.GetQuotesAsync();
        if (quotes == null) throw new Exception("Error testing GetQuotesAsync()");

        Console.WriteLine($"Nr of quotes: {quotes?.Count()}");
        Console.WriteLine($"First 5 quotes:");
        quotes?.Take(5).ToList().ForEach(c => Console.WriteLine($"{c.Quote}\n- {c.Author}"));


        Console.WriteLine("\nTesting GetQuotesAsync(5)");
        quotes = await service.GetQuotesAsync(5);
        if (quotes == null) throw new Exception("Error testing GetQuotesAsync(5)");

        Console.WriteLine($"Nr of quotes: {quotes?.Count()}");
        quotes?.ToList().ForEach(c => Console.WriteLine($"{c.Quote}\n- {c.Author}"));

    }
}


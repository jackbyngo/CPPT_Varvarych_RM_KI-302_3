using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using lab3.service;

public class TCPServer
{
    private readonly UserService _userService;
    private readonly BusService _busService;
    private readonly TripService _tripService;
    private readonly RatingService _ratingService;

    private readonly ILogger _logger;

    public TCPServer(
        UserService userService,
        BusService busService,
        TripService trailService,
        RatingService ratingService,
        ILogger<TCPServer> logger)
    {
        _userService = userService;
        _busService = busService;
        _tripService = trailService;
        _ratingService = ratingService;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        TcpListener listener = new TcpListener(IPAddress.Any, 5000);
        listener.Start();
        _logger.LogInformation("TCP Server started on port 5000.");

        while (!cancellationToken.IsCancellationRequested)
        {
            var client = await listener.AcceptTcpClientAsync();
            _ = HandleClientAsync(client);
        }
    }

    private async Task HandleClientAsync(TcpClient client)
    {
        _logger.LogInformation("Client connected.");
        using (var stream = client.GetStream())
        {
            var buffer = new byte[1024];
            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            string request = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            var data = JsonSerializer.Deserialize<Dictionary<string, string>>(request);
            var command = data["command"];

            try
            {
                if (command == "create_user")
                {
                    var username = data["username"];
                    var password = data["password"];
                    var passwordConfirm = data["passwordConfirm"];

                    var (success, message) = await _userService.RegisterUser(username, password, passwordConfirm);
                    var response = new
                    {
                        success = success,
                        message = message
                    };

                    var jsonResponse = JsonSerializer.Serialize(response);
                    await stream.WriteAsync(Encoding.UTF8.GetBytes(jsonResponse));
                }
                else if (command == "login_user")
                {
                    var username = data["username"];
                    var password = data["password"];

                    var (success, message) = await _userService.LoginUser(username, password);
                    var response = new
                    {
                        success = success,
                        message = message
                    };

                    var jsonResponse = JsonSerializer.Serialize(response);
                    await stream.WriteAsync(Encoding.UTF8.GetBytes(jsonResponse));
                }
                else if (command == "add_bus")
                {
                    var name = data["name"];
                    var number = int.Parse(data["number"]);
                    var amountSeats = int.Parse(data["amountSeats"]);

                    var (success, message) = await _busService.CreateBus(name, number, amountSeats);
                    var response = new
                    {
                        success = success,
                        message = message
                    };

                    var jsonResponse = JsonSerializer.Serialize(response);
                    await stream.WriteAsync(Encoding.UTF8.GetBytes(jsonResponse));
                }
                else if (command == "get_bookings_for_trip")
                {
                    // Перевірка на наявність tripId у словнику
                    if (data.ContainsKey("tripId"))
                    {
                        var tripId = int.Parse(data["tripId"]);
                        var bookings = await _tripService.GetBookingsForTrip(tripId);

                        // Формуємо відповідь
                        var response = new
                        {
                            success = true,
                            bookedSeats = bookings.Select(b => b.SeatNumber).ToList()
                        };

                        var jsonResponse = JsonSerializer.Serialize(response);

                        await stream.WriteAsync(Encoding.UTF8.GetBytes(jsonResponse));
                    }
                    else
                    {
                        // Якщо ключа tripId немає, повертаємо помилку
                        var errorResponse = new
                        {
                            success = false,
                            message = "Missing tripId in the request"
                        };
                        var jsonErrorResponse = JsonSerializer.Serialize(errorResponse);
                        await stream.WriteAsync(Encoding.UTF8.GetBytes(jsonErrorResponse));
                    }
                }


                else if (command == "create_trip")
                {
                    var name = data?["name"];
                    var busId = int.Parse(data?["busId"]);
                    var startTime = DateTime.Parse(data?["startTime"]);
                    var endTime = DateTime.Parse(data?["endTime"]);

                    var (success, message) = await _tripService.CreateTrip(name, busId, startTime, endTime);
                    var response = new
                    {
                        success = success,
                        message = message
                    };

                    var jsonResponse = JsonSerializer.Serialize(response);
                    await stream.WriteAsync(Encoding.UTF8.GetBytes(jsonResponse));
                }
                else if (command == "get_bookings_for_trip")
                {
                    // Перевірка на наявність tripId у словнику
                    if (data.ContainsKey("tripId"))
                    {
                        var tripId = int.Parse(data["tripId"]);
                        var bookings = await _tripService.GetBookingsForTrip(tripId);

                        // Формуємо відповідь
                        var response = new
                        {
                            success = true,
                            bookedSeats = bookings.Select(b => b.SeatNumber).ToList()
                        };

                        var jsonResponse = JsonSerializer.Serialize(response);

                        await stream.WriteAsync(Encoding.UTF8.GetBytes(jsonResponse));
                    }
                    else
                    {
                        // Якщо ключа tripId немає, повертаємо помилку
                        var errorResponse = new
                        {
                            success = false,
                            message = "Missing tripId in the request"
                        };
                        var jsonErrorResponse = JsonSerializer.Serialize(errorResponse);
                        await stream.WriteAsync(Encoding.UTF8.GetBytes(jsonErrorResponse));
                    }
                }

                else if (command == "get_trips")
                {
                    var trips = await _tripService.GetTrips();
                    var response = new
                    {
                        success = true,
                        trips = trips
                    };

                    var jsonResponse = JsonSerializer.Serialize(response);
                    await stream.WriteAsync(Encoding.UTF8.GetBytes(jsonResponse));
                }
                else if (command == "get_trips_by_name")
                {
                    var name = data?["name"];

                    var trips = await _tripService.SearchTripsByName(name);
                    var jsonResponse = JsonSerializer.Serialize(trips);

                    await stream.WriteAsync(Encoding.UTF8.GetBytes(jsonResponse));
                }
                else if (command == "get_trips_by_start_time")
                {
                    var startTime = DateTime.Parse(data?["startTime"]);

                    var trips = await _tripService.SearchTripsByStartDate(startTime);
                    var jsonResponse = JsonSerializer.Serialize(trips);

                    await stream.WriteAsync(Encoding.UTF8.GetBytes(jsonResponse));
                }
                else if (command == "take_a_seat_on_bus")
                {
                    var userId = long.Parse(data["userId"]);
                    var busName = int.Parse(data["busNumber"]);
                    var seatNumber = int.Parse(data["seatNumber"]);

                    Console.WriteLine(userId);
                    Console.WriteLine(busName);
                    Console.WriteLine(seatNumber);

                    var (success, message) = await _busService.TakeASeat(userId, busName, seatNumber);
                    var response = new
                    {
                        success = success,
                        message = message
                    };

                    var jsonResponse = JsonSerializer.Serialize(response);
                    await stream.WriteAsync(Encoding.UTF8.GetBytes(jsonResponse));
                }
                else if (command == "add_rating")
                {
                    var number = int.Parse(data?["number"]);
                    var tripId = int.Parse(data?["tripId"]);

                    var (success, message) = await _ratingService.AddRating(number, tripId);
                    var response = new
                    {
                        success = success,
                        message = message
                    };

                    var jsonResponse = JsonSerializer.Serialize(response);
                    await stream.WriteAsync(Encoding.UTF8.GetBytes(jsonResponse));
                }
                else if (command == "get_trip_ratings")
                {
                    var trips = await _ratingService.GetTripsRating();
                    var jsonResponse = JsonSerializer.Serialize(trips);

                    await stream.WriteAsync(Encoding.UTF8.GetBytes(jsonResponse));
                }
            }
            catch (InvalidOperationException ex)
            {
                await stream.WriteAsync(Encoding.UTF8.GetBytes($"Error: {ex.Message}"));
            }
            catch (Exception ex)
            {
                await stream.WriteAsync(Encoding.UTF8.GetBytes($"Error: {ex.Message}"));
            }
        }

        client.Close();
    }
}
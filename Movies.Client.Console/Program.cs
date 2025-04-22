using Movies.GraphQL.Client.Services;
using Movies.Models;

var service = new MovieService();

/*
var movie = await service.GetMovie(12);
Console.WriteLine($"Movie: {movie.Name}");

var movies = await service.GetMovies();
Console.WriteLine($"Total of movies: {movies.Count}");

var newMovie = new Movie()
{
    Name = "Filme 50",
    Description = "Descrição do filme 50",
    LaunchDate = DateTime.Now,
    Genre = Movies.Models.Enums.MovieGenre.Comedy
};

var addedMovie = await service.AddMovie(newMovie);
Console.WriteLine($"Movie added - Id: {addedMovie.Id} - Name: {addedMovie.Name}");


var updateMovieId = 51;
var updateMovie = new Movie()
{
    Name = "Die Hard",
    Description = "Descrição do filme Duro de matar",
    LaunchDate = DateTime.Now,
    Genre = Movies.Models.Enums.MovieGenre.Action
};

var updatedMovie = await service.UpdateMovie(updateMovieId, updateMovie);
Console.WriteLine($"Movie updated - Id: {updatedMovie.Id} - Name: {updatedMovie.Name} - Description: {updatedMovie.Description}");

var movieIdToDelete = 52;
bool deleted = await service.DeleteMovie(movieIdToDelete);

Console.WriteLine($"Movie deleted {movieIdToDelete} - {deleted}");
*/

var sub = service.SubscriptionMovieAdded();

sub.Subscribe(response =>
{
    Console.WriteLine($"Sub - Movie Added: {response.Data.Movie.Id} - {response.Data.Movie.Name}");
});

Console.ReadKey();
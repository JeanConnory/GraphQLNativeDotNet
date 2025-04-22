using GraphQL;
using GraphQL.Client.Http;
using Movies.GraphQL.Client.DTO;
using Movies.Models;

namespace Movies.GraphQL.Client.Services;

public class MovieService : GraphQLClient
{
    public async Task<Movie> GetMovie(int id)
    {
        var query = new GraphQLHttpRequest()
        {
            Query = " query GetMovie($movieId: Int!) { movie(id: $movieId) { id name description launchDate genre } }",
            Variables = new { movieId = id }
        };

        var response = await Client.SendQueryAsync<MovieResponse>(query);

        return response.Data.Movie;
    }

    public async Task<List<Movie>> GetMovies()
    {
        var query = new GraphQLHttpRequest()
        {
            Query = "{ movies { id name description launchDate genre } }"
        };

        var response = await Client.SendQueryAsync<MoviesResponse>(query);

        return response.Data.Movies;
    }

    public async Task<Movie> AddMovie(Movie movie)
    {
        var query = new GraphQLHttpRequest()
        {
            Query = "mutation AddMovie($movie:MovieInput!)  { movie:addMovie(movie:$movie) { id name description launchDate genre }}",
            Variables = new { movie = new { movie.Name, movie.Description, movie.LaunchDate, movie.Genre } }
        };

        var response = await Client.SendMutationAsync<MovieResponse>(query);

        return response.Data.Movie;
    }

    public async Task<Movie> UpdateMovie(int id, Movie movie)
    {
        var query = new GraphQLHttpRequest()
        {
            Query = "mutation updateMovie($id:ID! $movie:MovieInput!)  { movie:updateMovie(id:$id movie:$movie) { id name description launchDate genre }}",
            Variables = new { id = id, movie = new { movie.Name, movie.Description, movie.LaunchDate, movie.Genre } }
        };

        var response = await Client.SendMutationAsync<MovieResponse>(query);

        return response.Data.Movie;
    }

    public async Task<Boolean> DeleteMovie(int id)
    {
        var query = new GraphQLHttpRequest()
        {
            Query = "mutation deleteMovie($id:ID!) { deleted:deleteMovie(id:$id) }",
            Variables = new { id = id }
        };

        var response = await Client.SendMutationAsync<DeleteResponse>(query);

        return response.Data.Deleted;
    }

    public IObservable<GraphQLResponse<MovieResponse>> SubscriptionMovieAdded()
    {
        var query = new GraphQLHttpRequest()
        {
            Query = "subscription MovieAdded { movie:movieAdded { id name description launchDate genre } }"
        };        

        return Client.CreateSubscriptionStream<MovieResponse>(query);
    }
}
